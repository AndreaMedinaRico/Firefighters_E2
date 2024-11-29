using UnityEngine;

public class POIGenerator : MonoBehaviour
{
    public GameObject poiHiddenPrefab; // Prefab para POI oculto (1)
    public GameObject victimPrefab; // Prefab para víctima (2)
    public GameObject falseAlarmPrefab; // Prefab para falsa alarma (3)
    public float cellSize = 3f; // Tamaño de cada celda

    public void GeneratePOIs(float[][] poiMatrix)
    {

        // Limpia los POIs existentes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Recorre la matriz de POIs
        for (int x = 0; x < poiMatrix.Length; x++)
        {
            for (int y = 0; y < poiMatrix[x].Length; y++)
            {
                float poiState = poiMatrix[x][y];

                // Si no hay POI, no hacemos nada
                if (poiState == 0) continue;

                // Determinar el prefab a usar
                GameObject prefabToInstantiate = null;
                if (poiState == 1)
                {
                    prefabToInstantiate = poiHiddenPrefab; // POI oculto
                }
                else if (poiState == 2)
                {
                    prefabToInstantiate = victimPrefab; // Víctima
                }
                else if (poiState == 3)
                {
                    prefabToInstantiate = falseAlarmPrefab; // Falsa alarma
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
        // Calcula la posición del POI basado en la celda
        return new Vector3(x * cellSize, 0, y * cellSize);
    }
}
