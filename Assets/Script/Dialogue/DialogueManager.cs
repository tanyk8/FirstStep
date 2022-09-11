using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;

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

    [SerializeField] private TextMeshProUGUI dialogueText;

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
    private const string GIVEQUEST_TAG = "givequest_id";
    private const string RECEIVEQUESTID_TAG = "receivequest_id";

    public static event handleStartQuestT startQuestTrigger;
    public delegate void handleStartQuestT(QuestData questdata);

    private QuestData questData;

    public static event handleUpdateQuestPV updateQuestPV;
    public delegate void handleUpdateQuestPV(int receivequest_ID);

    public static event handleUpdateQuestT updateQuestTrigger;
    public delegate void handleUpdateQuestT(QuestData questdata,string type);

    public static event handleCompleteQuestT completeQuestTrigger;
    public delegate void handleCompleteQuestT(QuestData questdata);

    public static event Action updateTalkingActor;

    private DialogueVariableObserver dialoguevariableobserver;

    private GameObject talkingActor; 

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

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
        

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("layout_left");

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
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if(displayLineCoroutine != null)
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

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);
        

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach(char letter in line.ToCharArray())
        {
            if (InputManager.getInstance().getSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
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
        string givequest_id = "";
        string receivequest_id = "";
        string questTrigger = "";

        

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
                case GIVEQUEST_TAG:
                    givequest_id = tagValue;
                    break;
                case RECEIVEQUESTID_TAG:
                    receivequest_id = tagValue;
                    break;
                default:
                    Debug.LogWarning("Tag in switch case but not handled: "+tag);
                    break;
            }
        }

        if (questTrigger == "start")
        {
            questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(givequest_id));
            startQuestTrigger?.Invoke(questData);
        }

        if (questTrigger == "updateprogressvalue")
        {
            updateQuestPV?.Invoke(int.Parse(receivequest_id));
        }

        if (questTrigger == "proceedprogress")
        {
            updateQuestTrigger?.Invoke(questData, "proceedprogress");
        }

        if (questTrigger == "complete")
        {
            questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(givequest_id));
            completeQuestTrigger?.Invoke(questData);
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
