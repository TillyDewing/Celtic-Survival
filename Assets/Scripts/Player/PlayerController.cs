using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
    public float speed = 10;

    public float jumpHeight = 10;

    public float sensitivityX;
    public float sensitivityY;

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
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = ((transform.forward * moveZ) + (transform.right * moveZ)) * speed * Time.deltaTime;

            controller.SimpleMove(move);
        }
    }
}
