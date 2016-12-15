using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
    public const float speed = 10;

    public const float jumpHeight = 15;

    public float sensitivityX;
    public float sensitivityY;


    private const int minX = -180;
    private const int maxX = 180;
    private const int minY = -90;
    private const int maxY = 90;
    private const float gravity = -1;

    private float velocityY = 0;
    private float rotationX = 0;
    private float rotationY = 0;

    public Camera camera;

    private PlayerOptions options;

    private CharacterController controller;

    void Start()
    {
        options = GetComponent<PlayerOptions>();
        sensitivityX = options.sensitivityX;
        sensitivityY = options.sensitivityY;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            rotationX += mouseX;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);

            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
            rotationY += mouseY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            if (rotationX >= 180)
            {
                rotationX = -180;
            }
            else if (rotationX <= -180)
            {
                rotationX = 180;
            }

            camera.transform.localRotation = Quaternion.Euler(new Vector3(-rotationY, camera.transform.localRotation.y, camera.transform.localRotation.z));
            transform.rotation = Quaternion.Euler(transform.rotation.x, rotationX, transform.rotation.z);
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = ((transform.forward * moveZ) + (transform.right * moveX * .65f)) * speed * Time.deltaTime;

            if (!controller.isGrounded)
            {
                velocityY += gravity * Time.deltaTime;
            }
            else
            {
                velocityY = 0;
            }

            velocityY += gravity;
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                velocityY += jumpHeight;
            }

            move += (transform.up * velocityY * Time.deltaTime);

            controller.Move(move);
        }
    }
}
