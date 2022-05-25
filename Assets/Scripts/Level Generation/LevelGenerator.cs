using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] Color waterAsteroidColor;
    [SerializeField] Color foodAsteroidColor;
    [SerializeField] Color energyAsteroidColor;
    public float maxDistance;
    [SerializeField] float minDistanceApart;

    [SerializeField] List<Vector3> asteroidList;
    [SerializeField] List<GameObject> asteroids;
    [Header("Space Station Settings")]
    [SerializeField] GameObject spaceStation;
    [SerializeField] float merchantBufferDistance;
    [SerializeField] float minDistanceFromCenterMerchant;
    [Header("Refugee Settings")]
    [SerializeField] GameObject refugeeShip;
    [SerializeField] float refugeeBufferDistance;
    [SerializeField] float minDistanceFromCenterRefugee;
    [Header("Mysterious Man Settings")]
    [SerializeField] GameObject manShip;
    [SerializeField] float manBufferDistance;
    [SerializeField] float minDistanceFromCenterMan;
    [Header("Warp Point Settings")]
    [SerializeField] GameObject endPoint;
    [SerializeField] float endBufferDistance;
    [SerializeField] float minDistanceFromCenterEnd;
    [SerializeField] float minDistanceFromShipEnd;
    [Header("Enemy Settings")]
    [SerializeField] GameObject lightEnemy;
    [SerializeField] GameObject heavyEnemy;
    [SerializeField] float enemyBufferDistance;
    [SerializeField] float minDistanceFromCenterEnemy;

    float seed;
    Quaternion merchantRotation;
    List<Vector3> nextEnemyWaypoint;
    GameObject merchantShip;
    List<AsteroidWeights> asteroidWeights;
    [SerializeField] GameObject othea;
    // Start is called before the first frame update
    void Awake()
    {
        Random.InitState(LevelToLoad.seed);
        asteroidWeights = LevelToLoad.asteroidWeights;
        if (LevelToLoad.containsMerchant)
        {
            PlaceSpaceStation();
        }
        if (LevelToLoad.containsRefugee)
        {
            PlaceRefugeeShip();
        }
        if(LevelToLoad.containsMysteriousMan)
        {
            PlaceMysteriousMan();
        }
        for (int i = 0; i < LevelToLoad.standardEnemyCount; i++)
        {
            PlaceLightEnemy();
        }
        for (int i = 0; i < LevelToLoad.heavyEnemyCount; i++)
        {
            PlaceHeavyEnemy();
        }
        GenerateField(LevelToLoad.asteroidCount);
        if(PlayerStats.levelPassed == 8)
        {
            othea.SetActive(true);
        }
        else
        {
            othea.SetActive(false);
            PlaceEndPoint();
        }
        

    }
    void LateStart()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    
    void PlaceLightEnemy()
    {
        nextEnemyWaypoint = new List<Vector3>();
        float x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        float y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(x, 0f, y));

        x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(-x, 0f, y));

        x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(-x, 0f, -y));

        x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(x, 0f, -y));

        GameObject enemy = Instantiate(lightEnemy, nextEnemyWaypoint[Random.Range(0, nextEnemyWaypoint.Count)], Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().patrolPoints = nextEnemyWaypoint;
    }

    void PlaceHeavyEnemy()
    {
        nextEnemyWaypoint = new List<Vector3>();
        float x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        float y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(x, 0f, y));

        x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(-x, 0f, y));

        x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(-x, 0f, -y));

        x = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        y = Random.Range(maxDistance - enemyBufferDistance, minDistanceFromCenterEnemy);
        nextEnemyWaypoint.Add(new Vector3(x, 0f, -y));

        GameObject enemy = Instantiate(heavyEnemy, nextEnemyWaypoint[Random.Range(0, nextEnemyWaypoint.Count)], Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().patrolPoints = nextEnemyWaypoint;
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
        merchantShip = Instantiate(spaceStation, merchantPosition, merchantRotation);
    }

    void PlaceRefugeeShip()
    {
        Vector3 refugeePosition = new Vector3(0f, 0f, 0f);
        float x = Random.Range(-maxDistance + refugeeBufferDistance, -minDistanceFromCenterRefugee);
        float z = Random.Range(-maxDistance + refugeeBufferDistance, -minDistanceFromCenterRefugee);
        if (Random.Range(0, 2) == 0)
        {
            x = -x;
        }
        if (Random.Range(0, 2) == 0)
        {
            z = -z;
        }
        refugeePosition.x = x;
        refugeePosition.z = z;

        Instantiate(refugeeShip, refugeePosition, Quaternion.identity);
    }

    void PlaceMysteriousMan()
    {
        Vector3 manPosition = new Vector3(0f, 0f, 0f);
        float x = Random.Range(-maxDistance + manBufferDistance, -minDistanceFromCenterMan);
        float z = Random.Range(-maxDistance + merchantBufferDistance, -minDistanceFromCenterMan);
        if (Random.Range(0, 2) == 0)
        {
            x = -x;
        }
        if (Random.Range(0, 2) == 0)
        {
            z = -z;
        }
        manPosition.x = x;
        manPosition.z = z;

        Instantiate(manShip, manPosition, Quaternion.identity);
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
                GameObject newAsteroid = Instantiate(asteroids[Random.Range(0,asteroids.Count)], asteroidPos, Quaternion.identity);
                newAsteroid.GetComponent<Asteroids>().itemType = GetAsteroidType();
                if (newAsteroid.GetComponent<Asteroids>().itemType == ItemType.Water)
                {
                    newAsteroid.GetComponent<MeshRenderer>().material.SetColor("_Color", waterAsteroidColor);
                }
                if (newAsteroid.GetComponent<Asteroids>().itemType == ItemType.Energy)
                {
                    newAsteroid.GetComponent<MeshRenderer>().material.SetColor("_Color", energyAsteroidColor);
                }
                if (newAsteroid.GetComponent<Asteroids>().itemType == ItemType.Food)
                {
                    newAsteroid.GetComponent<MeshRenderer>().material.SetColor("_Color", foodAsteroidColor);
                }


            }
        }

    }
    ItemType GetAsteroidType()
    {
        float maxWeight = 0f;
        for (var i = 0; i < asteroidWeights.Count; i++)
        {
            maxWeight += asteroidWeights[i].weight;
        }
        float randomValue = Random.Range(0f, maxWeight);

        int index = 0;
        int lastIndex = asteroidWeights.Count - 1;
        float weightCap = 0;
        while (index < lastIndex)
        {
            weightCap += asteroidWeights[index].weight;
            if (randomValue < weightCap)
            {
                return asteroidWeights[index].type;
            }
            index++;
        }

        // No other item was selected, so return very last index.
        return asteroidWeights[index].type;
    }
    void PlaceEndPoint()
    {
        bool foundPlace = false;
        Vector3 end = Vector3.zero;
        while (foundPlace == false)
        {
            float x = Random.Range(-maxDistance + endBufferDistance, -minDistanceFromCenterEnd);
            float z = Random.Range(-maxDistance + endBufferDistance, -minDistanceFromCenterEnd);
            if (Random.Range(0, 2) == 0)
            {
                x = -x;
            }
            if (Random.Range(0, 2) == 0)
            {
                z = -z;
            }
            end.x = x;
            end.z = z;
            if(LevelToLoad.containsMerchant)
            {
                if ((end - merchantShip.transform.position).magnitude > minDistanceFromShipEnd)
                {
                    foundPlace = true;
                }
            }
            else
            {
                foundPlace = true;
            }
            
        }
        Instantiate(endPoint, end, Quaternion.identity);
    }
}
