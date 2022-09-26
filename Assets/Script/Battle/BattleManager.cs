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
    [SerializeField] GameObject skillList;

    [Header("GameObject")]
    [SerializeField] GameObject attBtnObj;
    [SerializeField] GameObject skillBtnObj;
    [SerializeField] GameObject backBtnObj;

    [SerializeField] GameObject playerstatusbtn;
    [SerializeField] GameObject enemystatusbtn;


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
        }
    }

    public static BattleManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        listObjectRef = skillList.GetComponent<ListLayout>();


        
        state = BattleState.START;

        StartCoroutine(initiateBattle());
    }
   

    IEnumerator initiateBattle()
    {
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
        //EventSystem.current.SetSelectedGameObject(attBtnObj);
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


        if (statuspanel.activeInHierarchy)
        {

            setBtnStatus(false);

            actionpanel.SetActive(true);
            statuspanel.SetActive(false);

            //listObjectRef.createBattleSkillList();
            listObjectRef.createBattleSkillList(SkillManager.GetInstance().skilllist, "learnt");

            StartCoroutine(ListLayout.selectOption(backBtnObj));
            //EventSystem.current.SetSelectedGameObject(backBtnObj);
        }
    }

    public void onBackBtn()
    {
        statuspanel.SetActive(true);
        actionpanel.SetActive(false);

        listObjectRef.destroyListSelection();

        setBtnStatus(true);

        StartCoroutine(ListLayout.selectOption(skillBtnObj));
        //EventSystem.current.SetSelectedGameObject(skillBtnObj);
    }

    public void onItemBtn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
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

    }

    public void onEnemyStatusBtn()
    {

    }

    private IEnumerator enemyTalk()
    {
        //charge strong attack
        //engage battle
        //use certain skill

        //or use dialogue
        yield return new WaitForSeconds(1f);
    }
}