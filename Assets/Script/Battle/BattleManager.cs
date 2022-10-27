using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
using Ink.Runtime;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    private BattleState state;

    private Player player;
    private Enemy enemy;

    private string battle_encountermessage="A strong presences of shadow energy has been sensed!";

    private ListLayout listObjectRef;

    private static BattleManager instance;

    [Header("Player")]
    [SerializeField] private GameObject playerGameObject;


    [Header("Enemy")]
    [SerializeField] public GameObject[] enemyPrefabArray;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] private Transform enemyLocation;
    [SerializeField] private TextMeshProUGUI enemy_name;

    [Header("Log")]
    [SerializeField] private TextMeshProUGUI battle_status;

    [Header("BattleHUD")]
    [SerializeField] BattleUIManager playerHUD;
    [SerializeField] BattleUIManager enemyHUD;

    [Header("Button")]
    [SerializeField] Button attackBtn;
    [SerializeField] Button skillBtn;
    [SerializeField] Button itemBtn;
    [SerializeField] Button runBtn;
    [SerializeField] Button backBtn;

    [Header("Panel")]
    [SerializeField] GameObject statuspanel;
    [SerializeField] GameObject actionpanel;

    [Header("ListLayout")]
    [SerializeField] GameObject actionList;
    [SerializeField] Transform actionListT;

    [Header("GameObject")]
    [SerializeField] GameObject attBtnObj;
    [SerializeField] GameObject skillBtnObj;
    [SerializeField] GameObject itemBtnObj;
    [SerializeField] GameObject backBtnObj;

    [SerializeField] public GameObject useBtnObj;

    [SerializeField] GameObject playerstatusbtn;
    [SerializeField] GameObject enemystatusbtn;
    [SerializeField] GameObject status;
    [SerializeField] GameObject statusempty;
    [SerializeField] GameObject statusleft;
    [SerializeField] GameObject statusright;
    [SerializeField] GameObject statusListRef;

    [SerializeField] GameObject descriptionText;
    [SerializeField] GameObject detailPanel_usebtn;
    [SerializeField] GameObject detailPanel_description;

    [SerializeField] GameObject statusDescription;
    [SerializeField] GameObject statusDescriptionPanel;
    [SerializeField] Transform statusListT;
    [SerializeField] GameObject statusTitle;
    [SerializeField] GameObject statusBackBtn;

    List<Status> enemystatus=new List<Status>();


    string lastSelectedOption;
    public string lastSelectedName;
    public int lastSelectedIndex;

    public string lastSelectedStatus;
    public int lastSelectedStatusIndex;

    string lastSelectedStatusBtn;


    bool playerAttackCharge;
    string tempAttackName;
    bool enemyAttackCharge;
    string tempEnemyAttackName;

    bool useSkill=false;
    bool useSkill_Enemy = false;

    //int currentTurn;

    GameObject previousSelectedObject;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Battle Manager in the scene");
        }
        instance = this;
    }

    private void Update()
    {
        if (state == BattleState.PLAYERTURN)
        {
            if (InputManager.getInstance().getSwitchPressed())
            {
                if (EventSystem.current.currentSelectedGameObject == playerstatusbtn || EventSystem.current.currentSelectedGameObject == enemystatusbtn)
                {
                    StartCoroutine(ListLayout.selectOption(previousSelectedObject));
                    previousSelectedObject = null;
                }
                else
                {
                    previousSelectedObject = EventSystem.current.currentSelectedGameObject;
                    StartCoroutine(ListLayout.selectOption(playerstatusbtn));
                }
            }

            if (SceneManager.GetActiveScene().name == "Battlescene"&&InputManager.getInstance().getMenuPressed() )
            {
                if (actionpanel.activeInHierarchy)
                {
                    if (EventSystem.current.currentSelectedGameObject == useBtnObj)
                    {
                        descriptionText.GetComponent<TextMeshProUGUI>().text = "";
                        detailPanel_usebtn.SetActive(false);
                        detailPanel_description.SetActive(false);
                        StartCoroutine(ListLayout.selectOption(actionListT.GetChild(lastSelectedIndex).gameObject));
                    }
                    else if (lastSelectedOption == "skill")
                    {
                        statuspanel.SetActive(true);
                        actionpanel.SetActive(false);
                        detailPanel_usebtn.SetActive(false);
                        detailPanel_description.SetActive(false);
                        listObjectRef.destroyListSelection();

                        setBtnStatus(true);

                        StartCoroutine(ListLayout.selectOption(skillBtnObj));
                    }
                    else if (lastSelectedOption == "item")
                    {
                        statuspanel.SetActive(true);
                        actionpanel.SetActive(false);
                        detailPanel_usebtn.SetActive(false);
                        detailPanel_description.SetActive(false);
                        listObjectRef.destroyListSelection();

                        setBtnStatus(true);

                        StartCoroutine(ListLayout.selectOption(itemBtnObj));
                    }
                }

                else if (status.activeInHierarchy)
                {
                    if (lastSelectedStatus != "")
                    {
                        statusDescription.GetComponent<TextMeshProUGUI>().text = "";
                        statusDescriptionPanel.SetActive(false);
                        StartCoroutine(ListLayout.selectOption(statusListT.GetChild(lastSelectedStatusIndex).gameObject));
                        lastSelectedStatus = "";
                    }
                    else if (status.activeInHierarchy)
                    {
                        statusListRef.GetComponent<ListLayout>().destroyListSelection();
                        status.SetActive(false);
                        if (lastSelectedStatusBtn == "player")
                        {
                            StartCoroutine(ListLayout.selectOption(playerstatusbtn));
                        }
                        else if (lastSelectedStatusBtn == "enemy")
                        {
                            StartCoroutine(ListLayout.selectOption(enemystatusbtn));
                        }
                    }
                }
            }
            
        }
    }

    public static BattleManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        //currentTurn = 0;
        
        listObjectRef = actionList.GetComponent<ListLayout>();


        
        state = BattleState.START;

        StartCoroutine(initiateBattle());
    }
   

    IEnumerator initiateBattle()
    {
        statuspanel.SetActive(true);
        setBtnStatus(false);

        if (GameStateManager.GetInstance().enemy == "firststage")
        {
           enemyPrefab = enemyPrefabArray[0];
        }
        else if (GameStateManager.GetInstance().enemy == "secondstage")
        {
            enemyPrefab = enemyPrefabArray[1];
        }
        else if (GameStateManager.GetInstance().enemy == "finalstage")
        {
            enemyPrefab = enemyPrefabArray[2];
        }
        else if (GameStateManager.GetInstance().enemy == "finalstage2")
        {
            enemyPrefab = enemyPrefabArray[3];
        }


        GameObject enemyGameObject= Instantiate(enemyPrefab, enemyLocation);
        enemy = enemyGameObject.GetComponent<Enemy>();

        //player=playerGameObject.GetComponent<Player>();
        player = Player.GetInstance();

        battle_status.text = battle_encountermessage;

        enemyHUD.setEnemyHUD(enemy);
        playerHUD.setPlayerHUD(player);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    void playerTurn()
    {

        StatusManager.GetInstance().updateStatusDuration();
        for (int x = 0; x < enemystatus.Count; x++)
        {
            enemystatus.ElementAt(x).current_duration -= 1;
        }

        for (int x = 0; x < enemystatus.Count; x++)
        {
            if (enemystatus.ElementAt(x).current_duration == 0)
            {
                enemystatus.RemoveAt(x);
            }
        }

        setBtnStatus(true);
        StartCoroutine(ListLayout.selectOption(attBtnObj));
        battle_status.text = "Choose an action!";
    }

    void endBattle()
    {
        if (state == BattleState.WON)
        {

            //if enemy prefab name is this, then this
            if (enemyPrefab.name == "Enemy1")
            {
                //item shard of light
                //ItemData additemData = Resources.Load<ItemData>("Item/item1");
                //InventoryManager.GetInstance().Add(additemData);
                //Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
                //temp.EvaluateFunction("activatestagetwo", true);
                string[] tempendmsg = { "You have purified the shadow!", "You received a shard of light" };
                ProgressManager.GetInstance().gameProgress = "progress9";
                player.setCurrent_Health(player.getStat_MaxHealth());
                StartCoroutine(endBattleChoiceMsg_E(tempendmsg));
            }
            else if (enemyPrefab.name == "Enemy2")
            {
                //item shard of light
                //ItemData additemData = Resources.Load<ItemData>("Item/item1");
                //InventoryManager.GetInstance().Add(additemData);
                //Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
                //temp.EvaluateFunction("activatestagetwo", true);
                string[] tempendmsg = { "You have purified the shadow!", "You received a shard of light" };
                ProgressManager.GetInstance().gameProgress = "progress19";
                player.setCurrent_Health(player.getStat_MaxHealth());
                StartCoroutine(endBattleChoiceMsg_E(tempendmsg));
            }
            else if (enemyPrefab.name == "Shadow")
            {
                //item shard of light
                //ItemData additemData = Resources.Load<ItemData>("Item/item1");
                //InventoryManager.GetInstance().Add(additemData);
                //Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
                //temp.EvaluateFunction("activatestagetwo", true);
                string[] tempendmsg = { "You have purified the shadow!" };
                ProgressManager.GetInstance().gameProgress = "progress25";
                player.setCurrent_Health(player.getStat_MaxHealth());
                StartCoroutine(endBattleChoiceMsg_E(tempendmsg));
            }
            else if (enemyPrefab.name == "Shadow Final Form")
            {
                //item shard of light
                //ItemData additemData = Resources.Load<ItemData>("Item/item1");
                //InventoryManager.GetInstance().Add(additemData);
                //Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
                //temp.EvaluateFunction("activatestagetwo", true);
                string[] tempendmsg = { "You have purified the shadow!" };
                ProgressManager.GetInstance().gameProgress = "progress28";
                player.setCurrent_Health(player.getStat_MaxHealth());
                StartCoroutine(endBattleChoiceMsg_E(tempendmsg));
            }
            
        }
        else if (state == BattleState.LOST)
        {
            string[] tempendmsg = { "It's too strong we have to retreat for now!" };
            player.setCurrent_Health(player.getStat_MaxHealth());
            StartCoroutine(endBattleChoiceMsg_E(tempendmsg));

        }

        

    }

    IEnumerator enemyTurn()
    {

        

        int enemydmgdealt = 0;
        int probability = Random.Range(0, 20);
        float enemystatusvalue = 100;

        int enemychoice = Random.Range(0, 100); //0-99
        string enemychoiceresult = "";

        if (enemy.getCurrent_Health() > enemy.getStat_MaxHealth() * 80 / 100)
        {
            if (enemychoice < 10)
            {
                enemychoiceresult = "skill";
            }
            else if (enemychoice >= 10 && enemychoice < 30)
            {
                enemychoiceresult = "debuff";
            }
            else
            {
                enemychoiceresult = "attack";
            }
        }
        else if (enemy.getCurrent_Health() > enemy.getStat_MaxHealth() * 60 / 100)
        {
            if (enemychoice < 10)
            {
                enemychoiceresult = "heal";
            }
            else if (enemychoice >= 10 && enemychoice < 30)
            {
                enemychoiceresult = "skill";
            }
            else
            {
                enemychoiceresult = "attack";
            }
        }
        else if (enemy.getCurrent_Health() > enemy.getStat_MaxHealth() * 40 / 100)
        {
            if (enemychoice < 10)
            {
                enemychoiceresult = "buff";
            }
            else if (enemychoice >= 10 && enemychoice < 30)
            {
                enemychoiceresult = "skill";
            }
            else if (enemychoice >= 30 && enemychoice < 60)
            {
                enemychoiceresult = "heal";
            }
            else
            {
                enemychoiceresult = "attack";
            }
        }
        else if (enemy.getCurrent_Health() > enemy.getStat_MaxHealth() * 20 / 100)
        {
            if (enemychoice < 10)
            {
                enemychoiceresult = "skill";
            }
            else if (enemychoice >= 10 && enemychoice < 30)
            {
                enemychoiceresult = "debuff";
            }
            else if (enemychoice >= 30 && enemychoice < 60)
            {
                enemychoiceresult = "heal";
            }
            else
            {
                enemychoiceresult = "attack";
            }
        }



        //skill 0 is dmg, 1 is heal, 2 is buff, 3 is debuff
        if (enemychoiceresult == "heal")
        {
            int temp = enemy.enemySkillHeal(enemy.enemydata.enemy_skilllist.ElementAt(1));
            string[] healmsg = { "Enemy has used " + enemy.enemydata.enemy_skilllist.ElementAt(1).enemyskill_name + "!", "Enemy has recovered " + temp + " HP!" };
            enemyHUD.setEnemyHP(enemy.getCurrent_Health(), enemy);

            StartCoroutine(actionChoiceMsg_E(healmsg));
        }
        else if (enemychoiceresult == "buff")
        {
            enemy_InflictBuffStatus(enemy.enemydata.enemy_skilllist.ElementAt(2));
            string[] buffmsg = { "The Enemy used " + enemy.enemydata.enemy_skilllist.ElementAt(2).enemyskill_name + "!", "You feel that the enemy has gotten stronger!" };

            StartCoroutine(actionChoiceMsg_E(buffmsg));
            
        }
        else if (enemychoiceresult == "debuff")
        {

            StatusManager.GetInstance().skill_InflictDebuffStatus(enemy.enemydata.enemy_skilllist.ElementAt(3));
            string[] debuffmsg = { "The Enemy used " + enemy.enemydata.enemy_skilllist.ElementAt(3).enemyskill_name + "!", "You start to feel weakened" };

            StartCoroutine(actionChoiceMsg_E(debuffmsg));
        }
        else
        {
            if (enemychoiceresult == "skill")
            {
                useSkill_Enemy = true;
            }

            if (useSkill_Enemy)
            {
                battle_status.text = enemy.getEnemyName() + " used " + enemy.enemydata.enemy_skilllist.ElementAt(0).enemyskill_name + "!";
            }
            else
            {
                battle_status.text = enemy.getEnemyName() + " attacks!";
            }
            yield return new WaitForSeconds(1f);

            if (enemystatus.Count != 0)
            {
                for (int x = 0; x < enemystatus.Count; x++)
                {
                    if (enemystatus.ElementAt(x).statusData.status_type == "enemystatus_dmg")
                    {
                        enemystatusvalue += enemystatus.ElementAt(x).statusData.status_value - 100;
                    }
                }
            }

            if (useSkill_Enemy)
            {

                enemydmgdealt = (int)(player.getStat_MentalPower() * enemy.enemydata.enemy_skilllist.ElementAt(0).enemyskill_power / 100.0f * enemystatusvalue / 100.0f);
                useSkill_Enemy = false;
            }
            else
            {
                enemydmgdealt = (int)(player.getStat_MentalPower() * enemystatusvalue / 100.0f);
            }

            string attackresult = "";


            if (probability <= 3)
            {
                enemydmgdealt = enemydmgdealt * 125 / 100;
                attackresult = "critical";

            }
            else if (probability == 19)
            {
                enemydmgdealt = 0;
                attackresult = "missed";

            }
            else
            {
                enemydmgdealt = Random.Range(enemydmgdealt * 75 / 100, enemydmgdealt * 115 / 100);
                attackresult = "normal";

            }

            float reducedmg = 0f;
            float playerstatusvalue = 100;

            StatusManager tempstatusmanager = StatusManager.GetInstance();

            if (!tempstatusmanager.checkStatusEmpty())
            {
                for (int x = 0; x < tempstatusmanager.statusList.Count; x++)
                {
                    if (tempstatusmanager.statusList.ElementAt(x).statusData.status_type == "playerstatus_def")
                    {
                        playerstatusvalue += tempstatusmanager.statusList.ElementAt(x).statusData.status_value - 100;
                    }
                }
            }



            reducedmg = 100 - (player.getStat_MentalProtection() * playerstatusvalue / 100) * 0.2f;

            enemydmgdealt = (int)(enemydmgdealt * reducedmg / 100);


            switch (attackresult)
            {
                case "critical":
                    battle_status.text = "Enemy dealt " + enemydmgdealt + " critical damage!";
                    break;
                case "normal":
                    battle_status.text = "Enemy dealt " + enemydmgdealt + " damage";
                    break;
                case "missed":
                    battle_status.text = "The enemy missed!";
                    break;
            }


            bool isDefeated = player.receiveDamage(enemydmgdealt);


            playerHUD.setPlayerHP(player.getCurrent_Health(), player);

            yield return new WaitForSeconds(1f);

            if (isDefeated)
            {

                state = BattleState.LOST;
                endBattle();
            }
            else
            {
                

                state = BattleState.PLAYERTURN;
                playerTurn();
            }

        }
        
    }

    IEnumerator playerAttack(Skill skill)
    {
        if (actionpanel.activeInHierarchy)
        {
            statuspanel.SetActive(true);
            actionpanel.SetActive(false);

            listObjectRef.destroyListSelection();
        }

        if (useSkill)
        {
            battle_status.text = "Player used "+lastSelectedName+"!";
        }
        else
        {
            battle_status.text = "Player chose to attack!";
        }
    
        yield return new WaitForSeconds(1f);


        int playerdmgdealt = 0;
        int probability = Random.Range(0, 20);

        float playerstatusvalue = 100;

        StatusManager tempstatusmanager = StatusManager.GetInstance();

        if (!tempstatusmanager.checkStatusEmpty())
        {
            for(int x = 0; x < tempstatusmanager.statusList.Count; x++)
            {
                if (tempstatusmanager.statusList.ElementAt(x).statusData.status_type=="playerstatus_dmg")
                {
                    playerstatusvalue += tempstatusmanager.statusList.ElementAt(x).statusData.status_value-100;
                }
            }
        }


        if (useSkill)
        {
            playerdmgdealt = (int)(player.getStat_MentalPower()*skill.skillData.skill_power/100.0f*playerstatusvalue/100.0f);
            useSkill = false;
        }
        else
        {
            playerdmgdealt = (int)(player.getStat_MentalPower()*playerstatusvalue/100.0f);
        }

        string attackresult = "";

        if ( probability<= 3)
        {
            playerdmgdealt = playerdmgdealt * 125 / 100;
            attackresult = "critical";
            
        }
        else if (probability==19)
        {
            playerdmgdealt = 0;
            attackresult = "missed";
            
        }
        else
        {
            playerdmgdealt = Random.Range(playerdmgdealt * 75 / 100, playerdmgdealt * 115 / 100);
            attackresult = "normal";
            
        }

        float reducedmg = 0f;
        float enemystatusvalue = 100;
        

        if (enemystatus.Count != 0)
        {
            for (int x = 0; x < enemystatus.Count; x++)
            {
                if (enemystatus.ElementAt(x).statusData.status_type == "enemystatus_def")
                {
                    enemystatusvalue += enemystatus.ElementAt(x).statusData.status_value - 100;
                }
            }
        }

        reducedmg = 100 - (enemy.getStat_Defence()*enemystatusvalue/100) * 0.2f;

        playerdmgdealt = (int)(playerdmgdealt * reducedmg / 100);

        switch (attackresult)
        {
            case "critical":
                battle_status.text = "Enemy took " + playerdmgdealt + " critical damage!";
                break;
            case "normal":
                battle_status.text = "Enemy took " + playerdmgdealt + " damage";
                break;
            case "missed":
                battle_status.text = "You've missed!";
                break;
        }


        bool isDefeated=enemy.receiveDamage(playerdmgdealt);

        enemyHUD.setEnemyHP(enemy.getCurrent_Health(),enemy);

        yield return new WaitForSeconds(1f);

        if (isDefeated)
        {
            state = BattleState.WON;
            endBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(enemyTurn());
        }
    }

    public void onAttackBtn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        setBtnStatus(false);
        StartCoroutine(playerAttack(null));
    }

    public void onSkillBtn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        if (SkillManager.GetInstance().checkSkillEmpty("learnt"))
        {
            string[] tempmsg = { "You don't have any skills!", "Choose an action!" };
            StartCoroutine(updateMsg(tempmsg));
            return;
        }


        if (statuspanel.activeInHierarchy)
        {

            setBtnStatus(false);

            actionpanel.SetActive(true);
            statuspanel.SetActive(false);

            listObjectRef.createBattleSkillList(SkillManager.GetInstance().skilllist, "learnt");

            StartCoroutine(ListLayout.selectOption(backBtnObj));
            lastSelectedOption = "skill";
        }
    }

    public void onBackBtn()
    {
        statuspanel.SetActive(true);
        actionpanel.SetActive(false);
        detailPanel_usebtn.SetActive(false);
        detailPanel_description.SetActive(false);
        descriptionText.GetComponent<TextMeshProUGUI>().text = "";

        listObjectRef.destroyListSelection();
        setBtnStatus(true);

        if (lastSelectedOption == "skill")
        {
            StartCoroutine(ListLayout.selectOption(skillBtnObj));
        }
        else if (lastSelectedOption == "item")
        {
            StartCoroutine(ListLayout.selectOption(itemBtnObj));
        }
    }

    public void onStatusBackBtn()
    {
        statusListRef.GetComponent<ListLayout>().destroyListSelection();
        status.SetActive(false);
        if (lastSelectedStatusBtn == "player")
        {
            StartCoroutine(ListLayout.selectOption(playerstatusbtn));
        }
        else if (lastSelectedStatusBtn == "enemy")
        {
            StartCoroutine(ListLayout.selectOption(enemystatusbtn));
        }
    }

    public void onItemBtn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        if (InventoryManager.GetInstance().checkInventoryEmpty("use"))
        {
            string[] tempmsg = { "You don't have any items!", "Choose an action!" };
            StartCoroutine(updateMsg(tempmsg));
            return;
        }

        if (statuspanel.activeInHierarchy)
        {

            setBtnStatus(false);

            actionpanel.SetActive(true);
            statuspanel.SetActive(false);

            listObjectRef.createBattleItemList(InventoryManager.GetInstance().inventory);

            StartCoroutine(ListLayout.selectOption(backBtnObj));

            lastSelectedOption = "item";
        }
    }

    public void onRunBtn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        player.setCurrent_Health(player.getStat_MaxHealth());
        SceneManager.LoadScene(GameStateManager.GetInstance().lastscene);
        GameStateManager.GetInstance().battleRun = true;
    }

    void setBtnStatus(bool btnStatus)
    {
        attackBtn.interactable= btnStatus;
        skillBtn.interactable = btnStatus;
        itemBtn.interactable = btnStatus;
        runBtn.interactable = btnStatus;
    }

    public void onPlayerStatusBtn()
    {
        statusTitle.GetComponent<TextMeshProUGUI>().text = "Player status";
        status.SetActive(true);
        if (StatusManager.GetInstance().checkStatusEmpty())
        {
            statusempty.SetActive(true);
            statusleft.SetActive(false);
            statusright.SetActive(false);
            StartCoroutine(ListLayout.selectOption(statusBackBtn));
        }
        else
        {
            statusleft.SetActive(true);
            statusDescription.GetComponent<TextMeshProUGUI>().text = "";
            statusright.SetActive(true);
            statusListRef.GetComponent<ListLayout>().createBattleStatusList(StatusManager.GetInstance().statusList);
            
        }
        lastSelectedStatusBtn = "player";
    }

    public void onEnemyStatusBtn()
    {
        statusTitle.GetComponent<TextMeshProUGUI>().text = "Enemy status";
        status.SetActive(true);
        if (enemystatus.Count==0)
        {
            statusempty.SetActive(true);
            statusleft.SetActive(false);
            statusright.SetActive(false);
            StartCoroutine(ListLayout.selectOption(statusBackBtn));
            
        }
        else
        {
            statusleft.SetActive(true);
            statusDescription.GetComponent<TextMeshProUGUI>().text = "";
            statusright.SetActive(true);
            statusListRef.GetComponent<ListLayout>().createBattleStatusList(enemystatus);
        }
        lastSelectedStatusBtn = "enemy";
    }

    public void onUseBtn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //setBtnStatus(false);


        if (lastSelectedOption == "skill")
        {
            //use skill based on lastSelectedName
            Debug.Log("Use skill " + lastSelectedName);
            Skill tempskill = null;
            tempskill= SkillManager.GetInstance().getSkillWName(lastSelectedName);

            if (player.getCurrent_MentalPoint() - tempskill.skillData.skill_cost < 0)
            {

                string[] temp = {"You don't have enough MP!"};


                StartCoroutine(updateMsg(temp));

                
                //setBtnStatus(true);
            }
            else
            {
                if (actionpanel.activeInHierarchy)
                {
                    statuspanel.SetActive(true);
                    actionpanel.SetActive(false);
                    detailPanel_usebtn.SetActive(false);
                    detailPanel_description.SetActive(false);
                    listObjectRef.destroyListSelection();
                    descriptionText.GetComponent<TextMeshProUGUI>().text = "";
                }

                switch (tempskill.skillData.skill_type)
                {
                    case "damage":
                        useSkill = true;
                        player.updatePlayerMP(tempskill);
                        playerHUD.setPlayerMP(player.getCurrent_MentalPoint(), player);
                        StartCoroutine(playerAttack(tempskill));
                        break;
                    case "heal":
                        int temp= player.playerSkillHeal(tempskill);
                        string[] healmsg = { "Player has used " + tempskill.skillData.skill_name + "!", "Player has recovered "+temp+" HP!" };
                        player.updatePlayerMP(tempskill);
                        playerHUD.setPlayerMP(player.getCurrent_MentalPoint(), player);
                        playerHUD.setPlayerHP(player.getCurrent_Health(), player);

                        StartCoroutine(actionChoiceMsg(healmsg));
                        break;
                    case "cure":
                        player.updatePlayerMP(tempskill);
                        playerHUD.setPlayerMP(player.getCurrent_MentalPoint(), player);

                        StatusManager.GetInstance().skill_CureStatus(tempskill);
                        string[] curemsg = {"Player has used "+tempskill.skillData.skill_name+"!","The abnormal status "+StatusManager.GetInstance().getStatusWID(tempskill.skillData.skill_statusID)+" has been cured"};

                        StartCoroutine(actionChoiceMsg(curemsg));
                        break;
                    case "buff":
                        player.updatePlayerMP(tempskill);
                        playerHUD.setPlayerMP(player.getCurrent_MentalPoint(), player);

                        StatusManager.GetInstance().skill_InflictBuffStatus(tempskill);
                        string[] buffmsg = { "The player used "+tempskill.skillData.skill_name+"!", "You feel that you have gotten stronger!"};

                        StartCoroutine(actionChoiceMsg(buffmsg));
                        break;
                    case "debuff":
                        player.updatePlayerMP(tempskill);
                        playerHUD.setPlayerMP(player.getCurrent_MentalPoint(), player);

                        enemy_InflictDebuffStatus(tempskill);
                        string[] debuffmsg = { "The player used " + tempskill.skillData.skill_name + "!", "You have weakened the enemy" };

                        StartCoroutine(actionChoiceMsg(debuffmsg));
                        break;
                }
            }

            

        }
        else if (lastSelectedOption == "item")
        {
            Debug.Log("Use item " + lastSelectedName);
            InventoryItem tempitem = null;
            tempitem=InventoryManager.GetInstance().getItemWName(lastSelectedName);

            if (tempitem.itemData.item_type == "use")
            {

                if (actionpanel.activeInHierarchy)
                {
                    statuspanel.SetActive(true);
                    actionpanel.SetActive(false);
                    detailPanel_usebtn.SetActive(false);
                    detailPanel_description.SetActive(false);
                    listObjectRef.destroyListSelection();
                    descriptionText.GetComponent<TextMeshProUGUI>().text = "";
                }

                switch (tempitem.itemData.item_usetype)
                {
                    case "heal":
                        int temp=player.playerItemHeal(tempitem);
                        string[] healmsg = { "Player has used " + tempitem.itemData.item_name + "!", "Player has recovered " + temp + " HP!" };
                        playerHUD.setPlayerHP(player.getCurrent_Health(), player);
                        StartCoroutine(actionChoiceMsg(healmsg));
                        break;
                    case "cure":
                        StatusManager.GetInstance().item_CureStatus(tempitem);
                        string[] curemsg = { "Player has used " + tempitem.itemData.item_name + "!", "The abnormal status " + StatusManager.GetInstance().getStatusWID(tempitem.itemData.item_statusID) + " has been cured" };
                        StartCoroutine(actionChoiceMsg(curemsg));
                        
                        break;
                }
            }

            InventoryManager.GetInstance().inventory.Remove(tempitem);
            //use item based on lastSelectedName
        }
    }

    private IEnumerator updateMsg(string[] msg)
    {
        //charge strong attack
        //engage battle
        //use certain skill

        //or use dialogue

        if (actionpanel.activeInHierarchy)
        {
            statuspanel.SetActive(true);
            actionpanel.SetActive(false);
        }

        for (int x = 0; x < msg.Length; x++)
        {
            battle_status.text = msg[x];

            yield return new WaitForSeconds(2f);
        }

        if (statuspanel.activeInHierarchy)
        {
            statuspanel.SetActive(false);
            actionpanel.SetActive(true);
        }

    }

    private IEnumerator actionChoiceMsg(string[] msg)
    {
        //charge strong attack
        //engage battle
        //use certain skill

        //or use dialogue
        for (int x = 0; x < msg.Length; x++)
        {
            battle_status.text = msg[x];

            yield return new WaitForSeconds(2f);
        }

        state = BattleState.ENEMYTURN;
        StartCoroutine(enemyTurn());

    }

    private IEnumerator actionChoiceMsg_E(string[] msg)
    {
        //charge strong attack
        //engage battle
        //use certain skill

        //or use dialogue
        for (int x = 0; x < msg.Length; x++)
        {
            battle_status.text = msg[x];

            yield return new WaitForSeconds(2f);
        }

        state = BattleState.PLAYERTURN;
        playerTurn();

    }

    private IEnumerator endBattleChoiceMsg_E(string[] msg)
    {
        //charge strong attack
        //engage battle
        //use certain skill

        //or use dialogue
        for (int x = 0; x < msg.Length; x++)
        {
            battle_status.text = msg[x];

            yield return new WaitForSeconds(2f);
        }

        StatusManager.GetInstance().statusList.Clear();
        SceneManager.LoadScene(GameStateManager.GetInstance().lastscene);
        GameStateManager.GetInstance().battleRun = true;


    }

    private void enemy_InflictBuffStatus(EnemySkill enemyskill)
    {
        StatusData teststatus = Resources.Load<StatusData>("Status/status" + enemyskill.enemyskill_statusID);
        Status status = new Status(teststatus);

        bool exist = false;
        int index = 0;

        //if already have reset duration
        for (int x = 0; x < enemystatus.Count; x++)
        {
            if (enemystatus.ElementAt(x).statusData.status_ID == enemyskill.enemyskill_statusID)
            {
                exist = true;
                index = x;
                return;
            }
        }

        if (exist)
        {
            enemystatus.ElementAt(index).current_duration = enemystatus.ElementAt(index).statusData.status_duration;
        }
        else
        {
            enemystatus.Add(status);
        }

    }

    private void enemy_InflictDebuffStatus(Skill skill)
    {
        StatusData teststatus = Resources.Load<StatusData>("Status/status" + skill.skillData.skill_statusID);
        Status status = new Status(teststatus);

        bool exist = false;
        int index = 0;

        //if already have reset duration
        for (int x = 0; x < enemystatus.Count; x++)
        {
            if (enemystatus.ElementAt(x).statusData.status_ID == skill.skillData.skill_statusID)
            {
                exist = true;
                index = x;
                return;
            }
        }

        if (exist)
        {
            enemystatus.ElementAt(index).current_duration = enemystatus.ElementAt(index).statusData.status_duration;
        }
        else
        {
            enemystatus.Add(status);
        }

    }

    public Status getEnemyStatusWName(string name)
    {
        int index = 0;


        index = enemystatus.FindIndex(x => x.statusData.status_name == name);

        return enemystatus.ElementAt(index);
    }
}