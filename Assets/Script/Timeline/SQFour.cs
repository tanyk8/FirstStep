using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SQFour : MonoBehaviour
{
    public string sqprogress4 = "progress0";

    // Start is called before the first frame update
    void Start()
    {

        if (DialogueVariableObserver.variables["quest3_progress"].ToString() == "3")
        {
            PlayableAsset sqCutscene = Resources.Load<PlayableAsset>("Timeline/SQ_3");
            TimelineManager.GetInstance().playTimeline(sqCutscene);
            sqprogress4 = "progress1";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && sqprogress4 == "progress1")
        {
            sqprogress4 = "progress2";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/HospitalNPC");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }
        else if (!DialogueManager.GetInstance().dialogueIsPlaying && sqprogress4 == "progress2" && DialogueVariableObserver.variables["quest3_progress"].ToString() == "4")
        {
            SceneManager.LoadScene("ThirdStage_roomone");
        }
    }
}
