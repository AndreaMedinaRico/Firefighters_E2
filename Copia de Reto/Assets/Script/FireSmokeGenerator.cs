using UnityEngine;

public class FireSmokeGenerator : MonoBehaviour
{
    public GameObject smokePrefab; // Prefab para humo (1)
    public GameObject firePrefab; // Prefab para fuego (2)
    public float cellSize = 3f; // Tamaño de cada celda

    public void GenerateFireSmoke(float[][] gridStatus)
    {

        // Limpia elementos existentes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Recorre la matriz de grid_status
        for (int x = 0; x < gridStatus.Length; x++)
        {
            for (int y = 0; y < gridStatus[x].Length; y++)
            {
                float cellState = gridStatus[x][y];

                // Si es 0, no hacemos nada
                if (cellState == 0) continue;

                // Determinar el prefab a usar
                GameObject prefabToInstantiate = null;
                if (cellState == 1)
                {
                    prefabToInstantiate = smokePrefab; // Humo
                }
                else if (cellState == 2)
                {
                    prefabToInstantiate = firePrefab; // Fuego
                }

                // Si no hay prefab asignado, saltar
                if (prefabToInstantiate == null) continue;

                // Calcular la posición
                Vector3 position = GetGridPosition(x, y);

                // Instanciar el prefab
                Instantiate(prefabToInstantiate, position, Quaternion.identity, this.transform);
            }
        }
    }

    private Vector3 GetGridPosition(int x, int y)
    {
        // Calcula la posición del objeto basado en la celda
        return new Vector3(x * cellSize, 0, y * cellSize);
    }
}
