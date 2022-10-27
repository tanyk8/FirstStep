using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{

    [SerializeField] GameObject newGameBtn;
    [SerializeField] GameObject loadGameBtn;
    [SerializeField] GameObject settingsBtn;
    [SerializeField] GameObject quitGameBtn;

    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject musicOnBtn;

    [SerializeField] GameObject loadTitleListRef;
    [SerializeField] GameObject loadPanel;


    private void Start()
    {
        StartCoroutine(ListLayout.selectOption(newGameBtn));

        SoundManager.GetInstance().playmuzic();

        Button backbtn = SoundManager.GetInstance().backsettingbtn;

        backbtn.onClick.RemoveAllListeners();
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            backbtn.onClick.AddListener(onSettingBack);
        }


    }

    private void Update()
    {
        if (InputManager.getInstance().getMenuPressed())
        {
            if (settingsMenu.activeInHierarchy)
            {
                settingsMenu.SetActive(false);
                StartCoroutine(ListLayout.selectOption(settingsBtn));
            }
            else if (loadPanel.activeInHierarchy)
            {
                loadPanel.SetActive(false);
                StartCoroutine(ListLayout.selectOption(loadGameBtn));
            }
        }
    }

    public void onStartGameBtn()
    {
        SceneManager.LoadScene("Loading");
    }

    public void onLoadGameBtn()
    {
        //createTitleLoadList
        loadPanel.SetActive(true);
        loadTitleListRef.GetComponent<ListLayout>().createTitleLoadList();
    }

    public void onBackLoadBtn()
    {
        loadTitleListRef.GetComponent<ListLayout>().destroyListSelection();
        loadPanel.SetActive(false);
    }

    public void onSettingsBtn()
    {
        settingsMenu.SetActive(true);
        SoundManager.GetInstance().updateUI();
        StartCoroutine(ListLayout.selectOption(musicOnBtn));
    }

    public void onSettingBack()
    {
        if (settingsMenu.activeInHierarchy)
        {
            //revert settings
            SoundManager.GetInstance().revertValue();
            settingsMenu.SetActive(false);
            StartCoroutine(ListLayout.selectOption(settingsBtn));
        }
    }

    public void onExitGameBtn()
    {
        Application.Quit();
    }
}
