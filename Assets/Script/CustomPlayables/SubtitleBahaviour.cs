using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using TMPro;
using UnityEngine;


public class SubtitleBahaviour : PlayableBehaviour
{
    public string subtitleText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI cutscenetext = playerData as TextMeshProUGUI;

        

        var progress = (float)(playable.GetTime()+0.5/ playable.GetDuration()-1);
        var subStringLength = Mathf.RoundToInt(Mathf.Clamp01(progress) * subtitleText.Length);
        cutscenetext.text = subtitleText.Substring(0,subStringLength);
        cutscenetext.color = new Color(1, 1, 1, info.weight);

        //TextMeshProUGUI cutscenetext = TimelineManager.GetInstance().overlayText;


    }



}
