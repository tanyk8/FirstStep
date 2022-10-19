using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class ProgressManager : MonoBehaviour
{

    

    private static ProgressManager instance;

    
    private const int TOTALSLOT=20;

    public Progress[] progressArray = new Progress[TOTALSLOT];

    private string path = "";
    private string persistentpath = "";

    private string sourceinkpath = "";
    private string defaultinkpath = "";
    private string inkpath = "";

    public string gameProgress ="opening";

    public bool loaded;
    public Vector3 loadedposition;
    public bool loading;

    public bool stageOne_newpos=false;

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Debug.LogWarning("Found more than one Progress Manager in the scene");
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
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


    }

    public static ProgressManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        

        for (int x = 0; x < TOTALSLOT; x++)
        {
            progressArray[x] = new Progress();
        }
    }

    public void setPath(int index)
    {
        sourceinkpath = Application.dataPath + Path.AltDirectorySeparatorChar + "Script" + Path.AltDirectorySeparatorChar + "Dialogue" + Path.AltDirectorySeparatorChar + "globals.ink";
        defaultinkpath = Application.dataPath + Path.AltDirectorySeparatorChar + "Script" + Path.AltDirectorySeparatorChar + "Dialogue" + Path.AltDirectorySeparatorChar + "defaultglobals.ink";
        path = Application.dataPath + Path.AltDirectorySeparatorChar +"save"+Path.AltDirectorySeparatorChar+ "savedata" + index + ".json";
        persistentpath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "savedata" + index +".json";
        inkpath = Application.dataPath + Path.AltDirectorySeparatorChar + "save" + Path.AltDirectorySeparatorChar + "savedataI" + index + ".ink";
    }

    

    public void SavetoFile(int index)
    {
        setPath(index);
        string savePath = path;
        string saveinkpath = inkpath;

        progressArray[index].updateProgress();


        Debug.Log("Data saved at " + savePath);
        string json = JsonUtility.ToJson(progressArray[index]);
        Debug.Log(json);
        


        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);

        //string ink= File.ReadAllText(sourceinkpath);
        //File.WriteAllText(saveinkpath, ink);


        //using StreamReader readink = new StreamReader(sourceinkpath);
        //string ink = readink.ReadToEnd();
        //Debug.Log(sourceinkpath+" | "+ink);
        //using StreamWriter writerink = new StreamWriter(saveinkpath);
        //writer.Write(ink);


    }

    public void LoadfromFile(int index)
    {
        setPath(index);

        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        Progress data = JsonUtility.FromJson<Progress>(json);
        loading = true;
        loadProgress(data);
        loading = false;
        //Debug.Log(data.progressData.questList.Count);
        //Debug.Log(data.progressData.questList.ElementAt(0).questState);
        //Debug.Log(data.progressData.questList.ElementAt(0).questData.quest_progress[0].description);
    }

    public string loadDateTime(int index)
    {
        setPath(index);
        if (File.Exists(path))
        {
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();

            Progress data = JsonUtility.FromJson<Progress>(json);

            return data.saveDate + "|" + data.saveTime;
        }
        else
        {
            return "-";
        }

    }

    public void loadProgress(Progress progress)
    {
        SceneManager.LoadScene(progress.progressData.sceneName);
        Debug.Log("Load " + progress.progressData.sceneName);

        ProgressData progdata = progress.progressData;

        DialogueVariableObserver.loadVariables(progress.inkVariableData);


        Player.GetInstance().updatePlayer(progdata.player_maxhealth, progdata.player_maxmp, progdata.player_power, progdata.player_protection, progdata.player_currhealth, progdata.player_currmp);
        
        for(int x = 0; x < progdata.inventoryList.Count; x++)
        {
            ItemData tempItemData = Resources.Load<ItemData>("Item/item" + progdata.inventoryList.ElementAt(x).id);
            //InventoryManager.GetInstance().inventory.ElementAt(x).itemData = tempItemData;
            progdata.inventoryList.ElementAt(x).itemData = tempItemData;
            
        }
        InventoryManager.GetInstance().updateInventory(progdata.inventoryList, progdata.itemDictionary);

        
        for (int x = 0; x < progdata.questList.Count; x++)
        {
            QuestData tempQuestData = Resources.Load<QuestData>("Quest/quest" + progdata.questList.ElementAt(x).id);
            //QuestManager.GetInstance().questlist.ElementAt(x).questData = tempQuestData;
            progdata.questList.ElementAt(x).questData = tempQuestData;
            
        }
        QuestManager.GetInstance().updateQuestManager(progdata.questList);

        
        for (int x = 0; x < progdata.skillList.Count; x++)
        {
            SkillData tempSkillData = Resources.Load<SkillData>("Skill/skill" + progdata.skillList.ElementAt(x).id);
            //SkillManager.GetInstance().skilllist.ElementAt(x).skillData = tempSkillData;
            progdata.skillList.ElementAt(x).skillData = tempSkillData;
        }
        SkillManager.GetInstance().updateSkill(progdata.skillList);
        
        for (int x = 0; x < progdata.statusList.Count; x++)
        {
            StatusData tempStatusData = Resources.Load<StatusData>("Status/status" + progdata.statusList.ElementAt(x).id);
            //StatusManager.GetInstance().statusList.ElementAt(x).statusData = tempStatusData;
            progdata.statusList.ElementAt(x).statusData = tempStatusData;
        }
        StatusManager.GetInstance().updateStatus(progdata.statusList);

        loadedposition = progdata.playerlocation;
        gameProgress = progress.currentgameprogress;
        stageOne_newpos = progress.stageOne_newpos;


        loaded = true;

        


    }

    public void loadProgressArray()
    {
        string saveFolderPath = Application.dataPath + Path.AltDirectorySeparatorChar+"save";

        string[] files = System.IO.Directory.GetFiles(saveFolderPath);
        char[] chartotrim = { 's','v','e','d','a','t','.','j','s','o','n'};
        int tempindex;

        foreach(string file in files)
        {
            Debug.Log(file);
            tempindex=int.Parse(file.Trim(chartotrim));
        }
    }
}
