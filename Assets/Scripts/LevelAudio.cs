using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelAudio : MonoBehaviour
{
    bool playerInDanger;
    bool playerInDangerThisFrame;
    List<EnemyMovement> enemies = new List<EnemyMovement>();
    [SerializeField] AudioSource dangerAudio;
    [SerializeField] AudioSource suspenseAudio;


    private void Update()
    {
        if (playerInDangerThisFrame)
        {
            playerInDangerThisFrame = false;
        }
        int trackingEnemies = 0;
        foreach (EnemyMovement enemy in enemies)
        {
            if (enemy.isTracking)
            {
                trackingEnemies += 1;
            }
        }

        if (!playerInDanger)
        {
            if (trackingEnemies > 0)
            {
                playerInDanger = true;
                playerInDangerThisFrame = true;
                StartCoroutine(StartDanger());
            }
            else
            {
                playerInDanger = false;
            }
        }
        else if (playerInDanger)
        {
            if (trackingEnemies > 0)
            {
                playerInDanger = true;
            }
            else
            {
                playerInDanger = false;
                StartCoroutine(ExitDanger());
            }
        }
        playerInDanger = trackingEnemies > 0;

    }
    private void Start()
    {
        enemies = FindObjectsOfType<EnemyMovement>().ToList();
    }

    IEnumerator StartDanger()
    {
        dangerAudio.Play();
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            suspenseAudio.volume = Mathf.Lerp(1, 0f, (elapsedTime / 2f));
            dangerAudio.volume = Mathf.Lerp(0f, 1f, (elapsedTime / 2f));
            yield return null;
        }
        suspenseAudio.Stop();
    }

    IEnumerator ExitDanger()
    {
        suspenseAudio.Play();
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            suspenseAudio.volume = Mathf.Lerp(0f, 1f, (elapsedTime / 2f));
            dangerAudio.volume = Mathf.Lerp(1f, 0f, (elapsedTime / 2f));
            yield return null;
        }
        dangerAudio.Stop();
    }
}
