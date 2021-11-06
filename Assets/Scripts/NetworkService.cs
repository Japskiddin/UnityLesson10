using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkService
{
    // url ����� ��� �������� �������
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Ulan-Ude,ru&mode=xml&APPID=d703f9ab2e2ae738eea812a5dda8b647";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Ulan-Ude,ru&APPID=d703f9ab2e2ae738eea812a5dda8b647";

    private IEnumerator CallAPI(string url, Action<string> callback) {
        using (UnityWebRequest request = UnityWebRequest.Get(url)) { // ������ UnityWebRequest � ������ GET
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { // ��������� ����� �� ������� ������
                Debug.LogError("Network problem: " + request.error);
            } else if (request.responseCode != (long) System.Net.HttpStatusCode.OK) {
                Debug.LogError("Response error: " + request.responseCode);
            } else {
                callback(request.downloadHandler.text); // ������� ����� ������� ��� ��, ��� � �������� �������
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback) { // ������ �������� ���� yield � ���������� ���� ����� ������� �����������
        return CallAPI(xmlApi, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback) {
        return CallAPI(jsonApi, callback);
    }
}
