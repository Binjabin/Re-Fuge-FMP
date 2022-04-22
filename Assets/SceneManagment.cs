using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReturnToMap()
    {
        StartCoroutine(ReturnToMapAnimation());
    }

    IEnumerator ReturnToMapAnimation()
    {
        return null;
    }
}
