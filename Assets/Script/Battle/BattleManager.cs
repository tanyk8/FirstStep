using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    private BattleState state;

    private Player player;
    private Enemy enemy;

    private string battle_encountermessage="A strong presences of negative energy has been sensed!";

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

    //[Header("Button")]

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(initiateBattle());
    }

    IEnumerator initiateBattle()
    {
        GameObject enemyGameObject= Instantiate(enemyPrefab, enemyLocation);
        enemy = enemyGameObject.GetComponent<Enemy>();

        player=playerGameObject.GetComponent<Player>();


        battle_status.text = battle_encountermessage;

        enemyHUD.setEnemyHUD(enemy);
        playerHUD.setPlayerHUD(player);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    void playerTurn()
    {
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

        battle_status.text = "Enemy dealt " + enemy.getStat_Damage() + " damage";
        bool isDefeated = player.receiveDamage(enemy.getStat_Damage());

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
        bool isDefeated=enemy.receiveDamage(player.getStat_MentalPower());

        enemyHUD.setEnemyHP(enemy.getCurrent_Health(),enemy);

        battle_status.text = "Enemy took "+player.getStat_MentalPower()+" damage";

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

        StartCoroutine(playerAttack());
    }

    public void onSkillBtn()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
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
    }

}