using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable
{
    public Camera cameraMain;
    private CharacterController _characterController;
    [SerializeField] private Transform sphereCheck;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 velocity;

    private bool isGrounded;

    public float sensetiveMouse = 9f;
    public float speed = 6f;
    public float gravity = -9.8f;
    public float jumpForce = 10f;
    public Vector3 pos;
    public Quaternion rot;

    private float minMaxVert = 60f;
    private float _rotationX = 0;

    public Photon.Pun.PhotonView photonView;
    public string nickName;

    void Start()
    {
        //isPaused = false;
        photonView = GetComponent<Photon.Pun.PhotonView>();

        Time.timeScale = 1f;

        cameraMain = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();


        // Закрепление курсора в центре экрана и отключение его видимости
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        if (!photonView.IsMine)
        {
            Destroy(cameraMain.gameObject);
        }
    }

    void Update()
    {
        if (photonView.IsMine && !Phone.phone && !GameManager.isPaused)
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

            pos = transform.position;
            rot = transform.rotation;
        }
    }

    public void Save()
    {
        Debug.Log("Save Player");
        PlayerPrefs.SetString("PlayerJSON", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Debug.Log("Load Player");
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerJSON"), this);
        transform.position = pos;
        transform.rotation = rot;
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("PlayerJSON");
    }
}
