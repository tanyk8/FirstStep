using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject menuCanvas;

    [Header("ContentPanel")]
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject saveloadPanel;
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject returntotitlePanel;

    [Header("Content")]
    [SerializeField] GameObject overlay;

    [Header("Button")]
    [SerializeField] GameObject characterBtn;
    [SerializeField] GameObject inventoryBtn;
    [SerializeField] GameObject questBtn;
    [SerializeField] GameObject saveloadBtn;
    [SerializeField] GameObject helpBtn;
    [SerializeField] GameObject settingsBtn;
    [SerializeField] GameObject returntoTitleBtn;

    [Header("ContentBtn")]
    [SerializeField] GameObject char_aboutBtn;
    [SerializeField] GameObject inventory_allBtn;
    [SerializeField] GameObject quest_mainBtn;
    [SerializeField] GameObject saveload_saveBtn;
    [SerializeField] GameObject returntoTitle_NoBtn;


    private void Start()
    {
        menuCanvas.SetActive(false);
        overlay.SetActive(false);
    }

    void Update()
    {

        if (InputManager.getInstance().getMenuPressed())
        {
            if (!menuCanvas.activeInHierarchy&& !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                menuCanvas.SetActive(true);
                StartCoroutine(ListLayout.selectFirstOption(characterBtn));
            }
            else if (overlay.activeInHierarchy) {
                if (characterPanel.activeInHierarchy)
                {
                    characterPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectFirstOption(characterBtn));
                }
                else if (inventoryPanel.activeInHierarchy)
                {
                    inventoryPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectFirstOption(inventoryBtn));
                }
                else if (questPanel.activeInHierarchy)
                {
                    questPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectFirstOption(questBtn));
                }
                else if (saveloadPanel.activeInHierarchy)
                {
                    saveloadPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectFirstOption(saveloadBtn));
                }
                else if (helpPanel.activeInHierarchy)
                {
                    helpPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectFirstOption(helpBtn));
                }
                else if (settingsPanel.activeInHierarchy)
                {
                    settingsPanel.SetActive(false);
                    StartCoroutine(ListLayout.selectFirstOption(settingsBtn));
                }
                else if (returntotitlePanel.activeInHierarchy)
                {
                    returntotitlePanel.SetActive(false);
                    StartCoroutine(ListLayout.selectFirstOption(returntoTitleBtn));
                }

                overlay.SetActive(false);
            }
            else
            {

                menuCanvas.SetActive(false);
            }
        }

    }

    public void onCloseBtn()
    {
        if (menuCanvas.activeInHierarchy)
        {
            menuCanvas.SetActive(false);
        }
    }

    public void onCharacterBtn()
    {
        if (!characterPanel.activeInHierarchy)
        {
            overlay.SetActive(true);
            characterPanel.SetActive(true);
            StartCoroutine(ListLayout.selectFirstOption(char_aboutBtn));
        }
    }

    public void onInventoryBtn()
    {
        if (!inventoryPanel.activeInHierarchy)
        {
            overlay.SetActive(true);
            inventoryPanel.SetActive(true);
            StartCoroutine(ListLayout.selectFirstOption(inventory_allBtn));
        }
    }

    public void onQuestBtn()
    {
        if (!questPanel.activeInHierarchy)
        {
            overlay.SetActive(true);
            questPanel.SetActive(true);
            StartCoroutine(ListLayout.selectFirstOption(quest_mainBtn));
        }
    }

    public void onSaveLoadBtn()
    {
        if (!saveloadPanel.activeInHierarchy)
        {
            overlay.SetActive(true);
            saveloadPanel.SetActive(true);
            StartCoroutine(ListLayout.selectFirstOption(saveload_saveBtn));
        }
    }

    public void onHelpBtn()
    {
        if (!helpPanel.activeInHierarchy)
        {
            overlay.SetActive(true);
            helpPanel.SetActive(true);
        }
    }

    public void onSettingsBtn()
    {
        if (!settingsPanel.activeInHierarchy)
        {
            overlay.SetActive(true);
            settingsPanel.SetActive(true);
        }
    }

    public void onReturntoTitleBtn()
    {
        if (!returntotitlePanel.activeInHierarchy)
        {
            overlay.SetActive(true);
            returntotitlePanel.SetActive(true);
            StartCoroutine(ListLayout.selectFirstOption(returntoTitle_NoBtn));
        }
    }

    public void onReturntoTitle_No()
    {
        if (returntotitlePanel.activeInHierarchy)
        {
            overlay.SetActive(false);
            returntotitlePanel.SetActive(false);
            StartCoroutine(ListLayout.selectFirstOption(returntoTitleBtn));
        }
    }

    public void onReturntoTitle_Yes()
    {

    }
}
