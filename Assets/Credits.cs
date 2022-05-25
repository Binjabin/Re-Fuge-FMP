using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject credits;
    [SerializeField] float creditsSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowCredits());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ShowCredits()
    {
        float dist = 0f;
        while (dist < 1940f)
        {
            dist += Time.deltaTime * creditsSpeed;
            credits.GetComponent<RectTransform>().position = new Vector3(0, dist, 0);
            yield return null;
        }
        credits.GetComponent<RectTransform>().position = new Vector3(0, 1940f, 0);
        FindObjectOfType<MainMenu>().OpenMenu();
    }
}
