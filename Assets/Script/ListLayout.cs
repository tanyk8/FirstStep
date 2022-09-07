using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Ink.Runtime;
using System.Linq;


public static class BtnExtension
{
    public static void AddEventListener<T>(this Button button, T param, System.Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }



}

public class ListLayout : MonoBehaviour
{

    private int totalElements;


    [Header("ListLayout")]
    [SerializeField] GameObject template_listitem;
    [SerializeField] GameObject panel_list;
    [SerializeField] Transform panel_listT;

    [Header("BattleManagerSkillPanelBack")]
    [SerializeField] GameObject backbtn;

    [Header("DialogueManager")]
    [SerializeField] GameObject dialoguemanagerobj;

    public ListLayout()
    {
        totalElements = 0;
    }

    public void createBattleSkillList()
    {
        totalElements = 10;


        for (int i = 0; i < totalElements; i++)
        {
            panel_list = Instantiate(template_listitem, panel_listT);
            panel_list.transform.GetChild(0).GetComponent<Text>().text = "item " + i;

            panel_list.GetComponent<Button>().AddEventListener(i, listItemClicked);
        }

        Navigation NewNavback = new Navigation();
        NewNavback.mode = Navigation.Mode.Explicit;

        //Set what you want to be selected on down, up, left or right;
        NewNavback.selectOnUp = panel_listT.GetChild(totalElements-1).GetComponent<Button>();
        NewNavback.selectOnDown = panel_listT.GetChild(0).GetComponent<Button>();

        //Assign the new navigation to your desired button or ui Object
        backbtn.GetComponent<Button>().navigation = NewNavback;



        for (int i = 0; i < totalElements; i++)
        {

            if (i == 0)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                NewNav.selectOnUp = backbtn.GetComponent<Button>();
                NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();


                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
            else if (i > 0&&i<totalElements-1)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                NewNav.selectOnUp = panel_listT.GetChild(i - 1).GetComponent<Button>();
                NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();

                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
            else if (i == totalElements-1)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                NewNav.selectOnUp = panel_listT.GetChild(i - 1).GetComponent<Button>();
                NewNav.selectOnDown = backbtn.GetComponent<Button>();

                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
        }

        StartCoroutine(selectFirstOption(backbtn));
    }

    public void createChoiceList(int numChoice,List<Choice> choiceList)
    {
        for (int i = 0; i < numChoice; i++)
        {
            panel_list = Instantiate(template_listitem, panel_listT);
            panel_list.transform.GetChild(0).GetComponent<Text>().text = choiceList.ElementAt(i).text;
            //panel_list.transform.GetChild(0).GetComponent<Text>().text = "choice " + i;


            panel_list.GetComponent<Button>().AddEventListener(i, listChoiceClicked);
        }

        for (int i = 0; i < numChoice; i++)
        {

            if (i == 0)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                NewNav.selectOnUp = panel_listT.GetChild(numChoice - 1).GetComponent<Button>();
                NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();


                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
            else if (i > 0 && i < numChoice - 1)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                NewNav.selectOnUp = panel_listT.GetChild(i - 1).GetComponent<Button>();
                NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();

                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
            else if (i == numChoice - 1)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                NewNav.selectOnUp = panel_listT.GetChild(i - 1).GetComponent<Button>();
                NewNav.selectOnDown = panel_listT.GetChild(0).GetComponent<Button>();

                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
        }
        StartCoroutine(selectFirstOption(panel_listT.GetChild(0).gameObject));
    }

    public void destroyListSelection()
    {
        foreach (Transform child in panel_listT)
        {
            Destroy(child.gameObject);
        }
    }

    void listItemClicked(int itemIndex)
    {
        Debug.Log("item " + itemIndex + " clicked");
    }

    void listChoiceClicked(int choiceIndex)
    {

        string tempText = panel_listT.GetChild(choiceIndex).GetComponentInChildren<Text>().text;


        if (tempText== "Quest")
        {
            dialoguemanagerobj.GetComponent<DialogueManager>().MakeChoice(choiceIndex,tempText);
        }
        else
        {
            dialoguemanagerobj.GetComponent<DialogueManager>().MakeChoice(choiceIndex,tempText);
        }
        

    }


    public static IEnumerator selectFirstOption(GameObject toselectgameobj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(toselectgameobj);
    }
}