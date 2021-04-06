using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private CharacterController _characterController;

    [SerializeField] private Transform endMarker;
    [SerializeField] private GameObject pause;
    [SerializeField] private Transform sphereCheck;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 velocity;

    public static bool isPaused;
    private bool isGrounded;

    public float sensetiveMouse = 9f;
    public float speed = 6f;
    public float gravity = -9.8f;
    public float jumpForce = 10f;

    private float minMaxVert = 60f;
    private float _rotationX = 0;

    void Start()
    {
        Time.timeScale = 1f; 

        _camera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();

        // Закрепление курсора в центре экрана и отключение его видимости
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!pause.activeSelf && !Phone.phone)
        {
            // Поворот камеры вокруг оси X
            _rotationX -= Input.GetAxis("Mouse Y") * sensetiveMouse;
            _rotationX = Mathf.Clamp(_rotationX, -minMaxVert, minMaxVert);

            _camera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);


            // Поворот игрока и камеры вокруг оси Y
            float delta = Input.GetAxis("Mouse X") * sensetiveMouse;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(0, rotationY, 0);
        }

        if (Phone.phone)
        {
            _camera.transform.LookAt(endMarker, endMarker.up);
        }

        // Передвижение игрока по осям X и Z
        Vector3 movment = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        movment = Vector3.ClampMagnitude(movment, speed);

        movment = transform.TransformDirection(movment * Time.deltaTime);
        _characterController.Move(movment);


        // Механика прыжка
        isGrounded = Physics.CheckSphere(sphereCheck.position, 0.5f, groundLayer);

        if (isGrounded && velocity.y < 0)
            velocity.y = 0;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        _characterController.Move(velocity * Time.deltaTime);

        //пауза
        if (Input.GetButtonDown("Cancel"))
        {
            pause.SetActive(!pause.activeSelf);
            if (!pause.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                isPaused = true;
            }
        }

    }
}
