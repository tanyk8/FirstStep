using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actorname;
    [SerializeField] private Slider slider_hp;
    [SerializeField] private TextMeshProUGUI hp_text;

    [SerializeField] private Gradient gradient;

    [SerializeField] private Image fill;

    public void setEnemyHUD(Enemy enemy)
    {
        //enemy
        actorname.text = enemy.getEnemyName();
        slider_hp.maxValue = enemy.getStat_MaxHealth();
        slider_hp.value = enemy.getCurrent_Health();
        hp_text.text = enemy.getCurrent_Health() + "/" + enemy.getStat_MaxHealth();

        fill.color = gradient.Evaluate(1f);
    }

    public void setPlayerHUD(Player player)
    {
        actorname.text = "Player";
        slider_hp.maxValue = player.getStat_MaxHealth();
        slider_hp.value = player.getCurrent_Health();
        hp_text.text = player.getCurrent_Health() + "/" + player.getStat_MaxHealth();
        fill.color = gradient.Evaluate(1f);
    }

    public void setEnemyHP(int hp,Enemy enemy)
    {
        slider_hp.value = hp;
        hp_text.text= enemy.getCurrent_Health() + "/" + enemy.getStat_MaxHealth();

        fill.color = gradient.Evaluate(slider_hp.normalizedValue);
    }

    public void setPlayerHP(int hp,Player player)
    {
        slider_hp.value = hp;
        hp_text.text = player.getCurrent_Health() + "/" + player.getStat_MaxHealth();
        fill.color = gradient.Evaluate(slider_hp.normalizedValue);
    }

}
