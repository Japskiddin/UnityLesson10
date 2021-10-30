using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // структура, которая будет сопоставлять имена с параметрами
    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f; // скорость вращения
    public float sensitivityVert = 9.0f; // поворот в вертикальной плоскости

    public float maximumVert = 45.0f;
    public float minimumVert = -45.0f;

    private float _rotationX = 0; // угол поворота по вертикали

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) {
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX) {
            // это поворот в горизонтальной плоскости
            Debug.Log("Mouse x - " + Input.GetAxis("Mouse X"));
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        } else if (axes == RotationAxes.MouseY) {
            // это поворот в вертикальной плоскости
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert; // увеличиваем угол поворота по вертикали в соответствии с перемещениями указателя мыши
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); // фиксируем угол поворота по вертикали в диапазоне, заданном минимальным и максимальным значениями
            float rotationY = transform.localEulerAngles.y; // сохраняем одинаковый угол поворота вокруг оси Y (то есть вращение в горизонтальной плоскости отсутсвует)
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // создаём новым вектор из сохранённых значений поворота
        } else {
            // это комбинированный поворот
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor; // величина изменения угла поворота
            float rotationY = transform.localEulerAngles.y + delta; // приращение угла поворота через значение delta

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
