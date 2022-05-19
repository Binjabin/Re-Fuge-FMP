using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip uiClick;
    AudioSource audio;
    [SerializeField] AudioSource music;
    [SerializeField] Animator transitions;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = uiClick;
    }
    public void StartGameButton()
    {

        StartCoroutine(StartGame());
    }

    public void ExitGameButton()
    {
        StartCoroutine(QuitGame());
    }

    public void CreditsButton()
    {
        audio.Play();
    }

    IEnumerator QuitGame()
    {
        audio.Play();
        transitions.SetTrigger("Out");
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        audio.Play();
        float elapsedTime = 0f;
        transitions.SetTrigger("Out");
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            music.volume = Mathf.Lerp(1f, 0f, (elapsedTime / 2f));
            yield return null;
        }
        SceneManager.LoadScene("Map");
    }

    IEnumerator Credits()
    {
        audio.Play();
        transitions.SetTrigger("Out");
        yield return new WaitForSeconds(2f);
        Debug.Log("Credits");
    }
}
