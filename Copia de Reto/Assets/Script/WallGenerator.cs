using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject completeWallPrefab; // Prefab para pared completa (1)
    public GameObject damagedWallPrefab; // Prefab para pared dañada (0.5)
    public float cellSize = 3f; // Tamaño de cada celda

    public void GenerateWalls(float[][][] wallsMatrix)
    {

        // Limpia paredes existentes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Recorre la matriz de paredes
        for (int x = 0; x < wallsMatrix.Length; x++)
        {
            for (int y = 0; y < wallsMatrix[x].Length; y++)
            {
                float[] cellWalls = wallsMatrix[x][y];

                // Revisar cada dirección: [arriba, izquierda, abajo, derecha]
                for (int direction = 0; direction < 4; direction++)
                {
                    float wallState = cellWalls[direction];

                    // Si no hay pared, no hacemos nada
                    if (wallState == 0) continue;

                    // Determinar el prefab a usar
                    GameObject prefabToInstantiate = null;
                    if (wallState == 1)
                    {
                        prefabToInstantiate = completeWallPrefab; // Pared completa
                    }
                    else if (wallState == 0.5f)
                    {
                        prefabToInstantiate = damagedWallPrefab; // Pared dañada
                    }

                    // Si no hay prefab asignado, saltar esta dirección
                    if (prefabToInstantiate == null) continue;

                    // Calcular la posición y rotación de la pared
                    Vector3 position = GetWallPosition(x, y, direction);
                    Quaternion rotation = GetWallRotation(direction);

                    // Instanciar la pared
                    Instantiate(prefabToInstantiate, position, rotation, this.transform);
                }
            }
        }
    }

    private Vector3 GetWallPosition(int x, int y, int direction)
    {
        // Calcula la posición de la pared basada en la dirección
        Vector3 basePosition = new Vector3(x * cellSize, 0, y * cellSize);

        switch (direction)
        {
            case 0: // Arriba
                return basePosition + new Vector3(-cellSize / 2, 0, 0);
            case 1: // Izquierda
                return basePosition + new Vector3(0, 0, -cellSize / 2);
            case 2: // Abajo
                return basePosition + new Vector3(cellSize / 2, 0, 0);
            case 3: // Derecha
                return basePosition + new Vector3(0, 0, cellSize / 2);
            default:
                return basePosition;
        }
    }

    private Quaternion GetWallRotation(int direction)
    {
        // Devuelve la rotación correcta basada en la dirección
        switch (direction)
        {
            case 0: // Arriba
            case 2: // Abajo
                return Quaternion.Euler(0, 0, 0); // Sin rotación
            case 1: // Izquierda
            case 3: // Derecha
                return Quaternion.Euler(0, 90, 0); // Rotación 90 grados
            default:
                return Quaternion.identity;
        }
    }
}
