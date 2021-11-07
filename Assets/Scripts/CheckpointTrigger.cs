using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public string identifier;
    private bool _triggered; // проверяем, срабатывала ли ужеs контрольная точка

    private void OnTriggerEnter(Collider other) {
        if (_triggered) return;

        Managers.Weather.LogWeather(identifier); // вызываем для отправки данных
        _triggered = true;
    }
}
