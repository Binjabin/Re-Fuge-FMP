using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset mapJSON1;
    [SerializeField] TextAsset mapJSON2;
    [SerializeField] TextAsset mapJSON3;
    [SerializeField] TextAsset mapJSON4;
    [SerializeField] TextAsset mapJSON5;
    [SerializeField] TextAsset mapJSON6;
    [SerializeField] TextAsset mapJSON7;
    [SerializeField] TextAsset mapJSON8;
    [SerializeField] TextAsset mapJSON9;
    float currentLevelsPassed;


    public void CheckDialogue()
    {
        StartCoroutine(WaitSeconds());
    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2f);
        currentLevelsPassed = FindObjectOfType<MapManager>().currentMap.path.Count;
        if (currentLevelsPassed == 0)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON1);
        }
        else if (currentLevelsPassed == 1)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON2);
        }
        else if (currentLevelsPassed == 2)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON3);
        }
        else if (currentLevelsPassed == 3)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON4);
        }
        else if (currentLevelsPassed == 4)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON5);
        }
        else if (currentLevelsPassed == 5)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON6);
        }
        else if (currentLevelsPassed == 6)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON7);
        }
        else if (currentLevelsPassed == 7)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON8);
        }
        else if (currentLevelsPassed == 8)
        {
            DialogueManager.GetInstance().EnterDialogue(mapJSON9);
        }
    }






}
