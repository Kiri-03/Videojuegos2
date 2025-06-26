using Unity.Netcode;
using UnityEngine;

public class PlayerManagement : NetworkBehaviour
{
    [SerializeField] private float speed = 8f;
    private Rigidbody rb;
    private MouseLook mouseLook;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (IsOwner)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;

            // Asignar cámara para controlar rotación y seguimiento
            mouseLook = Camera.main.GetComponent<MouseLook>();
            if (mouseLook != null)
            {
                mouseLook.SetPlayerBody(transform);
            }
            else
            {
                Debug.LogError("No se encontró MouseLook en la cámara principal");
            }
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movement += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right;

        movement = movement.normalized * speed;

        rb.velocity = movement;
    }

}
