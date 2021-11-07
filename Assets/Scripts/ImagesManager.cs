using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ImagesManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    private NetworkService _network;
    private Texture2D _webImage; // переменная для хранения скачанного изображения

    public void Startup(NetworkService service) {
        Debug.Log("Images manager starting...");

        _network = service;
        status = ManagerStatus.Started;
    }

    public void GetWebImage(Action<Texture2D> callback) {
        if (_webImage == null) { // проверяем, нет ли уже сохраненного изображения
            StartCoroutine(_network.DownloadImage((Texture2D image) => {
                _webImage = image; // сохраняем скачанное изображение
                callback(_webImage); // обратный вызов используется в лямбда-функции, а не отправляется непосредственно в NetworkService
            }));
        } else {
            callback(_webImage); // при наличии сохраненного изображения сразу активируется обратный вызов
        }
    }
}
