using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager {
    public ManagerStatus status {get; private set; }

    // сюда добавляется величина облачности
    private NetworkService _network;
    public float cloudValue { get; private set; } // облачность редактируется внутренне, в остальных местах это свойство предназначено только для чтения

    public void Startup(NetworkService service) {
        Debug.Log("Weather manager starting...");
        _network = service; // сохраняем вставленный объект NetworkService
        StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded)); // начинаем загрузку данных из интернета
        status = ManagerStatus.Initializing;
    }

    public void OnXMLDataLoaded(string data) { // метод обратного вызова сразу после загрузки данных
        Debug.Log(data);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data); // разбираем XML на структуру с возможностью поиска
        XmlNode root = doc.DocumentElement;
        XmlNode node = root.SelectSingleNode("clouds"); // извлекаем данные в один узел
        string value = node.Attributes["value"].Value;
        cloudValue = Convert.ToInt32(value) / 100f; // преобразуем в float в диапазоне от 0 до 1
        Debug.Log("Value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED); // рассылаем сообщение, информирующее остальные сценарии

        status = ManagerStatus.Started;
    }

    public void OnJSONDataLoaded(string data) {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;

        Dictionary<string, object> clouds = (Dictionary<string, object>)dict["clouds"];
        cloudValue = (long)clouds["all"] / 100f;
        Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }
}
