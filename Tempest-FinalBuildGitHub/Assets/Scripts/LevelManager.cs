using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
                if (instance == null)
                {
                    GameObject ob = new GameObject();
                    ob.hideFlags = HideFlags.HideAndDontSave;
                    instance = ob.AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }

    private List<GameObject> allEnemies;

    [SerializeField]
    private GameObject mapManagerObject;
    private MapManager mapManager;

    [SerializeField]
    private GameObject enemySpawnerObject;
    private EnemySpawner enemySpawner;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject playerObject;
    private PlayerController playerController;

    [SerializeField]
    private GameObject canvasObject;
    private CanvasManager canvasManager;

    [SerializeField]
    private GameObject enemyHolder;

    private int currentLevel = 0;
    private int score = 0;
    private int lives = 5;
    private bool levelStarted = false;
    public int enemyCount = 0;

    public bool canPlayerMove = true;

    public void AddEnemy(GameObject enemy)
    {
        allEnemies.Add(enemy);
    }

    public void Start()
    {
        mapManager = mapManagerObject.GetComponent<MapManager>();
        enemySpawner = enemySpawnerObject.GetComponent<EnemySpawner>();
        playerController = playerObject.GetComponent<PlayerController>();
        canvasManager = canvasObject.GetComponent<CanvasManager>();

        LoadLevel(currentLevel);
        SpawnEnemies();
    }

    private void Update()
    {
        if (enemyCount <= 0 && levelStarted) {
            levelStarted = false;
            LoadNextLevel();
        }
    }

    public void ClearEnemies() {
        enemyCount = 0;
        foreach (Transform enemy in enemyHolder.transform)
        {
            Destroy(enemy.gameObject);
        }
    }

    public void ClearPlane() {
        foreach (Transform tail in mapManager.transform)
        {
            if (tail.gameObject.tag != "Player") {
                Destroy(tail.gameObject);
            }
        }
    }

    public void RestartLevel() {
        lives -= 1;
        if (lives <= 0) {
            //SoundManager.instance.TransitionEnding();
            PlayerPrefs.SetInt("FinalScore", score);
            SceneManager.LoadScene("GameOver");
        }

        canvasManager.SetLife(lives);

        ClearPlane();
        ClearEnemies();
        LoadLevel(currentLevel);
        SpawnEnemies();
    }

    private void LoadNextLevel() {
        StartCoroutine(LoadNext());
    }

    public IEnumerator LoadNext() {
        yield return new WaitForSeconds(1f);

        Vector3 location = mapManager.transform.position;
        Vector3 newLocation = mapManager.transform.position + Vector3.forward * 80f;

        canPlayerMove = false;

        if (mapManager.spikeMap.Count != 0 && mapManager.spikeMap[playerController.objectLocation]) {
            yield return StartCoroutine(mapManager.spikes[playerController.objectLocation].GetComponent<SpikerEnemyController>().FullyElongateTail(0.6f));

            RestartLevel();
            canPlayerMove = true;

            yield break;
        }

        yield return StartCoroutine(Move(newLocation, 0.5f));

        currentLevel += 1;
        canvasManager.SetLife(lives);
        ClearPlane();
        ClearEnemies();
        LoadLevel(currentLevel);

        playerObject.transform.parent = mapManagerObject.transform;
        yield return StartCoroutine(Move(location, 0.2f));
        canPlayerMove = true;
        SpawnEnemies();
    }

    private void LoadLevel(int level)
    {
        mapManager.LoadNewMap(level);
    }

    private List<IEnumerator> enumerators;

    private void SpawnEnemies()
    {
        playerController.objectLocation = 0;
        playerController.MoveTo(mapManager.GetPlaneTransform(0));

        for (int i = 0; i < 2; i++) {
            enemySpawner.SpawnHopper();
        }

        for (int i = 0; i < 3; i++) {
            enemySpawner.SpawnTank();
        }

        if (currentLevel > 1) {
            enemySpawner.SpawnSpikers();
        }

        if (currentLevel > 2) {
            for (int i = 0; i < currentLevel / 2; i++) {
                enemySpawner.SpawnStraight();
            }
        }

        levelStarted = true;
    }

    public IEnumerator Move(Vector3 endLocation, float duration)
    {
        float time = 0;
        Vector3 startPos = mapManager.transform.position;

        while (time < duration)
        {
            mapManager.transform.position = Vector3.Lerp(startPos, endLocation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        mapManager.transform.position = endLocation;
    }

    public void UpdateScore(int amount) {
        score += amount;
        scoreText.text = score + "";
    }
}
