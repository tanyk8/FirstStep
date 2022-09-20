using System.IO;
using UnityEngine;
using System.Linq;

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
        for(int x = 0; x < TOTALSLOT; x++)
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

        loadProgress(data);
        //Debug.Log(data.progressData.questList.Count);
        //Debug.Log(data.progressData.questList.ElementAt(0).questState);
        //Debug.Log(data.progressData.questList.ElementAt(0).questData.quest_progress[0].description);
    }

    public void loadProgress(Progress progress)
    {
        ProgressData progdata = progress.progressData;

        DialogueVariableObserver.loadVariables(progress.inkVariableData);

        SkillManager.GetInstance().updateSkill(progdata.skillList);
        
        StatusManager.GetInstance().updateStatus(progdata.statusList);

        Player.GetInstance().updatePlayer(progdata.player_maxhealth, progdata.player_maxmp, progdata.player_power, progdata.player_protection, progdata.player_currhealth, progdata.player_currmp);
        InventoryManager.GetInstance().updateInventory(progdata.inventoryList, progdata.itemDictionary);
        QuestManager.GetInstance().updateQuestManager(progdata.questList);

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
