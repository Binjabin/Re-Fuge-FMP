using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float minDistanceApart;
    [SerializeField] List<Vector3> asteroidList;
    [SerializeField] GameObject asteroid;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(Random.Range(-1f, 1f));
        Debug.Log(Random.Range(-1f, 1f));
        Debug.Log(Random.Range(-1f, 1f));

        GenerateField(Random.RandomRange(0, 10000), LevelToLoad.asteroidCount);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateField(int seed, int count)
    {
        Random.seed = seed;
        for (int i = 0; i < count; i++)
        {
            Vector3 asteroidPos = Vector3.zero;
            bool foundPos = false;
            float attempts = 0;
            while (foundPos == false && attempts < 100)
            {
                float x = Random.Range(-maxDistance, maxDistance);
                float y = Random.Range(-maxDistance, maxDistance);
                asteroidPos = new Vector3(x, 0f, y);
                attempts++;
                bool checkDistance = true;
                for (int j = 0; j < asteroidList.Count; j++)
                {
                    if (Vector3.Distance(asteroidList[j], asteroidPos) < minDistanceApart)
                    {
                        checkDistance = false;
                    }
                }
                if (checkDistance = true)
                {
                    foundPos = true;

                }

            }

            if (attempts > 98f)
            {
                Debug.Log("failed to find position for asteroid");
            }
            else
            {
                asteroidList.Add(asteroidPos);
                Instantiate(asteroid, asteroidPos, Quaternion.identity);
            }
        }

    }
}
