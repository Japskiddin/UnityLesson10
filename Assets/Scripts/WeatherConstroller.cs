using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherConstroller : MonoBehaviour
{
    [SerializeField] private Material sky; // ������ ����� ���� �� ������ �� ������, �� � �� ��������
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
        _fullIntensity = sun.intensity; // ��������� ������������� ��������������� ��� "������"
    }

    private void OnWeatherUpdated() {
        SetOvercast(Managers.Weather.cloudValue); // ���������� �������� ���������� �� �������� WeatherController
    }

    private void SetOvercast(float value) { // ������������ �������� Blend ��������� � ������������� �����
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
