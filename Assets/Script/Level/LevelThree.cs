using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject shadowaura;

    public bool continuestory = false;
    public bool callonce = true;

    // Start is called before the first frame update
    void Start()
    {
        if (DialogueVariableObserver.variables["mainquest_progress"].ToString() == "12" && ProgressManager.GetInstance().gameProgress == "progress21")
        {
            ProgressManager.GetInstance().gameProgress = "progress22";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart4");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }

        if (GameStateManager.GetInstance().lastentrance == "hospitalhall1")
        {
            player.transform.position = new Vector3(3.125f, -0.2f, 0);
        }
        else if (GameStateManager.GetInstance().lastentrance == "hospitalhall2")
        {
            player.transform.position = new Vector3(-3.125f, -0.2f, 0);
        }
        else
        {
            player.transform.position = new Vector3(1f, 0.1f, 0);
        }

        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }
        if ( SoundManager.GetInstance().musicSource.clip.name != "bgm_stage3")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage3"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
        }

        if (!SoundManager.GetInstance().musicSource.isPlaying && SoundManager.GetInstance().musicSource.clip.name != "bgm_stage3")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage3"));
        }

        if (!shadowaura.activeInHierarchy && DialogueVariableObserver.variables["shadow3_escaped"].ToString() == "true" && DialogueVariableObserver.variables["shadow3_defeated"].ToString() == "false")
        {
            shadowaura.SetActive(true);
        }
        else if (shadowaura.activeInHierarchy && (DialogueVariableObserver.variables["shadow3_escaped"].ToString() == "false" || DialogueVariableObserver.variables["shadow3_defeated"].ToString() == "true"))
        {
            shadowaura.SetActive(false);
        }
    }
}
