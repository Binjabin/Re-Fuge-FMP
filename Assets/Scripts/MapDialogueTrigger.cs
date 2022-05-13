using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset startInkJSON;
    [SerializeField] TextAsset secondInkJSON;
    float currentLevelsPassed;


    public void CheckDialogue()
    {
        


        StartCoroutine(WaitSeconds());
    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2f);
        currentLevelsPassed = FindObjectOfType<MapManager>().currentMap.path.Count - 1;
        if (currentLevelsPassed < 1)
        {
            DialogueManager.GetInstance().EnterDialogue(startInkJSON);
        }
        else if (currentLevelsPassed < 2)
        {
            DialogueManager.GetInstance().EnterDialogue(secondInkJSON);
        }
    }






}
