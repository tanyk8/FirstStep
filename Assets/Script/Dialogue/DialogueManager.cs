using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("LoadGlobalsJSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private GameObject portraitFrame;
    [SerializeField] private GameObject speakerFrame;
    [SerializeField] private GameObject dialogueFrame;
    [SerializeField] private GameObject monologueFrame;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI monologueText;

    [SerializeField] private TextMeshProUGUI displayNameText;

    [SerializeField] private Animator portraitAnimator;

    private Animator layoutAnimator;

    [Header("ChoiceListRef")]
    [SerializeField] private GameObject choiceListref;
    [SerializeField] private GameObject dialoguechoicepanel;

    [Header("QuestManager")]
    [SerializeField] private GameObject questManager;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    
    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string QUESTTRIGGER_TAG = "questtrigger";
    //private const string GIVEQUEST_TAG = "givequest_id";
    //private const string RECEIVEQUESTID_TAG = "receivequest_id";
    private const string QUESTID_TAG = "quest_id";
    private const string BATTLE_TAG = "battle";
    private const string PORTAL_TAG = "portal";
    private const string LOG_TAG = "logtype";

    public static event handleStartQuestT startQuestTrigger;
    public delegate void handleStartQuestT(QuestData questdata);

    private QuestData questData;

    public static event handleUpdateQuestPV updateQuestPV;
    public delegate void handleUpdateQuestPV(int quest_ID);

    public static event handleUpdateQuestT updateQuestTrigger;
    public delegate void handleUpdateQuestT(QuestData questdata,string type);

    public static event handleCompleteQuestT completeQuestTrigger;
    public delegate void handleCompleteQuestT(QuestData questdata);

    public event Action updateTalkingActor;

    private DialogueVariableObserver dialoguevariableobserver;

    private GameObject talkingActor;

    private bool initiateBattle;
    private bool changeScene;
    private string destination;

    private string dialoguetype;

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(dialogueCanvas);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        

        dialoguevariableobserver = new DialogueVariableObserver(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialogueCanvas.SetActive(false);
        dialoguechoicepanel.SetActive(false);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (canContinueToNextLine&& currentStory.currentChoices.Count == 0 && InputManager.getInstance().getSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {

        updateTalkingActor?.Invoke();

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueCanvas.SetActive(true);

        dialoguevariableobserver.startListening(currentStory);

        if (talkingActor.GetComponent<QuestGiver>() != null&&questManager.GetComponent<QuestManager>() != null)
        {
            startQuestTrigger += questManager.GetComponent<QuestManager>().startQuest;
            updateQuestPV += questManager.GetComponent<QuestManager>().updateTalkProgressValue;
            updateQuestTrigger += questManager.GetComponent<QuestManager>().updateQuestProgress;
            completeQuestTrigger += questManager.GetComponent<QuestManager>().completeQuest;
        }


        displayNameText.text = "?0?";
        portraitAnimator.Play("default");
        layoutAnimator.Play("layout_left");
        dialoguetype = "di";

        monologueFrame.SetActive(false);
        dialogueFrame.SetActive(true);
        portraitFrame.SetActive(true);
        speakerFrame.SetActive(true);


        ContinueStory();

    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialoguevariableobserver.stopListening(currentStory);

        if (talkingActor.GetComponent<QuestGiver>() != null&& questManager.GetComponent<QuestManager>() != null)
        {
            startQuestTrigger -= questManager.GetComponent<QuestManager>().startQuest;
            updateQuestPV -= questManager.GetComponent<QuestManager>().updateTalkProgressValue;
            updateQuestTrigger -= questManager.GetComponent<QuestManager>().updateQuestProgress;
            completeQuestTrigger -= questManager.GetComponent<QuestManager>().completeQuest;
        }


 
        

        dialogueIsPlaying = false;
        //dialoguePanel.SetActive(false);
        dialogueCanvas.SetActive(false);
        dialogueText.text = "";

        if (initiateBattle)
        {
            initiateBattle = false;
            SceneManager.LoadScene("Battlescene");
        }
        else if (changeScene)
        {

            changeScene = false;
            switch (destination)
            {
                case "FirstStage_Park":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("FirstStage_Park");
                    break;

            }
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayMonoLine(string line)
    {

        monologueText.text = line;
        monologueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);


        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray())
        {
            if (InputManager.getInstance().getSubmitPressed())
            {
                monologueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                monologueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }


        }

        continueIcon.SetActive(true);

        if (currentStory.currentChoices.Count != 0)
        {
            DisplayChoices();
        }


        canContinueToNextLine = true;
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        monologueText.text = line;
        monologueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);
        

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach(char letter in line.ToCharArray())
        {
            if (InputManager.getInstance().getSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                monologueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                monologueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }

            
        }

        continueIcon.SetActive(true);

        if (currentStory.currentChoices.Count != 0)
        {
            DisplayChoices();
        }
        

        canContinueToNextLine = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        string quest_id = "";
        string questTrigger = "";
        string battle="";
        string portal = "";

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);

            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case QUESTTRIGGER_TAG:
                    questTrigger = tagValue;
                    break;
                case QUESTID_TAG:
                    quest_id = tagValue;
                    break;
                case BATTLE_TAG:
                    battle = tagValue;
                    break;
                case PORTAL_TAG:
                    portal = tagValue;
                    break;
                case LOG_TAG:
                    dialoguetype = tagValue;
                    if (tagValue == "mono")
                    {
                        monologueFrame.SetActive(true);
                        dialogueFrame.SetActive(false);
                        portraitFrame.SetActive(false);
                        speakerFrame.SetActive(false);
                    }
                    else if (tagValue == "di")
                    {
                        monologueFrame.SetActive(false);
                        dialogueFrame.SetActive(true);
                        portraitFrame.SetActive(true);
                        speakerFrame.SetActive(true);
                    }
                    
                    break;
                default:
                    Debug.LogWarning("Tag in switch case but not handled: "+tag);
                    break;
            }
        }

        if (questTrigger == "start")
        {
            questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(quest_id));
            startQuestTrigger?.Invoke(questData);
        }

        if (questTrigger == "updateprogressvalue")
        {
            updateQuestPV?.Invoke(int.Parse(quest_id));
        }

        if (questTrigger == "proceedprogress")
        {
            questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(quest_id));
            updateQuestTrigger?.Invoke(questData, "proceedprogress");
        }

        if (questTrigger == "complete")
        {
            questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(quest_id));
            completeQuestTrigger?.Invoke(questData);
        }

        if (battle == "start")
        {
            initiateBattle = true;
            
        }
        if (portal !="")
        {
            destination = portal;
            changeScene = true;
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (dialoguechoicepanel.activeInHierarchy == false)
        {
            dialoguechoicepanel.SetActive(true);
            choiceListref.GetComponent<ListLayout>().createChoiceList(currentChoices.Count, currentChoices);
        }
        


    }

    public void MakeChoice(int choiceIndex,string response)
    {

        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);

            InputManager.getInstance().registerSubmitPressed();

            choiceListref.GetComponent<ListLayout>().destroyListSelection();
            dialoguechoicepanel.SetActive(false);

            ContinueStory();
        }
        
    }

    public Story getStory()
    {
        return currentStory;
    }

    public void setTalkingActor(GameObject talkingactor)
    {
        talkingActor = talkingactor;
    }

}
