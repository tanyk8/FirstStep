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

    [Header("Quest")]
    [SerializeField] GameObject mainQuestBtn;
    [SerializeField] GameObject subQuestBtn;
    [SerializeField] TextMeshProUGUI questname;
    [SerializeField] TextMeshProUGUI questdesc;
    [SerializeField] TextMeshProUGUI questprogress;


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
                
                if (totalElements > 1)
                {
                    NewNav.selectOnUp = backbtn.GetComponent<Button>();
                    NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                }
                


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

        StartCoroutine(selectOption(backbtn));
    }

    public void createInventoryList(List<InventoryItem> inventoryList,string type)
    {
        InventoryItem inventoryItem = null;

        int count = 0;


        if (type=="all")
        {

            for (int i = 0; i < inventoryList.Count; i++)
            {
                panel_list = Instantiate(template_listitem, panel_listT);
                panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.itemName;

                panel_list.GetComponent<Button>().AddEventListener(i, listInventoryClicked);
                count++;

                if (count == 1)
                {
                    inventoryItem = inventoryList.ElementAt(count - 1);
                }
            }
        }

        else if (type == "use")
        {

            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList.ElementAt(i).itemData.itemType=="use")
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.itemName;

                    panel_list.GetComponent<Button>().AddEventListener(i, listInventoryClicked);
                    count++;

                    if (count == 1)
                    {
                        inventoryItem = inventoryList.ElementAt(count - 1);
                    }
                }


            }
        }
        else if (type == "etc")
        {
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList.ElementAt(i).itemData.itemType == "etc")
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.itemName;

                    panel_list.GetComponent<Button>().AddEventListener(i, listInventoryClicked);
                    count++;

                    if (count == 1)
                    {
                        inventoryItem = inventoryList.ElementAt(count - 1);
                    }
                }


            }
        }
        else if (type == "important")
        {
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList.ElementAt(i).itemData.itemType == "important")
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.itemName;

                    panel_list.GetComponent<Button>().AddEventListener(i, listInventoryClicked);
                    count++;

                    if (count == 1)
                    {
                        inventoryItem = inventoryList.ElementAt(count - 1);
                    }
                }


            }
        }

        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;

                if (count > 1)
                {
                    NewNav.selectOnUp = panel_listT.GetChild(count - 1).GetComponent<Button>();
                    NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                }



                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }

            else if (i > 0 && i < totalElements - 1)
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
            else if (i == totalElements - 1)
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

        StartCoroutine(selectOption(panel_listT.GetChild(0).gameObject));

    }

    public void createQuestList(List<Quest> questList, string type)
    {
        Quest quest= null;

        int count = 0;

        if (type == "inprog")
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList.ElementAt(i).questState==QuestState.INPROGRESS)
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = questList.ElementAt(i).questData.quest_name;

                    panel_list.GetComponent<Button>().AddEventListener(i, listQuestClicked);
                    count++;

                    if (count == 1)
                    {
                        quest = questList.ElementAt(count - 1);
                    }
                }


            }
        }
        else if (type == "completed")
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList.ElementAt(i).questState==QuestState.COMPLETED) { 
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = questList.ElementAt(i).questData.quest_name;

                    panel_list.GetComponent<Button>().AddEventListener(i, listQuestClicked);
                    count++;

                    if (count == 1)
                    {
                        quest = questList.ElementAt(count-1);
                    }
                }

                
            }


        }

        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                
                if (count > 1)
                {
                    NewNav.selectOnUp = panel_listT.GetChild(count - 1).GetComponent<Button>();
                    NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                }
                


                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }

            else if (i > 0 && i < totalElements - 1)
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
            else if (i == totalElements - 1)
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

        StartCoroutine(selectOption(panel_listT.GetChild(0).gameObject));
        string temp = "";


        if (type == "inprog")
        {
            questname.text = quest.questData.quest_name;
            questdesc.text = quest.questData.quest_description;


            for (int x = 0; x < quest.quest_progress; x++)
            {
                if (quest.questData.quest_totalprogress-1>x&&quest.quest_progress-1==x)
                {
                    temp += quest.questData.quest_progress[x].description + "<br>";
                }
                else
                {
                    temp += "<s>" + quest.questData.quest_progress[x].description + "</s><br>";
                }

            }
        }
        else if (type == "completed")
        {
            questname.text = quest.questData.quest_name;
            questdesc.text = quest.questData.quest_description;
            for (int x = 0; x < quest.quest_progress-1; x++)
            {
                temp += "<s>" + quest.questData.quest_progress[x].description + "</s><br>";
            }
        }
        
        questprogress.text = temp;


    }

    public void createSaveLoadList(Progress[] progressArray)
    {
        Progress progress = null;
        int total=progressArray.Length;
        Debug.Log(progressArray.Length);
        for (int i = 0; i < total; i++)
        {
            panel_list = Instantiate(template_listitem, panel_listT);
            panel_list.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Save Data "+i;

            panel_list.GetComponent<Button>().AddEventListener(i, listProgressClicked);
        }

        for (int i = 0; i < total; i++)
        {
            if (i == 0)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;


                NewNav.selectOnUp = panel_listT.GetChild(total - 1).GetComponent<Button>();
                NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                



                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }

            else if (i > 0 && i < totalElements - 1)
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
            else if (i == totalElements - 1)
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

        StartCoroutine(selectOption(panel_listT.GetChild(0).gameObject));
    }

    public void createChoiceList(int numChoice,List<Choice> choiceList)
    {
        for (int i = 0; i < numChoice; i++)
        {
            panel_list = Instantiate(template_listitem, panel_listT);
            panel_list.transform.GetChild(0).GetComponent<Text>().text = choiceList.ElementAt(i).text;



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
                
                if (numChoice > 1)
                {
                    NewNav.selectOnUp = panel_listT.GetChild(numChoice - 1).GetComponent<Button>();
                    NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                }
                


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
        StartCoroutine(selectOption(panel_listT.GetChild(0).gameObject));
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

    void listQuestClicked(int questIndex)
    {

    }

    void listInventoryClicked(int inventoryIndex)
    {

    }

    void listProgressClicked(int progressIndex)
    {
        if (MenuManager.GetInstance().lastSelectedSaveLoadBtn == "save")
        {
            Debug.Log("save "+ progressIndex);
            ProgressManager.GetInstance().SavetoFile(progressIndex);
        }
        else if(MenuManager.GetInstance().lastSelectedSaveLoadBtn=="load")
        {
            Debug.Log("load " + progressIndex);
            ProgressManager.GetInstance().LoadfromFile(progressIndex);
        }
    }


    public static IEnumerator selectOption(GameObject toselectgameobj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(toselectgameobj);
    }

}
