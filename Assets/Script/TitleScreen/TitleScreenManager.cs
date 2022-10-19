using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

    [SerializeField] GameObject newGameBtn;
    [SerializeField] GameObject loadGameBtn;
    [SerializeField] GameObject settingsBtn;
    [SerializeField] GameObject quitGameBtn;


    private void Start()
    {
        StartCoroutine(ListLayout.selectOption(newGameBtn));
    }

    public void onStartGameBtn()
    {
        SceneManager.LoadScene("Loading");
    }
}
