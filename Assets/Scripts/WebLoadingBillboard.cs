using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour
{
    public void Operate() {
        Managers.Images.GetWebImage(OnWebImage); // вызываем метод в сценарии ImagesManager
    }

    public void OnWebImage(Texture2D image) {
        GetComponent<Renderer>().material.mainTexture = image; // при обратном вызове скачанное изображение назначается материалу
    }
}
