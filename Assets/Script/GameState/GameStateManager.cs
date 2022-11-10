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
        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
        }
    }

    public static GameStateManager GetInstance()
    {
        return instance;
    }

    public void updateLastScene(Scene current)
    {
        lastscene = current.name;
        
        updateOnce = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= updateLastScene;
    }
}
