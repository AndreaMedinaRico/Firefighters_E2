using UnityEngine;
using TMPro; // Asegúrate de tener TextMeshPro importado
using static WebClient;

public class TextMeshProManager : MonoBehaviour
{
    public TextMeshProUGUI gamePhaseText; // Texto para mostrar la fase del juego
    public TextMeshProUGUI agentInfoText; // Texto para mostrar información de agentes
    public TextMeshProUGUI poiText; // Texto para mostrar información de POIs
    public TextMeshProUGUI falseAlarmsText; // Texto para mostrar las falsas alarmas
    public TextMeshProUGUI lostVictimsText; // Texto para mostrar las víctimas perdidas
    public TextMeshProUGUI totalVictimsText; // Texto para mostrar las víctimas totales
    public TextMeshProUGUI rescuedVictimsText; // Texto para mostrar las víctimas rescatadas
    public TextMeshProUGUI damageCounterText;

    public void UpdateGamePhase(string gamePhase)
    {
        // Actualiza el texto de la fase del juego
        if (gamePhaseText != null)
        {
            gamePhaseText.text = $"Game Phase: {gamePhase}";
        }
    }

    public void UpdateAgentInfo(Agent[] agents)
    {
        if (agentInfoText != null)
        {
            // Construir información de agentes
            string info = "Agents:\n";
            foreach (Agent agent in agents)
            {
                info += $"ID: {agent.id}, Pos: ({agent.position.x}, {agent.position.y}), " +
                        $"Action: {agent.attributes.action_made}\n";
            }

            // Actualizar el texto
            agentInfoText.text = info;
        }
    }

    public void UpdatePOIInfo(int pointOfInterest, int falseAlarms, int lostVictims, int totalVictims, int rescuedVictims, int damageCounter)
    {
        if (poiText != null)
        {
            poiText.text = $"POIs: {pointOfInterest}";
        }
        if (falseAlarmsText != null)
        {
            falseAlarmsText.text = $"False Alarms: {falseAlarms}";
        }
        if (lostVictimsText != null)
        {
            lostVictimsText.text = $"Lost Victims: {lostVictims}";
        }
        if (totalVictimsText != null)
        {
            totalVictimsText.text = $"Total Victims: {totalVictims}";
        }
        if (rescuedVictimsText != null)
        {
            rescuedVictimsText.text = $"Rescued Victims: {rescuedVictims}";
        }
        if (damageCounterText != null)
        {
            damageCounterText.text = $"Damage Counter: {damageCounter}";
        }
    }
}
