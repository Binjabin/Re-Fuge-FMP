using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartDialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset firstLevelJSON;
    [SerializeField] TextAsset firstLevelJSON2;
    [SerializeField] TextAsset firstLevelJSON3;
    [SerializeField] TextAsset secondLevelJSON;
    [SerializeField] TextAsset forthLevelJSON;
    [SerializeField] TextAsset sixthLevelJSON;
    [SerializeField] TextAsset eightLevelJSON;
    [SerializeField] TextAsset ninthLevelJSON;
    [SerializeField] TextAsset ninthLevelJSON2;
    public bool minedAsteroids;
    public bool checkedInventory;
    // Start is called before the first frame update
    void Start()
    {
        minedAsteroids = false;
        checkedInventory = false;
        StartCoroutine(WaitSeconds());
    }

    // Update is called once per frame
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2f);
        if(FindObjectOfType<PlayerMovement>().inactive == false)
        {
            if (PlayerStats.levelPassed == 0)
            {
                DialogueManager.GetInstance().EnterDialogue(firstLevelJSON);
                FindObjectOfType<PlayerMovement>().EnterMonologue();
                while (minedAsteroids == false)
                {
                    yield return null;
                }
                DialogueManager.GetInstance().EnterDialogue(firstLevelJSON2);
                FindObjectOfType<PlayerMovement>().EnterMonologue();
                FindObjectOfType<Inventory>().showTutorial = true;
                while (checkedInventory == false)
                {
                    yield return null;
                }
                DialogueManager.GetInstance().EnterDialogue(firstLevelJSON3);
                FindObjectOfType<Inventory>().showTutorial = false;
                FindObjectOfType<PlayerMovement>().EnterMonologue();
            }
            else if (PlayerStats.levelPassed == 1)
            {
                DialogueManager.GetInstance().EnterDialogue(secondLevelJSON);
                FindObjectOfType<PlayerMovement>().EnterMonologue();
            }
            else if (PlayerStats.levelPassed == 3)
            {
                DialogueManager.GetInstance().EnterDialogue(forthLevelJSON);
                FindObjectOfType<PlayerMovement>().EnterMonologue();
            }
            else if (PlayerStats.levelPassed == 5)
            {
                DialogueManager.GetInstance().EnterDialogue(sixthLevelJSON);
                FindObjectOfType<PlayerMovement>().EnterMonologue();
            }
            else if (PlayerStats.levelPassed == 7)
            {
                DialogueManager.GetInstance().EnterDialogue(eightLevelJSON);
                FindObjectOfType<PlayerMovement>().EnterMonologue();
            }
            else if (PlayerStats.levelPassed == 8)
            {
                if (FindObjectOfType<Inventory>().hasID)
                {
                    DialogueManager.GetInstance().EnterDialogue(ninthLevelJSON2);
                    FindObjectOfType<PlayerMovement>().EnterMonologue();
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogue(ninthLevelJSON);
                    FindObjectOfType<PlayerMovement>().EnterMonologue();
                }

            }

        }
    }
        
}
