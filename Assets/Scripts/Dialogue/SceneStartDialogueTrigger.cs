using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartDialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset firstLevelJSON;
    [SerializeField] TextAsset secondLevelJSON;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else if (PlayerStats.levelPassed < 2)
        {
            DialogueManager.GetInstance().EnterDialogue(secondLevelJSON);
        }
    }
}
