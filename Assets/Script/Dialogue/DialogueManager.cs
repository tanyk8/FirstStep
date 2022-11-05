using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

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

    //[Header("Animator")]
    //[SerializeField] private Animator mainenemyAnimator;
    //[SerializeField] private Animator darkauraAnimator;
    //[Header("AnimatorObj")]
    //[SerializeField] private GameObject mainenemyObj;

    [Header("cutscene")]
    [SerializeField] private GameObject cutscene;


    private Animator currentAnimator;
    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    
    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string QUESTTRIGGER_TAG = "questtrigger";
    private const string QUESTTRITYPE_TAG = "questtrigger_type";
    private const string QUESTID_TAG = "quest_id";
    private const string BATTLE_TAG = "battle";
    private const string PORTAL_TAG = "portal";
    private const string LOG_TAG = "logtype";
    private const string CUTSCENE_TAG = "cutscene";
    private const string ANIMATOR_TAG = "animator";
    private const string ANIMATION_TAG = "animation";
    private const string LEARNSKILL_TAG = "learnskill";
    private const string GETITEM_TAG = "getitem";
    private const string REMOVEITEM_TAG = "removeitem";
    private const string CALLFUNCTION_TAG = "callfunction";
    private const string ENEMY_TAG = "enemy";
    private const string ADDSTAT_TAG = "addstat";
    private const string PLAYSE_TAG = "playse";

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

    //private GameObject talkingActor;

    private bool initiateBattle;
    private bool changeScene;
    private string destination;

    private string dialoguetype;

    public bool notInteractDialogue=false;

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
                DontDestroyOnLoad(cutscene);
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
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
            Destroy(dialogueCanvas);
            Destroy(cutscene);
        }

        if (!dialogueIsPlaying)
        {
            return;
        }
        else
        {
            if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && InputManager.getInstance().getSubmitPressed())
            {
                
                ContinueStory();
            }
        }
        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {



        if (!notInteractDialogue)
        {
            updateTalkingActor?.Invoke();
        }
        


        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueCanvas.SetActive(true);

        dialoguevariableobserver.startListening(currentStory);

        //if (!notInteractDialogue)
        //{
        //    if(talkingActor.GetComponent<QuestGiver>() != null && questManager.GetComponent<QuestManager>() != null)
        //    {
        //        startQuestTrigger += questManager.GetComponent<QuestManager>().startQuest;
        //        updateQuestPV += questManager.GetComponent<QuestManager>().updateTalkProgressValue;
        //        updateQuestTrigger += questManager.GetComponent<QuestManager>().updateQuestProgress;
        //        completeQuestTrigger += questManager.GetComponent<QuestManager>().completeQuest;
        //    }

        //}

        startQuestTrigger += questManager.GetComponent<QuestManager>().startQuest;
        updateQuestPV += questManager.GetComponent<QuestManager>().updateTalkProgressValue;
        updateQuestTrigger += questManager.GetComponent<QuestManager>().updateQuestProgress;
        completeQuestTrigger += questManager.GetComponent<QuestManager>().completeQuest;

        monologueFrame.SetActive(false);
        dialogueFrame.SetActive(true);
        portraitFrame.SetActive(true);
        speakerFrame.SetActive(true);

        displayNameText.text = "?0?";
        portraitAnimator.Play("default");
        layoutAnimator.Play("layout_left");
        dialoguetype = "di";

        


        ContinueStory();

    }

    public void ExitDialogueMode()
    {
        //IEnumerator
        //yield return new WaitForSeconds(0.2f);

        dialoguevariableobserver.stopListening(currentStory);

        //if (!notInteractDialogue)
        //{
        //    if (talkingActor.GetComponent<QuestGiver>() != null && questManager.GetComponent<QuestManager>() != null)
        //    {
        //        startQuestTrigger -= questManager.GetComponent<QuestManager>().startQuest;
        //        updateQuestPV -= questManager.GetComponent<QuestManager>().updateTalkProgressValue;
        //        updateQuestTrigger -= questManager.GetComponent<QuestManager>().updateQuestProgress;
        //        completeQuestTrigger -= questManager.GetComponent<QuestManager>().completeQuest;
        //    }
        //}


        startQuestTrigger -= questManager.GetComponent<QuestManager>().startQuest;
        updateQuestPV -= questManager.GetComponent<QuestManager>().updateTalkProgressValue;
        updateQuestTrigger -= questManager.GetComponent<QuestManager>().updateQuestProgress;
        completeQuestTrigger -= questManager.GetComponent<QuestManager>().completeQuest;


        dialogueIsPlaying = false;
        //dialoguePanel.SetActive(false);
        dialogueCanvas.SetActive(false);
        dialogueText.text = "";

        notInteractDialogue = false;

        if (initiateBattle)
        {
            initiateBattle = false;
            GameStateManager.GetInstance().position= GameObject.Find("Player").transform.position;
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
                case "Beginning":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Beginning");
                    
                    break;
                case "SQ_1_backstory":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SQ_1_backstory");
                    break;
                case "SecondStage_Hallway":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SecondStage_Hallway");
                    break;
                case "SQ_2_backstory":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SQ_2_backstory");
                    break;
                case "SQ_2_mindworld":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SQ_2_mindworld");
                    break;
                case "ThirdStage_hospitalhall":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("ThirdStage_hospitalhall");
                    break;
                case "SQ_3_backstory":
                    destination = "";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SQ_3_backstory");
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
            Debug.Log("Test");
            //StartCoroutine(ExitDialogueMode());
            ExitDialogueMode();
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
        string questTrigger_type = "";
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
                case QUESTTRITYPE_TAG:
                    questTrigger_type = tagValue;
                    break;
                case QUESTID_TAG:
                    quest_id = tagValue;
                    break;
                case BATTLE_TAG:
                    battle = tagValue;
                    break;
                case PORTAL_TAG:
                    portal = tagValue;
                    Debug.Log(tagValue);
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
                case CUTSCENE_TAG:
                    PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/"+tagValue);
                    
                    TimelineManager.GetInstance().playTimeline(cutscene);

                    break;
                case ANIMATOR_TAG:
                    if (tagValue == "mainenemyappear")
                    {
                        //mainenemyObj.SetActive(true);
                        //darkauraAnimator.SetTrigger("Fadein");
                        //mainenemyAnimator.SetTrigger("Fadein");
                        LevelZero lvlzero = GameObject.Find("LevelManager").GetComponent<LevelZero>();

                        lvlzero.performAction(tagValue);
                    }
                    

                    break;
                case LEARNSKILL_TAG:
                    if (tagValue == "starter")
                    {
                        SkillData testskill1 = Resources.Load<SkillData>("Skill/skill1");
                        Skill skill1 = new Skill(testskill1);
                        skill1.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill1);

                        SkillData testskill2 = Resources.Load<SkillData>("Skill/skill2");
                        Skill skill2 = new Skill(testskill2);
                        skill2.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill2);

                        SkillData testskill3 = Resources.Load<SkillData>("Skill/skill3");
                        Skill skill3 = new Skill(testskill3);
                        skill3.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill3);

                        SkillData testskill10 = Resources.Load<SkillData>("Skill/skill10");
                        Skill skill10 = new Skill(testskill10);
                        skill10.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill10);
                    }
                    else if (tagValue == "second")
                    {
                        SkillData testskill4 = Resources.Load<SkillData>("Skill/skill4");
                        Skill skill4 = new Skill(testskill4);
                        skill4.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill4);

                        SkillData testskill5 = Resources.Load<SkillData>("Skill/skill5");
                        Skill skill5 = new Skill(testskill5);
                        skill5.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill5);

                        SkillData testskill6 = Resources.Load<SkillData>("Skill/skill6");
                        Skill skill6 = new Skill(testskill6);
                        skill6.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill6);
                    }
                    else if (tagValue == "third")
                    {
                        SkillData testskill7 = Resources.Load<SkillData>("Skill/skill7");
                        Skill skill7 = new Skill(testskill7);
                        skill7.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill7);

                        SkillData testskill8 = Resources.Load<SkillData>("Skill/skill8");
                        Skill skill8 = new Skill(testskill8);
                        skill8.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill8);

                        SkillData testskill9 = Resources.Load<SkillData>("Skill/skill9");
                        Skill skill9 = new Skill(testskill9);
                        skill9.updateSkillLearnt();
                        SkillManager.GetInstance().skilllist.Add(skill9);
                    }

                    break;
                case ADDSTAT_TAG:
                    if (tagValue == "clearfirst")
                    {
                        Player.GetInstance().updatePlayer(2000, 250, 160, 65, 2000, 250);
                    }
                    else if (tagValue == "clearsecond")
                    {
                        Player.GetInstance().updatePlayer(3250, 300, 325, 135, 3250, 300);
                    }
                    else if (tagValue == "finalpower")
                    {
                        Player.GetInstance().updatePlayer(3500, 350, 450, 280, 3500, 350);
                    }
                    //Player.GetInstance().
                    break;
                case GETITEM_TAG:
                    ItemData additemData = Resources.Load<ItemData>("Item/"+tagValue);
                    InventoryManager.GetInstance().Add(additemData);
                    break;
                case REMOVEITEM_TAG:
                    ItemData removeitemData = Resources.Load<ItemData>("Item/" + tagValue);
                    InventoryManager.GetInstance().Remove(removeitemData);
                    break;
                case ENEMY_TAG:
                    GameStateManager.GetInstance().enemy = tagValue;
                    break;
                case PLAYSE_TAG:
                    if (tagValue == "heal")
                    {
                        SoundManager.GetInstance().playSE(Resources.Load<AudioClip>("Sound/SE/se_heal"));
                    }
                    else if (tagValue == "gate")
                    {
                        SoundManager.GetInstance().playSE(Resources.Load<AudioClip>("Sound/SE/se_gate"));
                    }
                    
                    break;
                default:
                    Debug.LogWarning("Tag in switch case but not handled: "+tag);
                    break;
            }
        }

        if (questTrigger == "start")
        {
            //questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(quest_id));
            questData = Resources.Load<QuestData>("Quest/quest"+quest_id);
            startQuestTrigger?.Invoke(questData);
        }

        if (questTrigger == "updateprogressvalue")
        {
            updateQuestPV?.Invoke(int.Parse(quest_id));
        }

        if (questTrigger == "proceedprogress")
        {
            //questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(quest_id));
            questData = Resources.Load<QuestData>("Quest/quest" + quest_id);
            if (questTrigger_type == "")
            {
                updateQuestTrigger?.Invoke(questData, "proceedprogress");
            }
            else if (questTrigger_type == "force")
            {
                updateQuestTrigger?.Invoke(questData, "force");
            }
            
        }

        if (questTrigger == "complete")
        {
            //questData = talkingActor.GetComponent<QuestGiver>().getTargetQuestData(int.Parse(quest_id));
            questData = Resources.Load<QuestData>("Quest/quest" + quest_id);
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

    //public void setTalkingActor(GameObject talkingactor)
    //{
    //    talkingActor = talkingactor;
    //}

}
