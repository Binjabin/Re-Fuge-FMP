using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    bool typing;
    static DialogueManager instance;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;
    private Story currentStory;
    public bool dialogueIsPlaying;
    List<Choice> currentChoices;
    List<string> tags;
    AudioSource audio;
    public bool showCostObject;
    string currentSentence;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator merchantAnimator;
    [SerializeField] Animator mysteriousAnimator;
    [SerializeField] Animator refugeeAnimator;
    Animator anim;

    [SerializeField] Color foodColor;
    [SerializeField] Color waterColor;
    [SerializeField] Color energyColor;
    [SerializeField] Color boldColor;

    string highResource;
    string midResource;
    string lowResource;

    Color highColor;
    Color midColor;
    Color lowColor;

    [SerializeField] AudioClip startDialogueClip;
    [SerializeField] AudioClip playerDialogueClip;
    [SerializeField] AudioClip otherDialogueClip;
    [SerializeField] AudioSource commsSource;

    bool isPlayer;
    
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
            if(!typing)
            {
                if (currentChoices.Count < 1)
                {

                    ContinueStory();

                }
            }
            else
            {
                DisplayFullSentence();

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
            if (prefix.ToLower() == "event")
            {
                if (param == "openShop")
                {
                    Debug.Log("open");
                    ExitDialogue();
                    FindObjectOfType<Inventory>().inShop = true;
                }
                else if (param == "showResource")
                {
                    showCostObject = true;
                }
                else if (param == "hideResource")
                {
                    showCostObject = false;
                }
                else if(param == "gainID")
                {
                    FindObjectOfType<Inventory>().hasID = true;
                }
                else if(param == "pickup")
                {
                    FindObjectOfType<Inventory>().helpedRefugee = true;
                    FindObjectOfType<Inventory>().resourceMultiplier += 0.5f;
                    FindObjectOfType<Inventory>().pickedUpRefugee = true;
                }
                else if(param == "share")
                {
                    FindObjectOfType<Inventory>().helpedRefugee = true;
                    FindObjectOfType<Inventory>().currentFood = FindObjectOfType<Inventory>().currentFood*0.3f;
                    FindObjectOfType<Inventory>().currentWater = FindObjectOfType<Inventory>().currentWater*0.3f;
                    FindObjectOfType<Inventory>().currentEnergy = FindObjectOfType<Inventory>().currentEnergy*0.3f;
                }

            }
            else if(prefix.ToLower() == "involved")
            {
                switch (player.ToLower())
                {
                    case "player":
                        playerAnimator.gameObject.SetActive(bool.Parse(param));
                        break;
                    case "merchant":
                        merchantAnimator.gameObject.SetActive(bool.Parse(param));
                        break;
                    case "mysterious":
                        mysteriousAnimator.gameObject.SetActive(bool.Parse(param));
                        break;
                    case "refugee":
                        refugeeAnimator.gameObject.SetActive(bool.Parse(param));
                        break;
                }
            }
            else
            {
                switch (player.ToLower())
                {
                    case "player":
                        anim = playerAnimator;
                        if (param.Contains("in"))
                        {
                            isPlayer = true;
                        }
                        else if(param.Contains("out"))
                        {
                            isPlayer = false;
                        }
                            break;
                    case "merchant":
                        anim = merchantAnimator;
                        break;
                    case "mysterious":
                        anim = mysteriousAnimator;
                        break;
                    case "refugee":
                        anim = refugeeAnimator;
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
        isPlayer = false;
        SetUpVariables();
        commsSource.volume = 1f;
        commsSource.clip = startDialogueClip;
        commsSource.time = 0f;
        commsSource.Play();
        
        StartStory();
    }
    void SetUpVariables()
    {
        Dictionary<string, Ink.Runtime.Object> variables = new Dictionary<string, Ink.Runtime.Object>();
        var variableNames = new List<string>();
        foreach (string name in currentStory.variablesState)
        {

            Ink.Runtime.Object value = currentStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            variableNames.Add(name);
        }

        SetUpColors();

        foreach (string name in variableNames)
        {
            switch (name)
            {
                case "highresource":
                    currentStory.variablesState[name] = highResource;
                    break;
                case "midresource":
                    currentStory.variablesState[name] = midResource;
                    break;
                case "lowresource":
                    currentStory.variablesState[name] = lowResource;
                    break;
                case "highresourcecolor":
                    currentStory.variablesState[name] = "#" + ColorUtility.ToHtmlStringRGB(highColor);
                    break;
                case "midresourcecolor":
                    currentStory.variablesState[name] = "#" + ColorUtility.ToHtmlStringRGB(midColor);
                    break;
                case "lowresourcecolor":
                    currentStory.variablesState[name] = "#" + ColorUtility.ToHtmlStringRGB(lowColor);
                    break;
                case "defaultcolor":
                    currentStory.variablesState[name] = "#" + ColorUtility.ToHtmlStringRGB(dialogueText.color);
                    break;
                case "boldcolor":
                    currentStory.variablesState[name] = "#" + ColorUtility.ToHtmlStringRGB(boldColor);
                    break;
            }
        }
    }
    void SetUpColors()
    {
        
        if (PlayerStats.food > PlayerStats.water)
        {
            if (PlayerStats.food > PlayerStats.energy)
            {
                highResource = "food";
                highColor = foodColor;
                if (PlayerStats.energy > PlayerStats.water)
                {
                    midResource = "energy";
                    midColor = energyColor;
                    lowResource = "water";
                    lowColor = waterColor;
                }
                else
                {
                    midResource = "water";
                    midColor = waterColor;
                    lowResource = "energy";
                    lowColor = energyColor;
                }
            }
            else
            {
                highResource = "energy";
                highColor = energyColor;
                midResource = "food";
                midColor = foodColor;
                lowResource = "water";
                lowColor = waterColor;
            }
        }
        else
        {
            if (PlayerStats.water > PlayerStats.energy)
            {
                highResource = "water";
                highColor = waterColor;
                if (PlayerStats.energy > PlayerStats.food)
                {
                    midResource = "energy";
                    midColor = energyColor;
                    lowResource = "food";
                    lowColor = foodColor;
                }
                else
                {
                    midResource = "food";
                    midColor = foodColor;
                    lowResource = "energy";
                    lowColor = energyColor;
                }
            }
            else
            {
                highResource = "energy";
                highColor = energyColor;
                midResource = "water";
                midColor = waterColor;
                lowResource = "food";
                lowColor = foodColor;

            }
        }
    }
    void ExitDialogue()
    {
        if (FindObjectOfType<PlayerMovement>() != null)
        {
            FindObjectOfType<PlayerMovement>().ExitDialogue();
        }

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    IEnumerator TypeSentence(string sentence)
    {
        bool colorOutputting = false;
        dialogueText.text = "";
        string currentTypedText = "";
        string colorText = "";
        typing = true;
        foreach (char letter in sentence.ToCharArray())
        {
            if (letter == '<')
            {
                colorOutputting = true;
            }
            if (letter == '>')
            {
                colorOutputting = false;
                currentTypedText += colorText;
                colorText = "";

            }
            if (colorOutputting)
            {
                colorText += letter;
            }
            else
            {
                currentTypedText += letter;
                yield return new WaitForSeconds(0.05f);
            }

            dialogueText.text = currentTypedText;

        }
        typing = false;
        commsSource.Stop();
    }

    void DisplayFullSentence()
    {
        StopAllCoroutines();
        dialogueText.text = currentSentence;
        typing = false;
        commsSource.Stop();
    }
    void DoSound()
    {
        commsSource.Stop();
        if (isPlayer)
        {
            commsSource.volume = 2f;
            commsSource.pitch = Random.Range(-0.8f, -1.2f);
            commsSource.clip = playerDialogueClip;
            float offset = Random.Range(0f, playerDialogueClip.length);
            commsSource.time = offset;
        }
        else
        {
            commsSource.volume = 2f;
            commsSource.pitch = Random.Range(0.6f, 0.8f);
            commsSource.clip = otherDialogueClip;
            float offset = Random.Range(0f, otherDialogueClip.length);
            commsSource.time = offset;
        }



        commsSource.Play();
    }

    void ContinueStory()
    {

        

        if (currentStory.canContinue)
        {
            
            StopAllCoroutines();
            currentSentence = currentStory.Continue();
            StartCoroutine(TypeSentence(currentSentence));
            DisplayChoices();
            ParseTags();
            DoSound();
        }
        else
        {
            ExitDialogue();
        }
    }

    void StartStory()
    {
        if (currentStory.canContinue)
        {
            StopAllCoroutines();
            currentSentence = currentStory.Continue();
            StartCoroutine(TypeSentence(currentSentence));
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
        audio = GetComponent<AudioSource>();
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        audio.Play();
        ContinueStory();
    }
}
