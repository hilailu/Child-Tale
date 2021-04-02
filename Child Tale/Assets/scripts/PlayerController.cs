using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody _playerRB;

    public float sensetiveMouse = 9f;
    public float speed = 6f;
    public float gravity = 1f;
    public float jumpForce = 10f;

    private float minMaxVert = 45f;
    private float _rotationX = 0;


    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _playerRB = GetComponent<Rigidbody>();
        _playerRB.freezeRotation = true;
        Physics.gravity *= gravity;
    }

    void Update()
    {
        // Закрепление курсора в центре экрана и отключение его видимости
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // Поворот камеры вокруг оси X
        _rotationX -= Input.GetAxis("Mouse Y") * sensetiveMouse;
        _rotationX = Mathf.Clamp(_rotationX, -minMaxVert, minMaxVert);

        _camera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);


        // Поворот игрока и камеры вокруг оси Y
        float delta = Input.GetAxis("Mouse X") * sensetiveMouse;
        float rotationY = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(0, rotationY, 0);


        // Передвижение игрока по осям X и Z
        Vector3 movment = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        movment = Vector3.ClampMagnitude(movment, speed);

        movment *= Time.deltaTime;
        transform.Translate(movment);


        // Механика прыжка 
        if (Input.GetKeyDown(KeyCode.Space) && _playerRB.velocity.y == 0)
        {
            _playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
