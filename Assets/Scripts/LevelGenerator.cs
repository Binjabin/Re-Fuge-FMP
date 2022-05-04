using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float minDistanceApart;
    [SerializeField] List<Vector3> asteroidList;
    [SerializeField] List<GameObject> asteroids;
    [SerializeField] GameObject spaceStation;
    [SerializeField] GameObject lightEnemy;
    [SerializeField] GameObject heavyEnemy;
    [SerializeField] float merchantBufferDistance;
    [SerializeField] float minDistanceFromCenterMerchant;
    float seed;
    Quaternion merchantRotation;
    // Start is called before the first frame update
    void Start()
    {
        Random.seed = LevelToLoad.seed;
        if (LevelToLoad.containsMerchant)
        {
            PlaceSpaceStation();
        }
        for(int i = 0; i < LevelToLoad.standardEnemyCount; i++)
        {

        }
        GenerateField(LevelToLoad.asteroidCount);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaceSpaceStation()
    {
        Vector3 merchantPosition = new Vector3(0f, 0f, 0f);
        float x = Random.Range(-maxDistance + merchantBufferDistance, -minDistanceFromCenterMerchant);
        float z = Random.Range(-maxDistance + merchantBufferDistance, -minDistanceFromCenterMerchant);
        if (Random.Range(0, 2) == 0)
        {
            x = -x;
        }
        if (Random.Range(0, 2) == 0)
        {
            z = -z;
        }
        merchantPosition.x = x;
        merchantPosition.z = z;
        if (merchantPosition.z > merchantPosition.x)
        {
            merchantRotation.eulerAngles = new Vector3(0f, -90f, 0f);
        }
        else
        {
            merchantRotation.eulerAngles = new Vector3(0f, -90f, 0f);
        }
        Instantiate(spaceStation, merchantPosition, merchantRotation);
    }

    public void GenerateField(int count)
    {
        
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
                Instantiate(asteroids[Random.Range(0,asteroids.Count)], asteroidPos, Quaternion.identity);
            }
        }

    }
}
