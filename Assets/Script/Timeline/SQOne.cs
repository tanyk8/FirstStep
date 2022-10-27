using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Linq;
using UnityEngine.SceneManagement;

public class SQOne : MonoBehaviour
{
    public string sqprogress="progress0";

    // Start is called before the first frame update
    void Start()
    {
        if (DialogueVariableObserver.variables["quest1_progress"].ToString() == "5")
        {
            PlayableAsset sqCutscene = Resources.Load<PlayableAsset>("Timeline/SQ_1");
            TimelineManager.GetInstance().playTimeline(sqCutscene);
            sqprogress = "progress1";
        }
        

        //play timeline
    }

    // Update is called once per frame
    void Update()
    {

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && sqprogress=="progress1")
        {
            sqprogress = "progress2";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/TutorialNPC");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }
        else if (!DialogueManager.GetInstance().dialogueIsPlaying&&sqprogress == "progress2"&& DialogueVariableObserver.variables["quest1_progress"].ToString() == "6")
        {
            SceneManager.LoadScene("FirstStage_Park");
        }
    }
}
