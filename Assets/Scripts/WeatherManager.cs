using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager {
    public ManagerStatus status {get; private set; }

    // ���� ����������� �������� ����������
    private NetworkService _network;
    public float cloudValue { get; private set; } // ���������� ������������� ���������, � ��������� ������ ��� �������� ������������� ������ ��� ������

    public void Startup(NetworkService service) {
        Debug.Log("Weather manager starting...");
        _network = service; // ��������� ����������� ������ NetworkService
        StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded)); // �������� �������� ������ �� ���������
        status = ManagerStatus.Initializing;
    }

    public void OnXMLDataLoaded(string data) { // ����� ��������� ������ ����� ����� �������� ������
        Debug.Log(data);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data); // ��������� XML �� ��������� � ������������ ������
        XmlNode root = doc.DocumentElement;
        XmlNode node = root.SelectSingleNode("clouds"); // ��������� ������ � ���� ����
        string value = node.Attributes["value"].Value;
        cloudValue = Convert.ToInt32(value) / 100f; // ����������� � float � ��������� �� 0 �� 1
        Debug.Log("Value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED); // ��������� ���������, ������������� ��������� ��������

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
