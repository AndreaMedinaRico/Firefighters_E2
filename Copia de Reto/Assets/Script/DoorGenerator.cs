using UnityEngine;

public class DoorGenerator : MonoBehaviour
{
    public GameObject openDoorPrefab; // Prefab para puerta abierta (1)
    public GameObject closedDoorPrefab; // Prefab para puerta cerrada (2)
    public float cellSize = 3f; // Tamaño de cada celda

    public void GenerateDoors(float[][][] doorsMatrix)
    {

        // Limpia puertas existentes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Recorre la matriz de puertas
        for (int x = 0; x < doorsMatrix.Length; x++)
        {
            for (int y = 0; y < doorsMatrix[x].Length; y++)
            {
                float[] cellDoors = doorsMatrix[x][y];

                // Revisar cada dirección: [arriba, izquierda, abajo, derecha]
                for (int direction = 0; direction < 4; direction++)
                {
                    float doorState = cellDoors[direction];

                    // Si no hay puerta, no hacemos nada
                    if (doorState == 0) continue;

                    // Determinar el prefab a usar
                    GameObject prefabToInstantiate = null;
                    if (doorState == 1)
                    {
                        prefabToInstantiate = openDoorPrefab; // Puerta abierta
                    }
                    else if (doorState == 2)
                    {
                        prefabToInstantiate = closedDoorPrefab; // Puerta cerrada
                    }

                    // Si no hay prefab asignado, saltar esta dirección
                    if (prefabToInstantiate == null) continue;

                    // Calcular la posición y rotación de la puerta
                    Vector3 position = GetDoorPosition(x, y, direction);
                    Quaternion rotation = GetDoorRotation(direction);

                    // Instanciar la puerta
                    Instantiate(prefabToInstantiate, position, rotation, this.transform);
                }
            }
        }
    }

    private Vector3 GetDoorPosition(int x, int y, int direction)
    {
        // Copia la lógica de WallGenerator
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

    private Quaternion GetDoorRotation(int direction)
    {
        // Copia la lógica de WallGenerator
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
