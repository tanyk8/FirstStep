using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Progress
{
    [SerializeField] GameObject questmanager;

    [Header("LoadGlobalsJSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    public ProgressData progressData;

    public string inkVariableData;

    public string saveDate;
    public string saveTime;
    public string currentgameprogress;
    public bool stageOne_newpos;

    private DialogueVariableObserver dialoguevariableobserver;
    public Progress()
    {
        progressData = new ProgressData();
        inkVariableData = "";
        saveDate = "";
        saveTime = "";
    }

    public void updateProgress()
    {
        Player player = Player.GetInstance();

        progressData.player_maxhealth=player.getStat_MaxHealth();
        progressData.player_maxmp= player.getStat_MaxMentalPoint();
        progressData.player_power= player.getStat_MentalPower();
        progressData.player_protection= player.getStat_MentalProtection();
        progressData.player_currhealth= player.getCurrent_Health();
        progressData.player_currmp= player.getCurrent_MentalPoint();

        progressData.playerlocation = GameObject.Find("Player").transform.position;
        progressData.sceneName = SceneManager.GetActiveScene().name;

        progressData.skillList = SkillManager.GetInstance().skilllist;
        progressData.statusList = StatusManager.GetInstance().statusList;

        progressData.inventoryList = InventoryManager.GetInstance().inventory;
        //progressData.itemDictionary = InventoryManager.GetInstance().itemDictionary;

        progressData.questList = QuestManager.GetInstance().questlist;
        //progressData.questManager = QuestManager.GetInstance().getQuestManager();
        //progressData.questManager.questlist = new List<Quest>();
        //foreach(Quest quest in QuestManager.GetInstance().questlist)
        //{
        //    progressData.questList.Add(quest);
        //}

        //string sourceinkpath = Application.dataPath + Path.AltDirectorySeparatorChar + "Script" + Path.AltDirectorySeparatorChar + "Dialogue" + Path.AltDirectorySeparatorChar + "globals.ink";
        //inkVariableData = File.ReadAllText(sourceinkpath);


        inkVariableData=DialogueVariableObserver.saveVariables();



        saveDate = System.DateTime.Now.ToString("dd/MM/yyyy");
        saveTime = System.DateTime.Now.ToString("HH:mm:ss");

        currentgameprogress = ProgressManager.GetInstance().gameProgress;
        stageOne_newpos = ProgressManager.GetInstance().stageOne_newpos;
    }



}

