using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable
{
    public Camera cameraMain;
    private CharacterController _characterController;
    [SerializeField] private Transform sphereCheck;
    [SerializeField] private LayerMask groundLayer;
    private Phone phone;
    private Vector3 velocity;

    private bool isGrounded;
    public bool isHungry;

    public float sensetiveMouse = 9f;
    public float speed = 6f;
    public float gravity = -9.8f;
    public float jumpForce = 10f;

    private float minMaxVert = 60f;
    private float _rotationX = 0;

    public PhotonView photonView;


    private void Awake()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
        photonView = GetComponent<PhotonView>();
        cameraMain = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        phone = GetComponentInChildren<Phone>();
        isHungry = true;
    }

    void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }

    void Start()
    {
        // ???????? ?????? ?????? ? ??????????????
        if (!photonView.IsMine)
            Destroy(cameraMain.gameObject);
    }

    void Update()
    {
        if (photonView.IsMine && !GameManager.isPaused && !phone.phone)
        {
            // ??????? ?????? ?????? ??? X
            _rotationX -= Input.GetAxis("Mouse Y") * sensetiveMouse;
            _rotationX = Mathf.Clamp(_rotationX, -minMaxVert, minMaxVert);

            cameraMain.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);



            // ??????? ?????? ? ?????? ?????? ??? Y
            float delta = Input.GetAxis("Mouse X") * sensetiveMouse;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(0, rotationY, 0);



            // ???????????? ?????? ?? ???? X ? Z
            Vector3 movment = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            movment = Vector3.ClampMagnitude(movment, speed);

            movment = transform.TransformDirection(movment * Time.deltaTime);
            _characterController.Move(movment);



            // ???????? ??????
            isGrounded = Physics.CheckSphere(sphereCheck.position, 0.5f, groundLayer);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        _characterController.Move(velocity * Time.deltaTime);
    }

    public void Save()
    {
        PlayerData.instance.pos = this.transform.position;
        PlayerData.instance.rot = this.transform.rotation;
        PlayerData.instance.isHungry = this.isHungry;
    }

    public void Load()
    {
        this.transform.position = PlayerData.instance.pos;
        this.transform.rotation = PlayerData.instance.rot;
        this.isHungry = PlayerData.instance.isHungry;
    }
}
