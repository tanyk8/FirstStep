using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject eventsys;
    [SerializeField] AudioClip submitSE;
    [SerializeField] AudioClip cancelSE;

    [Header("ContentPanel")]
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject saveloadPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject returntotitlePanel;

    [Header("Content")]
    [SerializeField] GameObject overlay;

    [Header("Button")]
    [SerializeField] GameObject characterBtn;
    [SerializeField] GameObject inventoryBtn;
    [SerializeField] GameObject questBtn;
    [SerializeField] GameObject saveloadBtn;
    [SerializeField] GameObject settingsBtn;
    [SerializeField] GameObject returntoTitleBtn;

    [Header("ContentBtn")]
    [SerializeField] GameObject returntoTitle_NoBtn;

    [Header("Character")]
    [SerializeField] TextMeshProUGUI powerValue;
    [SerializeField] TextMeshProUGUI protectionValue;
    [SerializeField] private Slider slider_hp;
    [SerializeField] private TextMeshProUGUI hp_text;
    [SerializeField] private Slider slider_mp;
    [SerializeField] private TextMeshProUGUI mp_text;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] Player player;

    [SerializeField] GameObject characterContentPanel;
    [SerializeField] GameObject characterAboutPanel;
    [SerializeField] GameObject characterLeftPanel;
    [SerializeField] GameObject characterRightPanel;
    [SerializeField] GameObject characterEmptyMsg;

    [SerializeField] GameObject characterAboutBtn;
    [SerializeField] GameObject characterSkillBtn;
    [SerializeField] GameObject characterStatusBtn;

    [SerializeField] GameObject skillListRef;
    [SerializeField] Transform skillListT;

    [Header("Inventory")]
    [SerializeField] GameObject inventoryContentPanel;
    [SerializeField] GameObject inventoryLeftPanel;
    [SerializeField] GameObject inventoryRightPanel;
    [SerializeField] GameObject inventoryEmptyMsg;

    [SerializeField] GameObject inventoryListRef;
    [SerializeField] Transform inventoryListT;

    [SerializeField] GameObject inventoryAllBtn;
    [SerializeField] GameObject inventoryUseBtn;
    [SerializeField] GameObject inventoryETCBtn;
    [SerializeField] GameObject inventoryImportantBtn;

    [SerializeField] GameObject inventoryUseItemBtn;

    [Header("Quest")]
    [SerializeField] GameObject inprogQuestBtn;
    [SerializeField] GameObject completedQuestBtn;
    [SerializeField] GameObject questListPanel;
    [SerializeField] GameObject questInfoPanel;
    [SerializeField] GameObject questListRef;
    [SerializeField] GameObject emptyQuestMsg;
    [SerializeField] GameObject questContentPanel;
    [SerializeField] Transform questListT;

    [SerializeField] GameObject scrollbarq;
    [SerializeField] GameObject scrollareaq;

    [Header("SaveLoad")]
    [SerializeField] GameObject saveloadContentPanel;
    [SerializeField] GameObject saveloadListRef;
    [SerializeField] GameObject saveBtn;
    [SerializeField] GameObject loadBtn;

    private static MenuManager instance;

    public string lastSelectedQuestBtn="";
    public string lastSelectedCharacterBtn="";
    public string lastSelectedInventoryBtn="";
    public string lastSelectedSaveLoadBtn = "";

    public string lastSelectedSkill = "";
    public int lastSelectedSkillIndex = 0;

    public string lastSelectedStatus = "";
    public int lastSelectedStatusIndex = 0;

    public string lastSelectedItem = "";
    public int lastSelectedItemIndex = 0;

    public string lastSelectedQuest = "";
    public int lastSelectedQuestIndex = 0;

    public bool menuIsOpened { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(menuCanvas);
            //DontDestroyOnLoad(eventsys);
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static MenuManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        Button backbtn = SoundManager.GetInstance().backsettingbtn;

        backbtn.onClick.RemoveAllListeners();
        if (SceneManager.GetActiveScene().name != "TitleScreen")
        {
            backbtn.onClick.AddListener(closeSettingsBtn);
        }

        menuCanvas.SetActive(false);
        overlay.SetActive(false);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
            Destroy(menuCanvas);
        }
        if (SceneManager.GetActiveScene().name != "Battlescene"&& InputManager.GetInstance().getMenuPressed())
        {
            if (!TimelineManager.GetInstance().dontmove&&!menuCanvas.activeInHierarchy&& !DialogueManager.GetInstance().dialogueIsPlaying&& TimelineManager.GetInstance().getPlayState() != PlayState.Playing)
            {
                SoundManager.GetInstance().playSEMenu(submitSE);
                menuIsOpened = true;
                menuCanvas.SetActive(true);
                StartCoroutine(ListLayout.selectOption(characterBtn));
            }
            else if (lastSelectedCharacterBtn!=""&&characterContentPanel.activeInHierarchy)
            {
                if (characterRightPanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    characterRightPanel.SetActive(false);
                    if (lastSelectedCharacterBtn == "skill")
                    {
                        StartCoroutine(ListLayout.selectOption(skillListT.GetChild(lastSelectedSkillIndex).gameObject));
                    }
                    
                    else if (lastSelectedCharacterBtn == "status")
                    {
                        StartCoroutine(ListLayout.selectOption(skillListT.GetChild(lastSelectedStatusIndex).gameObject));
                    }
                }
                else
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    characterEmptyMsg.SetActive(false);
                    characterAboutPanel.SetActive(false);
                    characterLeftPanel.SetActive(false);
                    characterRightPanel.SetActive(false);
                    characterContentPanel.SetActive(false);
                    skillListRef.GetComponent<ListLayout>().destroyListSelection();

                    if (lastSelectedCharacterBtn == "about")
                    {
                        StartCoroutine(ListLayout.selectOption(characterAboutBtn));
                    }
                    else if (lastSelectedCharacterBtn == "skill")
                    {
                        StartCoroutine(ListLayout.selectOption(characterSkillBtn));
                    }
                    else if (lastSelectedCharacterBtn == "status")
                    {
                        StartCoroutine(ListLayout.selectOption(characterStatusBtn));
                    }
                    lastSelectedCharacterBtn = "";
                }


                
            }
            else if (lastSelectedInventoryBtn != "" && inventoryContentPanel.activeInHierarchy)
            {
                if (inventoryRightPanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    inventoryRightPanel.SetActive(false);
                    if (lastSelectedInventoryBtn == "all")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryListT.GetChild(lastSelectedItemIndex).gameObject));
                    }
                    else if (lastSelectedInventoryBtn == "use")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryListT.GetChild(lastSelectedItemIndex).gameObject));
                    }
                    else if (lastSelectedInventoryBtn == "etc")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryListT.GetChild(lastSelectedItemIndex).gameObject));
                    }
                    else if (lastSelectedInventoryBtn == "important")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryListT.GetChild(lastSelectedItemIndex).gameObject));
                    }
                }
                else
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    inventoryLeftPanel.SetActive(false);
                    inventoryRightPanel.SetActive(false);
                    inventoryEmptyMsg.SetActive(false);
                    inventoryContentPanel.SetActive(false);
                    inventoryListRef.GetComponent<ListLayout>().destroyListSelection();

                    if (lastSelectedInventoryBtn == "all")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryAllBtn));
                    }
                    else if (lastSelectedInventoryBtn == "use")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryUseBtn));
                    }
                    else if (lastSelectedInventoryBtn == "etc")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryETCBtn));
                    }
                    else if (lastSelectedInventoryBtn == "important")
                    {
                        StartCoroutine(ListLayout.selectOption(inventoryImportantBtn));
                    }
                    lastSelectedInventoryBtn = "";
                }
                
            }
            else if (lastSelectedQuestBtn!=""&&questContentPanel.activeInHierarchy)
            {
                if (questInfoPanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    questInfoPanel.SetActive(false);
                    if (lastSelectedQuestBtn == "inprog")
                    {
                        StartCoroutine(ListLayout.selectOption(questListT.GetChild(lastSelectedQuestIndex).gameObject));
                    }
                    else if (lastSelectedQuestBtn== "completed")
                    {
                        StartCoroutine(ListLayout.selectOption(questListT.GetChild(lastSelectedQuestIndex).gameObject));
                    }
                }
                else
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    emptyQuestMsg.SetActive(false);
                    questListPanel.SetActive(false);
                    questInfoPanel.SetActive(false);
                    questContentPanel.SetActive(false);

                    questListRef.GetComponent<ListLayout>().destroyListSelection();
                    if (lastSelectedQuestBtn == "inprog")
                    {
                        StartCoroutine(ListLayout.selectOption(inprogQuestBtn));
                    }
                    else if (lastSelectedQuestBtn == "completed")
                    {
                        StartCoroutine(ListLayout.selectOption(completedQuestBtn));
                    }
                    lastSelectedQuestBtn = "";
                }
            }
            else if (lastSelectedSaveLoadBtn!=""&&saveloadContentPanel.activeInHierarchy)
            {
                SoundManager.GetInstance().playSEMenu(cancelSE);
                saveloadContentPanel.SetActive(false);
                saveloadListRef.GetComponent<ListLayout>().destroyListSelection();
                if (lastSelectedSaveLoadBtn == "save")
                {
                    StartCoroutine(ListLayout.selectOption(saveBtn));
                }
                else if (lastSelectedSaveLoadBtn == "load")
                {
                    StartCoroutine(ListLayout.selectOption(loadBtn));
                }
                lastSelectedSaveLoadBtn = "";
            }

            else if (overlay.activeInHierarchy) {
                GameObject temp = SoundManager.GetInstance().settings;
                if (characterPanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    characterPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectOption(characterBtn));
                }
                else if (inventoryPanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    inventoryPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectOption(inventoryBtn));
                }
                else if (questPanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    questPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectOption(questBtn));
                }
                else if (saveloadPanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    saveloadPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectOption(saveloadBtn));
                }
                else if (temp.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    SoundManager.GetInstance().revertValue();
                    temp.SetActive(false);
                    StartCoroutine(ListLayout.selectOption(settingsBtn));
                }
                else if (returntotitlePanel.activeInHierarchy)
                {
                    SoundManager.GetInstance().playSEMenu(cancelSE);
                    returntotitlePanel.SetActive(false);
                    StartCoroutine(ListLayout.selectOption(returntoTitleBtn));
                }

                overlay.SetActive(false);

            }
            else if(menuCanvas.activeInHierarchy)
            {
                SoundManager.GetInstance().playSEMenu(cancelSE);
                menuCanvas.SetActive(false);
                menuIsOpened = false;
            }
        }

    }

    public void onCloseBtn()
    {
        if (menuCanvas.activeInHierarchy)
        {
            menuCanvas.SetActive(false);
        }
    }

    public void onCharacterBtn()
    {
        if (!characterPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            overlay.SetActive(true);

            slider_hp.maxValue = player.getStat_MaxHealth();
            slider_hp.value = player.getCurrent_Health();
            hp_text.text = player.getCurrent_Health() + "/" + player.getStat_MaxHealth();
            fill.color = gradient.Evaluate(1f);
            slider_mp.maxValue = player.getStat_MaxMentalPoint();
            slider_mp.value = player.getCurrent_MentalPoint();
            mp_text.text = player.getCurrent_MentalPoint() + "/" + player.getStat_MaxMentalPoint();

            powerValue.text =player.getStat_MentalPower().ToString();
            protectionValue.text = player.getStat_MentalProtection().ToString();

            characterPanel.SetActive(true);
            StartCoroutine(ListLayout.selectOption(characterAboutBtn));

        }
    }

    public void onAboutCharacterBtn()
    {
        if (!characterContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            characterContentPanel.SetActive(true);
            characterAboutPanel.SetActive(true);
            lastSelectedCharacterBtn = "about";
        }
        
    }

    public void onSkillCharacterBtn()
    {
        if (!characterContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            characterContentPanel.SetActive(true);

            if (SkillManager.GetInstance().checkSkillEmpty("learnt"))
            {
                characterEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(characterEmptyMsg));
            }
            else
            {
                characterLeftPanel.SetActive(true);
                //characterRightPanel.SetActive(true);
                skillListRef.GetComponent<ListLayout>().createSkillList(SkillManager.GetInstance().skilllist, "learnt");
            }

            lastSelectedCharacterBtn = "skill";
        }
    }

    public void onStatusCharacterBtn()
    {
        if (!characterContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            characterContentPanel.SetActive(true);
            if (StatusManager.GetInstance().checkStatusEmpty())
            {
                characterEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(characterEmptyMsg));
            }
            else
            {
                characterLeftPanel.SetActive(true);
                //characterRightPanel.SetActive(true);
                skillListRef.GetComponent<ListLayout>().createStatusList(StatusManager.GetInstance().statusList);
            }
            
            
            lastSelectedCharacterBtn = "status";
        }
    }

    public void onInventoryBtn()
    {
        if (!inventoryPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            overlay.SetActive(true);
            inventoryPanel.SetActive(true);
            StartCoroutine(ListLayout.selectOption(inventoryAllBtn));
        }
    }

    public void onAllInventoryBtn()
    {
        if (!inventoryContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            inventoryContentPanel.SetActive(true);
            inventoryUseItemBtn.SetActive(false);
            
            if (InventoryManager.GetInstance().checkInventoryEmpty("all"))
            {
                inventoryEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(inventoryEmptyMsg));
            }
            else
            {
                inventoryLeftPanel.SetActive(true);
                //inventoryRightPanel.SetActive(true);
                inventoryListRef.GetComponent<ListLayout>().createInventoryList(InventoryManager.GetInstance().inventory, "all");
            }
            lastSelectedInventoryBtn = "all";
        }
    }

    public void onUseInventoryBtn()
    {
        if (!inventoryContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            inventoryContentPanel.SetActive(true);
            inventoryUseItemBtn.SetActive(false);

            if (InventoryManager.GetInstance().checkInventoryEmpty("use"))
            {
                inventoryEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(inventoryEmptyMsg));
            }
            else
            {
                inventoryLeftPanel.SetActive(true);
                //inventoryRightPanel.SetActive(true);
                //inventoryUseItemBtn.SetActive(true);
                inventoryListRef.GetComponent<ListLayout>().createInventoryList(InventoryManager.GetInstance().inventory, "use");

            }
            lastSelectedInventoryBtn = "use";
        }
    }

    public void onETCInventoryBtn()
    {
        if (!inventoryContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            inventoryContentPanel.SetActive(true);
            inventoryUseItemBtn.SetActive(false);

            if (InventoryManager.GetInstance().checkInventoryEmpty("etc"))
            {
                inventoryEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(inventoryEmptyMsg));
            }
            else
            {
                inventoryLeftPanel.SetActive(true);
                //inventoryRightPanel.SetActive(true);
                inventoryListRef.GetComponent<ListLayout>().createInventoryList(InventoryManager.GetInstance().inventory, "etc");
            }
            lastSelectedInventoryBtn = "etc";
        }
    }

    public void onImportantInventoryBtn()
    {
        if (!inventoryContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            inventoryContentPanel.SetActive(true);
            inventoryUseItemBtn.SetActive(false);

            if (InventoryManager.GetInstance().checkInventoryEmpty("important"))
            {
                inventoryEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(inventoryEmptyMsg));
            }
            else
            {
                inventoryLeftPanel.SetActive(true);
                //inventoryRightPanel.SetActive(true);
                inventoryListRef.GetComponent<ListLayout>().createInventoryList(InventoryManager.GetInstance().inventory, "important");
            }
            lastSelectedInventoryBtn = "important";
        }
    }

    public void onUseItemBtn()
    {
        SoundManager.GetInstance().playSEMenu(submitSE);
        Debug.Log("Use item " + lastSelectedItem);
        InventoryItem tempitem = null;
        tempitem = InventoryManager.GetInstance().getItemWName(lastSelectedItem);

        if (tempitem.itemData.item_type == "use")
        {

            switch (tempitem.itemData.item_usetype)
            {
                case "heal":
                    int temp = player.playerItemHeal(tempitem);

                    break;
                case "cure":
                    StatusManager.GetInstance().item_CureStatus(tempitem);

                    break;
            }
        }

        InventoryManager.GetInstance().inventory.Remove(tempitem);

        inventoryRightPanel.SetActive(false);
        inventoryListRef.GetComponent<ListLayout>().destroyListSelection();
        
        if (lastSelectedInventoryBtn == "all")
        {
            inventoryUseItemBtn.SetActive(false);

            if (InventoryManager.GetInstance().checkInventoryEmpty("all"))
            {
                inventoryEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(inventoryEmptyMsg));
            }
            else
            {
                inventoryLeftPanel.SetActive(true);
                //inventoryRightPanel.SetActive(true);
                inventoryListRef.GetComponent<ListLayout>().createInventoryList(InventoryManager.GetInstance().inventory, "all");
            }
            lastSelectedInventoryBtn = "all";
        }
        else if (lastSelectedInventoryBtn == "use")
        {
            inventoryUseItemBtn.SetActive(false);

            if (InventoryManager.GetInstance().checkInventoryEmpty("use"))
            {
                inventoryEmptyMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(inventoryEmptyMsg));
            }
            else
            {
                inventoryLeftPanel.SetActive(true);
                //inventoryRightPanel.SetActive(true);
                //inventoryUseItemBtn.SetActive(true);
                inventoryListRef.GetComponent<ListLayout>().createInventoryList(InventoryManager.GetInstance().inventory, "use");

            }
            lastSelectedInventoryBtn = "use";
        }
    }

    public void onQuestBtn()
    {
        if (!questPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            overlay.SetActive(true);
            questPanel.SetActive(true);
            StartCoroutine(ListLayout.selectOption(inprogQuestBtn));
        }
    }

    public void onInProgQuestBtn()
    {
        if (!questContentPanel.activeInHierarchy) {
            SoundManager.GetInstance().playSEMenu(submitSE);
            questContentPanel.SetActive(true);
            if (QuestManager.GetInstance().checkQuestListEmpty("inprog"))
            {
                emptyQuestMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(scrollareaq));
            }
            else
            {
                questListPanel.SetActive(true);
                //questInfoPanel.SetActive(true);
                questListRef.GetComponent<ListLayout>().createQuestList(QuestManager.GetInstance().questlist, "inprog");
            }
            lastSelectedQuestBtn = "inprog";
        }
    }

    public void onCompletedQuestBtn()
    {
        if (!questContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            questContentPanel.SetActive(true);
            if (QuestManager.GetInstance().checkQuestListEmpty("completed"))
            {
                emptyQuestMsg.SetActive(true);
                StartCoroutine(ListLayout.selectOption(scrollareaq));
            }
            else
            {
                questListPanel.SetActive(true);
                //questInfoPanel.SetActive(true);
                questListRef.GetComponent<ListLayout>().createQuestList(QuestManager.GetInstance().questlist, "completed");
            }
            lastSelectedQuestBtn = "completed";
        }
    }

    public void onSaveLoadBtn()
    {
        if (!saveloadPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            overlay.SetActive(true);
            saveloadPanel.SetActive(true);
            StartCoroutine(ListLayout.selectOption(saveBtn));
        }
    }

    public void onSaveBtn()
    {
        if (!saveloadContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            saveloadContentPanel.SetActive(true);
            saveloadListRef.GetComponent<ListLayout>().createSaveLoadList(ProgressManager.GetInstance().progressArray);
            lastSelectedSaveLoadBtn = "save";

        }
    }

    public void onLoadBtn()
    {
        if (!saveloadContentPanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            saveloadContentPanel.SetActive(true);
            saveloadListRef.GetComponent<ListLayout>().createSaveLoadList(ProgressManager.GetInstance().progressArray);

            lastSelectedSaveLoadBtn = "load";
        }
    }

    public void onSettingsBtn()
    {
        GameObject temp = SoundManager.GetInstance().settings;

        if (!temp.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            overlay.SetActive(true);
            temp.SetActive(true);
            SoundManager.GetInstance().updateUI();
            StartCoroutine(ListLayout.selectOption(SoundManager.GetInstance().muteMusicObj));

        }
    }

    public void closeSettingsBtn()
    {
        GameObject temp = SoundManager.GetInstance().settings;
        if (temp.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(cancelSE);
            SoundManager.GetInstance().revertValue();
            temp.SetActive(false);
            overlay.SetActive(false);
            StartCoroutine(ListLayout.selectOption(settingsBtn));
        }
    }

    public void onReturntoTitleBtn()
    {
        if (!returntotitlePanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(submitSE);
            overlay.SetActive(true);
            returntotitlePanel.SetActive(true);
            StartCoroutine(ListLayout.selectOption(returntoTitle_NoBtn));
        }
    }

    public void onReturntoTitle_No()
    {
        if (returntotitlePanel.activeInHierarchy)
        {
            SoundManager.GetInstance().playSEMenu(cancelSE);
            overlay.SetActive(false);
            returntotitlePanel.SetActive(false);
            StartCoroutine(ListLayout.selectOption(returntoTitleBtn));
        }
    }

    public void onReturntoTitle_Yes()
    {
        SoundManager.GetInstance().playSEMenu(submitSE);
        SceneManager.LoadScene("TitleScreen");
    }

    public void closeMenu()
    {
        SoundManager.GetInstance().playSEMenu(cancelSE);
        saveloadContentPanel.SetActive(false);
        saveloadPanel.SetActive(false);
        overlay.SetActive(false);
        menuCanvas.SetActive(false);
        menuIsOpened = false;
    }

    
}
