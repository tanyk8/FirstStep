using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    private BattleState state;

    private Player player;
    private Enemy enemy;

    private string battle_encountermessage="A strong presences of negative energy has been sensed!";

    private ListLayout listObjectRef;

    private static BattleManager instance;

    [Header("Player")]
    [SerializeField] private GameObject playerGameObject;


    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;
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

                        listObjectRef.destroyListSelection();

                        setBtnStatus(true);

                        StartCoroutine(ListLayout.selectOption(skillBtnObj));
                    }
                    else if (lastSelectedOption == "item")
                    {
                        statuspanel.SetActive(true);
                        actionpanel.SetActive(false);

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
        listObjectRef = actionList.GetComponent<ListLayout>();


        
        state = BattleState.START;

        StartCoroutine(initiateBattle());
    }
   

    IEnumerator initiateBattle()
    {
        statuspanel.SetActive(true);
        setBtnStatus(false);

        GameObject enemyGameObject= Instantiate(enemyPrefab, enemyLocation);
        enemy = enemyGameObject.GetComponent<Enemy>();

        //player=playerGameObject.GetComponent<Player>();
        player = Player.GetInstance();

        battle_status.text = battle_encountermessage;

        enemyHUD.setEnemyHUD(enemy);
        playerHUD.setPlayerHUD(player);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    void playerTurn()
    {
        setBtnStatus(true);
        StartCoroutine(ListLayout.selectOption(attBtnObj));
        battle_status.text = "Choose an action!";
    }

    void endBattle()
    {
        if (state == BattleState.WON)
        {
            battle_status.text = "You have defeated the negative!";
        }
        else if (state == BattleState.LOST)
        {
            battle_status.text = "You have been defeated!";
        }
    }

    IEnumerator enemyTurn()
    {
        battle_status.text = enemy.getEnemyName()+" attacks!";
        yield return new WaitForSeconds(1f);

        int enemydmgdealt = 0;
        int probability = Random.Range(0, 20);

        if (probability <= 3)
        {
            enemydmgdealt = enemy.getStat_Damage() * 125 / 100;
            battle_status.text = "Enemy dealt " + enemydmgdealt + " critical damage!";
        }
        else if(probability==19){
            enemydmgdealt = 0;
            battle_status.text = "The enemy missed!";
        }
        else
        {
            enemydmgdealt = Random.Range(enemy.getStat_Damage() * 75 / 100, enemy.getStat_Damage() * 115 / 100);
            battle_status.text = "Enemy dealt " + enemydmgdealt + " damage";
        }

        float reducedmg = 0f;

        reducedmg = 100-player.getStat_MentalProtection()*0.2f;

        enemydmgdealt = (int)(enemydmgdealt*reducedmg/100);

        bool isDefeated = player.receiveDamage(enemydmgdealt);


        playerHUD.setPlayerHP(player.getCurrent_Health(),player);

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

    IEnumerator playerAttack()
    {
        if (actionpanel.activeInHierarchy)
        {
            statuspanel.SetActive(true);
            actionpanel.SetActive(false);

            listObjectRef.destroyListSelection();
        }

        battle_status.text = "Player chose to attack!";

        yield return new WaitForSeconds(1f);


        int playerdmgdealt = 0;
        int probability = Random.Range(0, 20);

        if ( probability<= 3)
        {
            playerdmgdealt = player.getStat_MentalPower() * 125 / 100;
            battle_status.text = "Enemy took " + playerdmgdealt + " critical damage!";
        }
        else if (probability==19)
        {
            playerdmgdealt = 0;
            battle_status.text = "You've missed!";
        }
        else
        {
            playerdmgdealt = Random.Range(player.getStat_MentalPower() * 75 / 100, player.getStat_MentalPower() * 115 / 100);
            battle_status.text = "Enemy took " + playerdmgdealt + " damage";
        }

        float reducedmg = 0f;

        reducedmg = 100 - enemy.getStat_Defence() * 0.2f;

        playerdmgdealt = (int)(playerdmgdealt * reducedmg / 100);


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
        StartCoroutine(playerAttack());
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

        SceneManager.LoadScene("Beginning");
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
            statusright.SetActive(true);
            statusListRef.GetComponent<ListLayout>().createBattleStatusList(enemystatus);
        }
        lastSelectedStatusBtn = "enemy";
    }

    public void onUseBtn()
    {
        if (lastSelectedOption == "skill")
        {
            //use skill based on lastSelectedName
            Debug.Log("Use skill " + lastSelectedName);
        }
        else if (lastSelectedOption == "item")
        {
            Debug.Log("Use item " + lastSelectedName);
            //use item based on lastSelectedName
        }
    }

    private IEnumerator updateMsg(string[] msg)
    {
        //charge strong attack
        //engage battle
        //use certain skill

        //or use dialogue
        for(int x = 0; x < msg.Length; x++)
        {
            battle_status.text = msg[x];

            yield return new WaitForSeconds(2f);
        }


        
    }
}