using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SQTwo : MonoBehaviour
{
    public string sqprogress2 = "progress0";

    // Start is called before the first frame update
    void Start()
    {

        if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "2")
        {
            PlayableAsset sqCutscene = Resources.Load<PlayableAsset>("Timeline/SQ_2");
            TimelineManager.GetInstance().playTimeline(sqCutscene);
            sqprogress2 = "progress1";
        }
        SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_memories"));
    }

    // Update is called once per frame
    void Update()
    {
        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && sqprogress2 == "progress1")
        {
            sqprogress2 = "progress2";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }
        else if (!DialogueManager.GetInstance().dialogueIsPlaying && sqprogress2 == "progress2" && DialogueVariableObserver.variables["quest2_progress"].ToString() == "3")
        {
            SceneManager.LoadScene("SecondStage_Classroom");
        }
    }
}
