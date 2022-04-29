using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
    [SerializeField] Animator outAnimation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LeaveScene()
    {
        StartCoroutine(LeaveSceneAnimation());
    }
    public void ReturnToMap()
    {
        StartCoroutine(ReturnToMapAnimation());
    }

    IEnumerator LeaveSceneAnimation()
    {
        outAnimation.SetTrigger("Out");
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator ReturnToMapAnimation()
    {
        outAnimation.SetTrigger("Out");
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Map");
    }
}
