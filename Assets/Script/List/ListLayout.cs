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

    [Header("Skill")]
    [SerializeField] GameObject menuSkillDescription;
    [SerializeField] GameObject menuSkillDescPanel;

    [Header("Quest")]
    [SerializeField] GameObject mainQuestBtn;
    [SerializeField] GameObject subQuestBtn;
    [SerializeField] TextMeshProUGUI questname;
    [SerializeField] TextMeshProUGUI questdesc;
    [SerializeField] TextMeshProUGUI questprogress;
    [SerializeField] GameObject questRightPanel;

    [Header("Item")]
    [SerializeField] GameObject itemname;
    [SerializeField] GameObject itemdesc;
    [SerializeField] GameObject itembtn;
    [SerializeField] GameObject itemusebtn;

    [SerializeField] GameObject itemDescPanel;
    [SerializeField] GameObject itemDescription;
    [SerializeField] GameObject inventoryUseItemBtn;
    [SerializeField] Transform itemListT;
    [SerializeField] GameObject itemRightPanel;


    [Header("Battle")]
    [SerializeField] GameObject battleUseBtn;
    [SerializeField] GameObject descriptionText;
    [SerializeField] GameObject detailPanel_useBtn;
    [SerializeField] GameObject detailPanel_description;

    [SerializeField] GameObject statusBackBtn;
    [SerializeField] GameObject statusDescription;
    [SerializeField] GameObject statusDescriptionPanel;

    [SerializeField] GameObject statusActor;
    

    public ListLayout()
    {
        totalElements = 0;
    }

    public void createBattleSkillList(List<Skill> skillList,string type)
    {
        Skill skill = null;

        int count = 0;

        if (type == "notlearnt")
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList.ElementAt(i).skillLearnt == false)
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = skillList.ElementAt(i).skillData.skill_name;

                    panel_list.GetComponent<Button>().AddEventListener(count, listBattleSkillClicked);
                    count++;

                    if (count == 1)
                    {
                        skill = skillList.ElementAt(count - 1);
                    }
                }


            }
        }
        else if (type == "learnt")
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList.ElementAt(i).skillLearnt == true)
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = skillList.ElementAt(i).skillData.skill_name;

                    panel_list.GetComponent<Button>().AddEventListener(i, listBattleSkillClicked);
                    count++;

                    if (count == 1)
                    {
                        skill = skillList.ElementAt(count - 1);
                    }
                }


            }


        }

        if (count != 0)
        {
            Navigation NewNavback = new Navigation();
            NewNavback.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNavback.selectOnUp = panel_listT.GetChild(count - 1).GetComponent<Button>();
            NewNavback.selectOnDown = panel_listT.GetChild(0).GetComponent<Button>();


            //Assign the new navigation to your desired button or ui Object
            backbtn.GetComponent<Button>().navigation = NewNavback;
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
                    NewNav.selectOnUp = backbtn.GetComponent<Button>();
                    NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                }
                else if (count == 1)
                {
                    NewNav.selectOnUp = backbtn.GetComponent<Button>();
                    NewNav.selectOnDown = backbtn.GetComponent<Button>();
                }



                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
            else if (i > 0&&i<count-1)
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
            else if (i == count-1)
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

    public void createSkillList(List<Skill> skillList,string type)
    {
        Skill skill = null;

        int count = 0;

        //skillList.Sort((p,q)=>p.skillData.skill_name.CompareTo(q.skillData.skill_name));
        //skillList.Sort((p, q) => p.skillData.skill_type.CompareTo(q.skillData.skill_type));


        if (type == "notlearnt")
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList.ElementAt(i).skillLearnt==false)
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = skillList.ElementAt(i).skillData.skill_name;

                    panel_list.GetComponent<Button>().AddEventListener(count, listSkillClicked);
                    count++;

                    if (count == 1)
                    {
                        skill = skillList.ElementAt(count - 1);
                    }
                }


            }
        }
        else if (type == "learnt")
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList.ElementAt(i).skillLearnt==true)
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = skillList.ElementAt(i).skillData.skill_name;

                    panel_list.GetComponent<Button>().AddEventListener(count, listSkillClicked);
                    count++;

                    if (count == 1)
                    {
                        skill = skillList.ElementAt(count - 1);
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

            else if (i > 0 && i < count - 1)
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
            else if (i == count - 1)
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

    public void createStatusList(List<Status> statusList)
    {
        //Skill skill = null;

        int count = 0;

        //skillList.Sort((p,q)=>p.skillData.skill_name.CompareTo(q.skillData.skill_name));
        //skillList.Sort((p, q) => p.skillData.skill_type.CompareTo(q.skillData.skill_type));

        count = statusList.Count;
        
    
        for (int i = 0; i < count; i++)
        {

            panel_list = Instantiate(template_listitem, panel_listT);
            panel_list.transform.GetChild(0).GetComponent<Text>().text = statusList.ElementAt(i).statusData.status_name;

            panel_list.GetComponent<Button>().AddEventListener(i, listStatusClicked);

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

            else if (i > 0 && i < count - 1)
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
            else if (i == count - 1)
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

    public void createBattleStatusList(List<Status> statusList)
    {
        int count = 0;

        count=statusList.Count;

        for (int i = 0; i < count; i++)
        {
            panel_list = Instantiate(template_listitem, panel_listT);
            panel_list.transform.GetChild(0).GetComponent<Text>().text = statusList.ElementAt(i).statusData.status_name;

            panel_list.GetComponent<Button>().AddEventListener(i, listBattleStatusClicked);

        }

        if (count != 0)
        {
            Navigation NewNavback = new Navigation();
            NewNavback.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNavback.selectOnUp = panel_listT.GetChild(count - 1).GetComponent<Button>();
            NewNavback.selectOnDown = panel_listT.GetChild(0).GetComponent<Button>();


            //Assign the new navigation to your desired button or ui Object
            statusBackBtn.GetComponent<Button>().navigation = NewNavback;
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
                    NewNav.selectOnUp = statusBackBtn.GetComponent<Button>();
                    NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                }
                else if (count == 1)
                {
                    NewNav.selectOnUp = statusBackBtn.GetComponent<Button>();
                    NewNav.selectOnDown = statusBackBtn.GetComponent<Button>();
                }
                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }

            else if (i > 0 && i < count - 1)
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
            else if (i == count - 1)
            {
                //Create a new navigation
                Navigation NewNav = new Navigation();
                NewNav.mode = Navigation.Mode.Explicit;

                //Set what you want to be selected on down, up, left or right;
                NewNav.selectOnUp = panel_listT.GetChild(i - 1).GetComponent<Button>();
                NewNav.selectOnDown = statusBackBtn.GetComponent<Button>();

                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
        }

        StartCoroutine(selectOption(panel_listT.GetChild(0).gameObject));

        

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
                panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.item_name;

                panel_list.GetComponent<Button>().AddEventListener(count, listInventoryClicked);
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
                if (inventoryList.ElementAt(i).itemData.item_type=="use")
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.item_name;

                    panel_list.GetComponent<Button>().AddEventListener(count, listInventoryClicked);
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
                if (inventoryList.ElementAt(i).itemData.item_type == "etc")
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.item_name;

                    panel_list.GetComponent<Button>().AddEventListener(count, listInventoryClicked);
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
                if (inventoryList.ElementAt(i).itemData.item_type == "important")
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.item_name;

                    panel_list.GetComponent<Button>().AddEventListener(count, listInventoryClicked);
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
                    //if (type == "use")
                    //{
                    //    NewNav.selectOnRight = itemusebtn.GetComponent<Button>();
                    //    NewNav.selectOnLeft = itemusebtn.GetComponent<Button>();
                    //}
                }

                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;

                //Navigation useNav = new Navigation();
                //useNav.mode = Navigation.Mode.Explicit;
                //useNav.selectOnLeft = panel_listT.GetChild(i).GetComponent<Button>();
                //useNav.selectOnRight = panel_listT.GetChild(i).GetComponent<Button>();
            }

            else if (i > 0 && i < count - 1)
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
            else if (i == count - 1)
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

    public void createBattleItemList(List<InventoryItem> inventoryList)
    {
        InventoryItem inventoryItem = null;

        int count = 0;

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList.ElementAt(i).itemData.item_type == "use")
            {
                panel_list = Instantiate(template_listitem, panel_listT);
                panel_list.transform.GetChild(0).GetComponent<Text>().text = inventoryList.ElementAt(i).itemData.item_name;

                panel_list.GetComponent<Button>().AddEventListener(count, listBattleItemClicked);
                count++;

                if (count == 1)
                {
                    inventoryItem = inventoryList.ElementAt(count - 1);
                }
            }


        }

        if (count != 0)
        {
            Navigation NewNavback = new Navigation();
            NewNavback.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNavback.selectOnUp = panel_listT.GetChild(count - 1).GetComponent<Button>();
            NewNavback.selectOnDown = panel_listT.GetChild(0).GetComponent<Button>();


            //Assign the new navigation to your desired button or ui Object
            backbtn.GetComponent<Button>().navigation = NewNavback;
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
                    NewNav.selectOnUp = backbtn.GetComponent<Button>();
                    NewNav.selectOnDown = panel_listT.GetChild(i + 1).GetComponent<Button>();
                }
                else if(count==1)
                {
                    NewNav.selectOnUp = backbtn.GetComponent<Button>();
                    NewNav.selectOnDown = backbtn.GetComponent<Button>();
                }



                //Assign the new navigation to your desired button or ui Object
                panel_listT.GetChild(i).GetComponent<Button>().navigation = NewNav;
            }
            else if (i > 0 && i < count - 1)
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
            else if (i == count - 1)
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

    public void createQuestList(List<Quest> questList, string type)
    {
        //Quest quest= null;

        int count = 0;

        if (type == "inprog")
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList.ElementAt(i).questState==QuestState.INPROGRESS)
                {
                    panel_list = Instantiate(template_listitem, panel_listT);
                    panel_list.transform.GetChild(0).GetComponent<Text>().text = questList.ElementAt(i).questData.quest_name;

                    panel_list.GetComponent<Button>().AddEventListener(count, listQuestClicked);
                    count++;

                    //if (count == 1)
                    //{
                    //    quest = questList.ElementAt(count - 1);
                    //}
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

                    panel_list.GetComponent<Button>().AddEventListener(count, listQuestClicked);
                    count++;

                    //if (count == 1)
                    //{
                    //    quest = questList.ElementAt(count-1);
                    //}
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

            else if (i > 0 && i < count - 1)
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
            else if (i == count - 1)
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
        //string temp = "";


        //if (type == "inprog")
        //{
        //    questname.text = quest.questData.quest_name;
        //    questdesc.text = quest.questData.quest_description;


        //    for (int x = 0; x < quest.quest_progress; x++)
        //    {
        //        if (quest.questData.quest_totalprogress-1>x&&quest.quest_progress-1==x)
        //        {
        //            temp += quest.questData.quest_progress[x].description + "<br>";
        //        }
        //        else
        //        {
        //            temp += "<s>" + quest.questData.quest_progress[x].description + "</s><br>";
        //        }

        //    }
        //}
        //else if (type == "completed")
        //{
        //    questname.text = quest.questData.quest_name;
        //    questdesc.text = quest.questData.quest_description;
        //    for (int x = 0; x < quest.quest_progress-1; x++)
        //    {
        //        temp += "<s>" + quest.questData.quest_progress[x].description + "</s><br>";
        //    }
        //}
        
        //questprogress.text = temp;


    }

    public void createSaveLoadList(Progress[] progressArray)
    {
        //Progress progress = null;
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

    void listSkillClicked(int skillIndex)
    {
        MenuManager.GetInstance().lastSelectedSkill= EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        MenuManager.GetInstance().lastSelectedSkillIndex = skillIndex;

        Skill tempskill = null;
        tempskill = SkillManager.GetInstance().getSkillWName(MenuManager.GetInstance().lastSelectedSkill);

        menuSkillDescription.transform.GetComponent<TextMeshProUGUI>().text = MenuManager.GetInstance().lastSelectedSkill + tempskill.skillData.skill_description;
        menuSkillDescPanel.SetActive(true);
        StartCoroutine(selectOption(menuSkillDescPanel));

    }

    void listBattleSkillClicked(int skillindex)
    {
        //switch to use btn
        BattleManager.GetInstance().lastSelectedName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        BattleManager.GetInstance().lastSelectedIndex = skillindex;

        Skill tempskill = null;
        tempskill = SkillManager.GetInstance().getSkillWName(BattleManager.GetInstance().lastSelectedName);


        descriptionText.transform.GetComponent<TextMeshProUGUI>().text = BattleManager.GetInstance().lastSelectedName+"<br>"+tempskill.skillData.skill_description;
        detailPanel_useBtn.SetActive(true);
        detailPanel_description.SetActive(true);
        StartCoroutine(selectOption(battleUseBtn));

        //SkillManager.GetInstance().useSkill(skillindex, panel_list.transform.GetChild(skillindex).GetComponent<Text>().text);
    }

    void listBattleItemClicked(int itemindex)
    {
        BattleManager.GetInstance().lastSelectedName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        BattleManager.GetInstance().lastSelectedIndex = itemindex;

        InventoryItem tempitem = null;
        tempitem = InventoryManager.GetInstance().getItemWName(BattleManager.GetInstance().lastSelectedName);

        descriptionText.transform.GetComponent<TextMeshProUGUI>().text = BattleManager.GetInstance().lastSelectedName + "<br>" + tempitem.itemData.item_description;

        detailPanel_useBtn.SetActive(true);
        detailPanel_description.SetActive(true);
        StartCoroutine(selectOption(battleUseBtn));
    }

    void listBattleStatusClicked(int statusindex)
    {
        if(statusActor.GetComponent<TextMeshProUGUI>().text=="Player status")
        {
            BattleManager.GetInstance().lastSelectedStatus = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
            BattleManager.GetInstance().lastSelectedStatusIndex = statusindex;

            Status tempstatus = null;
            tempstatus = StatusManager.GetInstance().getStatusWName(BattleManager.GetInstance().lastSelectedStatus);



            statusDescriptionPanel.SetActive(true);
            statusDescription.transform.GetComponent<TextMeshProUGUI>().text = BattleManager.GetInstance().lastSelectedStatus + "<br>" + tempstatus.statusData.status_description;

            StartCoroutine(selectOption(statusDescription));
        }
        else if(statusActor.GetComponent<TextMeshProUGUI>().text == "Enemy status")
        {
            BattleManager.GetInstance().lastSelectedStatus = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
            BattleManager.GetInstance().lastSelectedStatusIndex = statusindex;

            Status tempstatus = null;
            tempstatus = BattleManager.GetInstance().getEnemyStatusWName(BattleManager.GetInstance().lastSelectedStatus);

            statusDescriptionPanel.SetActive(true);
            statusDescription.transform.GetComponent<TextMeshProUGUI>().text = BattleManager.GetInstance().lastSelectedStatus + "<br>" + tempstatus.statusData.status_description;

            StartCoroutine(selectOption(statusDescription));
        }

        
    }

    void listStatusClicked(int statusIndex)
    {
        MenuManager.GetInstance().lastSelectedStatus = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        MenuManager.GetInstance().lastSelectedStatusIndex = statusIndex;

        Status tempstatus = null;
        tempstatus = StatusManager.GetInstance().getStatusWName(MenuManager.GetInstance().lastSelectedStatus);

        menuSkillDescription.transform.GetComponent<TextMeshProUGUI>().text = MenuManager.GetInstance().lastSelectedSkill + tempstatus.statusData.status_description;
        menuSkillDescPanel.SetActive(true);
        StartCoroutine(selectOption(menuSkillDescPanel));
    }

    void listItemClicked(int itemIndex)
    {
        



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
        MenuManager.GetInstance().lastSelectedQuest = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        MenuManager.GetInstance().lastSelectedQuestIndex = questIndex;

        Quest tempquest = null;
        tempquest = QuestManager.GetInstance().getQuestWName(MenuManager.GetInstance().lastSelectedQuest);
        string temp = "";

        if (MenuManager.GetInstance().lastSelectedQuestBtn == "inprog")
        {
            questname.text = tempquest.questData.quest_name;
            questdesc.text = tempquest.questData.quest_description;


            for (int x = 0; x < tempquest.quest_progress; x++)
            {

                if (tempquest.questData.quest_totalprogress - 1 > x && tempquest.quest_progress - 1 == x)
                {
                    temp += tempquest.questData.quest_progress[x].description + "<br>";
                }
                else
                {
                    temp += "<s>" + tempquest.questData.quest_progress[x].description + "</s><br>";
                }

            }
        }
        else if (MenuManager.GetInstance().lastSelectedQuestBtn == "completed")
        {
            questname.text = tempquest.questData.quest_name;
            questdesc.text = tempquest.questData.quest_description;
            for (int x = 0; x < tempquest.quest_progress - 1; x++)
            {
                temp += "<s>" + tempquest.questData.quest_progress[x].description + "</s><br>";
            }
        }

        questprogress.text = temp;

        questRightPanel.SetActive(true);
        StartCoroutine(selectOption(questRightPanel));
    }

    void listInventoryClicked(int inventoryIndex)
    {
        Debug.Log("item " + inventoryIndex + " clicked");
        MenuManager.GetInstance().lastSelectedItem = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        MenuManager.GetInstance().lastSelectedItemIndex = inventoryIndex;

        InventoryItem tempitem = null;
        tempitem = InventoryManager.GetInstance().getItemWName(MenuManager.GetInstance().lastSelectedItem);

        itemRightPanel.SetActive(true);
        itemDescPanel.SetActive(true);
        itemDescription.transform.GetComponent<TextMeshProUGUI>().text = MenuManager.GetInstance().lastSelectedItem +"<br>"+ tempitem.itemData.item_description;

        if (tempitem.itemData.item_type == "use")
        {
            inventoryUseItemBtn.SetActive(true);
            StartCoroutine(selectOption(inventoryUseItemBtn));
        }
        else
        {
            StartCoroutine(selectOption(itemDescription));
        }
        

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
