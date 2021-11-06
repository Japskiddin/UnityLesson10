using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkService
{
    // url адрес для отправки запроса
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Ulan-Ude,ru&mode=xml&APPID=d703f9ab2e2ae738eea812a5dda8b647";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Ulan-Ude,ru&APPID=d703f9ab2e2ae738eea812a5dda8b647";

    private IEnumerator CallAPI(string url, Action<string> callback) {
        using (UnityWebRequest request = UnityWebRequest.Get(url)) { // создаём UnityWebRequest в режиме GET
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { // проверяем ответ на наличие ошибок
                Debug.LogError("Network problem: " + request.error);
            } else if (request.responseCode != (long) System.Net.HttpStatusCode.OK) {
                Debug.LogError("Response error: " + request.responseCode);
            } else {
                callback(request.downloadHandler.text); // делегат можно вызвать так же, как и исходную функцию
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback) { // каскад ключевых слов yield в вызывающих друг друга методах сопрограммы
        return CallAPI(xmlApi, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback) {
        return CallAPI(jsonApi, callback);
    }
}
