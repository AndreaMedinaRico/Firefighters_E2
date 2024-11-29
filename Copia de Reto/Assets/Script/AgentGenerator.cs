using static WebClient;
using UnityEngine;

public class AgentGenerator : MonoBehaviour
{
    public GameObject agentPrefab; // Prefab para un agente normal
    public GameObject activeAgentPrefab; // Prefab para el agente activo
    public GameObject carryingVictimPrefab; // Prefab para el agente cargando una víctima
    public GameObject activeCarryingVictimPrefab; // Prefab para el agente activo cargando una víctima
    public float cellSize = 3f; // Tamaño de cada celda

    public void GenerateAgents(Agent[] agents, int activeAgentId)
    {
        // Limpia los agentes existentes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Instancia los agentes en sus posiciones
        foreach (Agent agent in agents)
        {
            // Determinar el prefab a usar
            GameObject prefabToInstantiate = null;

            if (agent.id == activeAgentId && agent.attributes.carrying_victim)
            {
                prefabToInstantiate = activeCarryingVictimPrefab; // Agente activo cargando una víctima
            }
            else if (agent.id == activeAgentId)
            {
                prefabToInstantiate = activeAgentPrefab; // Agente activo
            }
            else if (agent.attributes.carrying_victim)
            {
                prefabToInstantiate = carryingVictimPrefab; // Agente cargando una víctima
            }
            else
            {
                prefabToInstantiate = agentPrefab; // Agente normal
            }

            // Si no hay prefab asignado, saltar este agente
            if (prefabToInstantiate == null) continue;

            // Calcular la posición del agente
            Vector3 position = new Vector3(agent.position.x * cellSize, 0, agent.position.y * cellSize);

            // Instanciar el agente
            GameObject newAgent = Instantiate(prefabToInstantiate, position, Quaternion.identity, this.transform);
        }
    }
}
