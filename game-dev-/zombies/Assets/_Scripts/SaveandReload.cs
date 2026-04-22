using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using System; 
using System.Xml;

[Serializable]
public struct PlayerRecord {
    public Vector3 position;
    public float hp;
    public int fuel;
    public PlayerRecord(Vector3 pos, float health, int f) {
        position = pos; hp = health; fuel = f;
    }
}

[Serializable]
public struct ZombieRecord {
    public Vector3 position;
    public int zombieType; 
    public ZombieRecord(Vector3 pos, int type) {
        position = pos; zombieType = type;
    }
}

[Serializable]
public struct GameState {
    public int sceneIndex;
    public int currentScore;
    public PlayerRecord playerState;
    public ZombieRecord[] zombieStates;
}

public class SaveandReload : MonoBehaviour {
    public GameObject zombiePrefab;
    public GameObject patrolZombiePrefab;
    private string savePath;
    private static SaveandReload instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
          
            string folderPath = Application.dataPath + "/Saves";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            savePath = folderPath + "/savegame.xml"; 
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) Save();
        if (Input.GetKeyDown(KeyCode.F2)) Load();
    }

    public void Save() {
        PlayerManager pm = FindObjectOfType<PlayerManager>();
        if (pm == null) return;

        PlayerRecord pRec = new PlayerRecord(pm.transform.position, pm.hp, pm.fuelCount);

        List<ZombieRecord> zRecs = new List<ZombieRecord>();
        foreach (GameObject z in GameObject.FindGameObjectsWithTag("Zombie"))
            zRecs.Add(new ZombieRecord(z.transform.position, 0));
        foreach (GameObject pz in GameObject.FindGameObjectsWithTag("PatrolZombie"))
            zRecs.Add(new ZombieRecord(pz.transform.position, 1));

        GameState gs = new GameState {
            sceneIndex = SceneManager.GetActiveScene().buildIndex,
            currentScore = ScoreManager.scoreValue,
            playerState = pRec,
            zombieStates = zRecs.ToArray()
        };

        XmlSerializer serializer = new XmlSerializer(typeof(GameState));
        XmlDocument xmlDocument = new XmlDocument();

        using (MemoryStream stream = new MemoryStream()) {
            serializer.Serialize(stream, gs);
            stream.Position = 0;
            xmlDocument.Load(stream);
            xmlDocument.Save(savePath);
        }
        Debug.Log("Game Saved to: " + savePath);
    }

    public void LoadFromMenu() { Load(); }

    public void Load() {
        if (!File.Exists(savePath)) return;

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(savePath);
        string xmlString = xmlDocument.OuterXml;

        GameState gs;
        using (StringReader read = new StringReader(xmlString)) {
            XmlSerializer serializer = new XmlSerializer(typeof(GameState));
            using (XmlReader reader = new XmlTextReader(read)) {
                gs = (GameState)serializer.Deserialize(reader);
            }
        }
        StartCoroutine(RestoreGame(gs));
    }

    private System.Collections.IEnumerator RestoreGame(GameState gs) {
        
        if (SceneManager.GetActiveScene().buildIndex != gs.sceneIndex) {
            SceneManager.LoadScene(gs.sceneIndex);
            while (SceneManager.GetActiveScene().buildIndex != gs.sceneIndex) yield return null;
            yield return new WaitForSeconds(0.2f); 
        }

        PlayerManager pm = FindObjectOfType<PlayerManager>();
        if (pm != null) {
            CharacterController cc = pm.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false; 
            
            pm.transform.position = gs.playerState.position;
            pm.hp = gs.playerState.hp;
            pm.fuelCount = gs.playerState.fuel;
            
            ScoreManager.scoreValue = gs.currentScore;
            ScoreManager sm = FindObjectOfType<ScoreManager>();
            if (sm != null) sm.UpdateScoreUI();
            
            pm.UpdateFuelUI(); 

            if (cc != null) cc.enabled = true;
        }

        ClearAllZombies();
        
        GameObject[] fuelsOnMap = GameObject.FindGameObjectsWithTag("Items");
        for (int i = 0; i < gs.playerState.fuel; i++) {
            if (i < fuelsOnMap.Length) Destroy(fuelsOnMap[i]);
        }

        foreach (ZombieRecord zr in gs.zombieStates) {
            GameObject prefab = (zr.zombieType == 1) ? patrolZombiePrefab : zombiePrefab;
            if (prefab != null) {
                Instantiate(prefab, zr.position, Quaternion.identity);
            }
        }
    }

    void ClearAllZombies() {
        foreach (GameObject z in GameObject.FindGameObjectsWithTag("Zombie")) Destroy(z);
        foreach (GameObject pz in GameObject.FindGameObjectsWithTag("PatrolZombie")) Destroy(pz);
    }
    
}