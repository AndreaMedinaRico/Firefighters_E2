using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocidad de movimiento
    public float zoomSpeed = 10f; // Velocidad de zoom
    public float minZoom = 20f; // Límite mínimo de zoom
    public float maxZoom = 100f; // Límite máximo de zoom
    public Vector2 xLimits = new Vector2(-50f, 50f); // Límite de movimiento en X
    public Vector2 zLimits = new Vector2(-50f, 50f); // Límite de movimiento en Z

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Movimiento con WASD
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A/D
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;   // W/S

        Vector3 newPosition = transform.position + new Vector3(-moveZ, 0, moveX);

        // Limitar el movimiento dentro de los límites
        newPosition.x = Mathf.Clamp(newPosition.x, xLimits.x, xLimits.y);
        newPosition.z = Mathf.Clamp(newPosition.z, zLimits.x, zLimits.y);

        transform.position = newPosition;

        // Zoom con E y Q
        float scrollInput = 0f;
        if (Input.GetKey(KeyCode.E)) scrollInput = -zoomSpeed * Time.deltaTime; // Acercar
        if (Input.GetKey(KeyCode.Q)) scrollInput = zoomSpeed * Time.deltaTime;  // Alejar

        if (cam != null)
        {
            float newZoom = cam.fieldOfView + scrollInput;
            cam.fieldOfView = Mathf.Clamp(newZoom, minZoom, maxZoom); // Limitar el zoom
        }
    }
}
