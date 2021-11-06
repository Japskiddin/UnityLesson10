using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherConstroller : MonoBehaviour
{
    [SerializeField] private Material sky; // ссылка может быть не только на объект, но и на материал
    [SerializeField] private Light sun;

    private float _fullIntensity;

    private void Awake() {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    // Start is called before the first frame update
    void Start()
    {
        _fullIntensity = sun.intensity; // начальная интенсивность рассматривается как "полная"
    }

    private void OnWeatherUpdated() {
        SetOvercast(Managers.Weather.cloudValue); // используем величину облачности из сценария WeatherController
    }

    private void SetOvercast(float value) { // корректируем значение Blend материала и интенсивность света
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
