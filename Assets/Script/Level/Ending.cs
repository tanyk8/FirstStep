using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public string endprogress = "progress0";

    [SerializeField] PlayableDirector endingdirector;

    // Start is called before the first frame update
    void Start()
    {
        PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/Ending");
        endingdirector.Play(cutscene);
        endprogress = "progress1";
    }

    // Update is called once per frame
    void Update()
    {
        if (endprogress == "progress1"&&endingdirector.state != PlayState.Playing)
        {
            SceneManager.LoadScene("Credit");
        }
    }
}
