using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    private CharacterController _charController;
    public float gravity = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); // ограничим движение по диагонали той же скоростью, что и движение параллельно осям
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement); // пребразуем вектор движения от локальных к глобальным координатам
        _charController.Move(movement); // заставим этот вектор перемещать компонент CharacterController
    }
}
