using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float lookSpeed = 100f;
    public Vector3 offset = new Vector3(0f, 0.2f, -10f);


    void Update()
    {
        if (playerBody == null) return;

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1f;
            Debug.Log("Left arrow pressed");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1f;
            Debug.Log("Right arrow pressed");
        }
        if (Input.GetKey(KeyCode.UpArrow)) vertical = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) vertical = -1f;

        // Rotar jugador en Y
        playerBody.Rotate(Vector3.up * horizontal * lookSpeed * Time.deltaTime);

        // Rotar cámara local en X (pitch)
        xRotation -= vertical * lookSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotar cámara local en Y (yaw) - aquí la cámara rota independientemente del jugador
        yRotation += horizontal * lookSpeed * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        // Aplica rotación combinada a la cámara
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Posicionar cámara con offset fijo sin afectar rotación
        transform.position = playerBody.position + offset;
    }

    public void SetPlayerBody(Transform player)
    {
        playerBody = player;
    }
}
