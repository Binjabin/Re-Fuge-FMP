using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAsteroids : MonoBehaviour
{
    public float maxDistance;
    [SerializeField] float minDistanceApart;

    [SerializeField] List<Vector3> asteroidList;
    [SerializeField] List<GameObject> asteroids;
    // Start is called before the first frame update
    void Start()
    {
        GenerateField(50);
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
                asteroidPos = new Vector3(x, y, Random.RandomRange(0f, 5f));
                attempts++;
                bool checkDistance = true;
                for (int j = 0; j < asteroidList.Count; j++)
                {
                    if (Vector3.Distance(asteroidList[j], asteroidPos) < minDistanceApart)
                    {
                        checkDistance = false;
                    }
                }
                if (checkDistance == true)
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
                GameObject newAsteroid = Instantiate(asteroids[Random.Range(0, asteroids.Count)], asteroidPos, Quaternion.identity);
                newAsteroid.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                newAsteroid.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                newAsteroid.transform.parent = transform;

            }
        }

    }
}
