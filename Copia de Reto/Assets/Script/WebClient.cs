// TC2008B Modelación de Sistemas Multiagentes con gráficas computacionales
// C# client to interact with Python server via POST
// Sergio Ruiz-Loza, Ph.D. March 2021

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Unity.VisualScripting;


public class WebClient : MonoBehaviour
{

    private bool start = false;
    private float time = 0.5f;

    [System.Serializable]
    public class Agent
    {
        public int id; // ID único del agente
        public Position position; // Posición del agente
        public Attributes attributes; // Atributos adicionales del agente
    }

    [System.Serializable]
    public class Position
    {
        public int x; // Coordenada X
        public int y; // Coordenada Y
    }

    [System.Serializable]
    public class Attributes
    {
        public bool carrying_victim; // Si está cargando una víctima
        public bool knocked_down; // Si está derribado
        public string action_made;
    }

    [System.Serializable]
    public class ModelState
    {
        public int steps;
        public string game_phase;
        public int active_player;
        public int damage_counters;
        public int false_alarms;
        public int lost_victims;
        public int saved_victims;
        public int total_victims;
        public int point_of_interest;
        public float[][] grid_status;
        public float[][][] walls;
        public int[][] floor;
        public float[][][] doors;
        public float[][] poi;
        public Agent[] agents; // Lista de agentes
        public int active_agent;
        public int[][] explosions;
    }


    public ModelState stateOfGame;

    // IEnumerator - yield return
    IEnumerator SendData()
    {
        string url = "http://localhost:8585";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return www.SendWebRequest(); // Talk to Python

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Parse JSON response into stateOfGame
                stateOfGame = JsonConvert.DeserializeObject<ModelState>(www.downloadHandler.text);
            }
        }

    }

    IEnumerator SetGameStatus()
    {
        yield return StartCoroutine(SendData());
                     StartCoroutine(SetFloor());
                     StartCoroutine(SetWalls());
                     StartCoroutine(SetDoors());
                     StartCoroutine(SetFireSmoke());
                     StartCoroutine(SetPOIs());
                     StartCoroutine(SetAgents());
                     StartCoroutine(SetTextInfo());
                     StartCoroutine(SetExplosions());

    }

    IEnumerator SetFloor()
    {
        GameObject floorManager = GameObject.Find("FloorManager");
        FloorGenerator generator = floorManager.GetComponent<FloorGenerator>();
        generator.GenerateFloor(stateOfGame.floor);
        yield return null;
    }

    IEnumerator SetWalls()
    {
        GameObject wallManager = GameObject.Find("WallManager");
        WallGenerator generator = wallManager.GetComponent<WallGenerator>();
        generator.GenerateWalls(stateOfGame.walls);
        yield return null;
    }

    IEnumerator SetDoors()
    {
        GameObject doorManager = GameObject.Find("DoorManager");
        DoorGenerator generator = doorManager.GetComponent<DoorGenerator>();
        generator.GenerateDoors(stateOfGame.doors);
        yield return null;
    }

    IEnumerator SetFireSmoke()
    {
        GameObject fireSmokeManager = GameObject.Find("FireSmokeManager");
        FireSmokeGenerator generator = fireSmokeManager.GetComponent<FireSmokeGenerator>();
        generator.GenerateFireSmoke(stateOfGame.grid_status);
        yield break;
    }

    IEnumerator SetPOIs()
    {
        GameObject poiManager = GameObject.Find("POIManager");
        POIGenerator generator = poiManager.GetComponent<POIGenerator>();
        generator.GeneratePOIs(stateOfGame.poi);
        yield break;
    }

    IEnumerator SetAgents()
    {
        GameObject agentManager = GameObject.Find("AgentManager");
        AgentGenerator generator = agentManager.GetComponent<AgentGenerator>();
        generator.GenerateAgents(stateOfGame.agents, stateOfGame.active_agent);
        yield break;
    }

    IEnumerator SetTextInfo()
    {
        GameObject textManagerObject = GameObject.Find("TextMeshProManager");
        TextMeshProManager textManager = textManagerObject.GetComponent<TextMeshProManager>();
        textManager.UpdateGamePhase(stateOfGame.game_phase);
        textManager.UpdateAgentInfo(stateOfGame.agents);
        textManager.UpdatePOIInfo(
            stateOfGame.point_of_interest,
            stateOfGame.false_alarms, stateOfGame.lost_victims,
            stateOfGame.total_victims,
            stateOfGame.saved_victims,
            stateOfGame.damage_counters
            );

        yield break;
    }

    IEnumerator SetExplosions()
    {
        GameObject explosionManagerObject = GameObject.Find("ExplosionManager");
        ExplosionManager explosionManager = explosionManagerObject.GetComponent<ExplosionManager>();
        explosionManager.GenerateExplosions(stateOfGame.explosions);
        yield return null;
    }







    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetGameStatus());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && start == false)
        {

            start = true;
        }

        if (start)
        {
            if(time < 0)
            {
                StartCoroutine(SetGameStatus());
                time = 0.5f;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }



}