using UnityEngine;
using TMPro;
using System.Collections;


public class ExplosionManager : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab para la explosión
    public TextMeshProUGUI bannerText; // Texto del banner de explosión
    public float bannerDuration = 2f; // Duración del banner en segundos
    public float cellSize = 3f; // Tamaño de cada celda en Unity
    public Camera mainCamera;

    // Metodo para generar explosiones
    public void GenerateExplosions(int[][] explosions)
    {
        foreach (int[] explosion in explosions)
        {
            Debug.Log("HUBO UN BOOM");
            // Convertir la posición lógica a posición del mundo
            Vector3 position = new Vector3(explosion[0] * cellSize, 0, explosion[1] * cellSize);

            // Instanciar el prefab de explosión
            GameObject explosionfab = Instantiate(explosionPrefab, position, Quaternion.identity);

            Destroy(explosionfab, 3);


            // Agregar sacudida de cámara
            CameraShake cameraShake = mainCamera.GetComponent<CameraShake>();
            StartCoroutine(cameraShake.Shake(0.5f, 0.2f)); // Sacudida de cámara

            // Mostrar el banner
            StartCoroutine(ShowBanner());
        }

        
    }

    // Coroutine para mostrar el banner
    IEnumerator ShowBanner()
    {
        bannerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(bannerDuration);
        bannerText.gameObject.SetActive(false);
    }
}
