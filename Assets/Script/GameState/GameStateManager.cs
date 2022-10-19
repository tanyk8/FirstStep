using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;

    public string lastscene;
    public string lastentrance;
    public string enemy;
    public Vector3 position;
    public bool updateOnce;
    public bool battleRun;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        SceneManager.sceneUnloaded += updateLastScene;
        if (SceneManager.GetActiveScene().name.ToString()== "Battlescene")
        {
            BattleManager battlemanager= GameObject.Find("BattleManager").GetComponent<BattleManager>();
            if (enemy == "firststage")
            {
                battlemanager.enemyPrefab = battlemanager.enemyPrefabArray[0];
            }
        }
    }

    public static GameStateManager GetInstance()
    {
        return instance;
    }

    public void updateLastScene(Scene current)
    {
        lastscene = current.name;
        //if (GameObject.Find("Player")!=null)
        //{
        //    position = GameObject.Find("Player").transform.position;
        //    Debug.Log(position);
        //}
        
        updateOnce = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= updateLastScene;
    }
}
