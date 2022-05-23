using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartDialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset firstLevelJSON;
    [SerializeField] TextAsset firstLevelJSON2;
    [SerializeField] TextAsset firstLevelJSON3;
    [SerializeField] TextAsset secondLevelJSON;
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
        if (PlayerStats.levelPassed < 1)
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
        else if (PlayerStats.levelPassed < 2)
        {
            //DialogueManager.GetInstance().EnterDialogue(secondLevelJSON);
        }
    }
}
