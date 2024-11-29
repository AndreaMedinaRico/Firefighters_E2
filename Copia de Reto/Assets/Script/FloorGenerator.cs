using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public GameObject prefabTypeOutside; // Prefab para "0"
    public GameObject prefabTypeInside; // Prefab para "1"
    public float cellSize = 1f; // Tamaño de cada celda

    public void GenerateFloor(int[][] floorMatrix)
    {
        // Limpia cualquier objeto generado previamente
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Recorre la matriz y coloca los prefabs
        for (int x = 0; x < floorMatrix.Length; x++)
        {
            for (int y = 0; y < floorMatrix[x].Length; y++)
            {
                // Determina el prefab a usar
                GameObject prefabToInstantiate = floorMatrix[x][y] == 0 ? prefabTypeOutside : prefabTypeInside;

                // Calcula la posición en la escena
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize);

                // Instancia el prefab en la posición calculada
                GameObject instance = Instantiate(prefabToInstantiate, position, Quaternion.identity);

                // Configura el prefab como hijo del objeto contenedor
                instance.transform.parent = this.transform;
            }
        }
    }
}
