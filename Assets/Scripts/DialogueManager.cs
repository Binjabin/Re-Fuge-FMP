using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager instance;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;
    private Story currentStory;
    bool dialogueIsPlaying;
    List<Choice> currentChoices;
    List<string> tags;

    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator merchantAnimator;
    [SerializeField] Animator mysteriousAnimator;
    Animator anim;
    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("More than 1 dialogue manager!");
        }
        instance = this;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (!dialogueIsPlaying) { return; }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(currentChoices.Count < 1)
            {
                ContinueStory();
            }
            
        }
    }

    void ParseTags()
    {
        tags = currentStory.currentTags;
        foreach (string t in tags)
        {
            string player = t.Split(' ')[0];
            string prefix = t.Split(' ')[1];
            string param = t.Split(' ')[2];
            Debug.Log(player.ToString() + prefix.ToString() + param.ToString());
            if (prefix.ToLower() == "event")
            {
                if (param == "openShop")
                {
                    Debug.Log("open");
                    ExitDialogue();
                    FindObjectOfType<Inventory>().inShop = true;
                }

            }
            else
            {
                switch (player.ToLower())
                {
                    case "player":
                        anim = playerAnimator;
                        break;
                    case "merchant":
                        anim = merchantAnimator;
                        break;
                    case "mysterious":
                        anim = mysteriousAnimator;
                        break;
                }
                if (prefix == "anim")
                {
                    anim.Play(param);
                }
            }
            

        }
    }

    public void EnterDialogue(TextAsset json)
    {
        currentStory = new Story(json.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }
    void ExitDialogue()
    {
        FindObjectOfType<PlayerMovement>().ExitDialogue();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
            ParseTags();
        }
        else
        {
            ExitDialogue();
        }
    }

    void DisplayChoices()
    {
        currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than the UI can support.");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
            choicesText[i].text = "";
        }
    }
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
}
