using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;

    [SerializeField] GameObject shadowaura;

    public bool continuestory = false;
    public bool callonce = true;
    void Start()
    {
        if (DialogueVariableObserver.variables["mainquest_progress"].ToString() == "8" && ProgressManager.GetInstance().gameProgress == "progress11")
        {
            ProgressManager.GetInstance().gameProgress = "progress10";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart3");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }

        if (GameStateManager.GetInstance().lastentrance == "hall1")
        {
            player.transform.position = new Vector3(0.875f, -0.165f, 0);
        }
        else if (GameStateManager.GetInstance().lastentrance == "hall2")
        {
            player.transform.position = new Vector3(-1.2f, -0.165f, 0);
        }
        else if (GameStateManager.GetInstance().lastentrance == "hall3")
        {
            player.transform.position = new Vector3(-2.475f, -0.15f, 0);
        }

        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
        }

        if (!shadowaura.activeInHierarchy&&DialogueVariableObserver.variables["shadow2_escaped"].ToString() == "true"&& DialogueVariableObserver.variables["shadow2_defeated"].ToString() == "false")
        {
            shadowaura.SetActive(true);
        }
        else if(shadowaura.activeInHierarchy && (DialogueVariableObserver.variables["shadow2_escaped"].ToString() == "false" || DialogueVariableObserver.variables["shadow2_defeated"].ToString() == "true"))
        {
            shadowaura.SetActive(false);
        }
    }
}
