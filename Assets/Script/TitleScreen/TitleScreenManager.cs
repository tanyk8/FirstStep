using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] AudioClip submitSE;
    [SerializeField] AudioClip cancelSE;

    [SerializeField] GameObject newGameBtn;
    [SerializeField] GameObject loadGameBtn;
    [SerializeField] GameObject settingsBtn;
    [SerializeField] GameObject quitGameBtn;

    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject musicOnBtn;

    [SerializeField] GameObject loadTitleListRef;
    [SerializeField] GameObject loadPanel;

    bool playonce=true;

    private void Start()
    {
        StartCoroutine(ListLayout.selectOption(newGameBtn));


        Button backbtn = SoundManager.GetInstance().backsettingbtn;

        backbtn.onClick.RemoveAllListeners();
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            backbtn.onClick.AddListener(onSettingBack);
            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_titlescreen"));
        }


    }

    private void Update()
    {
        if (InputManager.getInstance().getMenuPressed())
        {
            if (settingsMenu.activeInHierarchy)
            {
                SoundManager.GetInstance().playSEMenu(cancelSE);
                settingsMenu.SetActive(false);
                StartCoroutine(ListLayout.selectOption(settingsBtn));
            }
            else if (loadPanel.activeInHierarchy)
            {
                SoundManager.GetInstance().playSEMenu(cancelSE);
                loadPanel.SetActive(false);
                StartCoroutine(ListLayout.selectOption(loadGameBtn));
            }
        }
    }

    public void onStartGameBtn()
    {
        SoundManager.GetInstance().playSEMenu(submitSE);
        SoundManager.GetInstance().musicSource.Stop();
        SceneManager.LoadScene("Loading");
    }

    public void onLoadGameBtn()
    {
        //createTitleLoadList
        SoundManager.GetInstance().playSEMenu(submitSE);
        loadPanel.SetActive(true);
        loadTitleListRef.GetComponent<ListLayout>().createTitleLoadList();
    }

    public void onBackLoadBtn()
    {
        SoundManager.GetInstance().playSEMenu(cancelSE);
        loadTitleListRef.GetComponent<ListLayout>().destroyListSelection();
        loadPanel.SetActive(false);
        StartCoroutine(ListLayout.selectOption(loadGameBtn));
    }

    public void onSettingsBtn()
    {
        SoundManager.GetInstance().playSEMenu(submitSE);
        settingsMenu.SetActive(true);
        SoundManager.GetInstance().updateUI();
        StartCoroutine(ListLayout.selectOption(musicOnBtn));
    }

    public void onSettingBack()
    {
        if (settingsMenu.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(cancelSE);
            //revert settings
            SoundManager.GetInstance().revertValue();
            settingsMenu.SetActive(false);
            StartCoroutine(ListLayout.selectOption(settingsBtn));
        }
    }

    public void onExitGameBtn()
    {
        SoundManager.GetInstance().playSEMenu(submitSE);
        Application.Quit();
    }
}
