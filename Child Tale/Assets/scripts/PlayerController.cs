using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable, IPunObservable
{
    public Camera cameraMain;
    private CharacterController _characterController;
    [SerializeField] private Transform sphereCheck;
    [SerializeField] private LayerMask groundLayer;
    private Phone phone;
    private Vector3 velocity;

    private bool isGrounded;
    private bool isRed;

    public float sensetiveMouse = 9f;
    public float speed = 6f;
    public float gravity = -9.8f;
    public float jumpForce = 10f;

    private float minMaxVert = 60f;
    private float _rotationX = 0;

    public PhotonView photonView;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        cameraMain = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        phone = GetComponentInChildren<Phone>();
    }

    void Start()
    {
        // Закрепление курсора в центре экрана и отключение его видимости
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // Удаление второй камеры и аудиолистенера
        if (!photonView.IsMine)
            Destroy(cameraMain.gameObject);
    }

    void Update()
    {
        if (photonView.IsMine && !phone.phone && !GameManager.isPaused)
        {
            // Поворот камеры вокруг оси X
            _rotationX -= Input.GetAxis("Mouse Y") * sensetiveMouse;
            _rotationX = Mathf.Clamp(_rotationX, -minMaxVert, minMaxVert);

            cameraMain.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);



            // Поворот игрока и камеры вокруг оси Y
            float delta = Input.GetAxis("Mouse X") * sensetiveMouse;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(0, rotationY, 0);



            // Передвижение игрока по осям X и Z
            Vector3 movment = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            movment = Vector3.ClampMagnitude(movment, speed);

            movment = transform.TransformDirection(movment * Time.deltaTime);
            _characterController.Move(movment);



            // Механика прыжка
            isGrounded = Physics.CheckSphere(sphereCheck.position, 0.5f, groundLayer);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);

            velocity.y += gravity * Time.deltaTime;
            _characterController.Move(velocity * Time.deltaTime);



            // Test
            if (Input.GetKey(KeyCode.Z))
            {
                isRed = true;
            }
            else isRed = false;
        }
        if (isRed) GetComponent<MeshRenderer>().material.color = Color.red;
        else GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void Save()
    {
        PlayerData.instance.pos = this.transform.position;
        PlayerData.instance.rot = this.transform.rotation;
    }

    public void Load()
    {
        this.transform.position = PlayerData.instance.pos;
        this.transform.rotation = PlayerData.instance.rot;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isRed);
        }
        else if (stream.IsReading)
        {
            isRed = (bool)stream.ReceiveNext();
        }
    }
}
