using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject ratObject;
    public GameObject ratFolder;

    [Header("Rat spawn rate")]
    //how many seconds before the next rat spawns
    [SerializeField]
    private float ratSpawnRate = 5f;

    //how much the spawn rate decreases after each rat spawns
    [SerializeField]
    private float ratSpawnIncreaseFactor = 0.2f;

    //the minimum seconds possible (clamped value)
    [SerializeField]
    private float ratSpawnMaxSpawnRate = 0.3f;


    [Header("Points activate settings")]

    [SerializeField]
    private float newPointSpawnRate;

    [SerializeField]
    private bool[] activatedPoints;

    [SerializeField]
    private Transform[] points;


    //other stuff
    private float timeSinceLastRat;
    private float timeSinceLastPointSpawn;
    private int numPointsSpawned;

    // Start is called before the first frame update
    void Start()
    {
        points = new Transform[transform.childCount];

        //all points are set to the children
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.childCount);
            Debug.Log(transform.GetChild(i));
            points[i] = transform.GetChild(i).GetComponent<Transform>();
        }

        //points = transform.GetComponentsInChildren<Transform>();

        //initialize the list of bool
        activatedPoints = new bool[points.Length];
        for (int i = 0; i < activatedPoints.Length; i++)
        {
            activatedPoints[i] = false;
        }
        activatedPoints[0] = true;
        numPointsSpawned = 1;

        timeSinceLastRat = 0;
        timeSinceLastPointSpawn = 0;

        Debug.Log("Points size: " + points.Length);
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastRat += Time.deltaTime;
        timeSinceLastPointSpawn += Time.deltaTime;

        //spawn new rat
        if (timeSinceLastRat >= ratSpawnRate)
        {
            SpawnRat();
            timeSinceLastRat = 0f;
            ratSpawnRate = Mathf.Max(ratSpawnRate - ratSpawnIncreaseFactor, ratSpawnMaxSpawnRate);
        }

        //activate new point
        if (timeSinceLastPointSpawn >= newPointSpawnRate && numPointsSpawned < points.Length)
        {
            int randomPoint = Random.Range(0, points.Length);
            while (activatedPoints[randomPoint])
            {
                randomPoint = Random.Range(0, points.Length);
            }
            activatedPoints[randomPoint] = true;
            numPointsSpawned++;
            timeSinceLastPointSpawn = 0f;
        }

        
    }

    void SpawnRat()
    {
        int randomPoint = Random.Range(0, points.Length);

        //pick new points as long as the chosen point is not activated yet
        while (!activatedPoints[randomPoint]) {
            randomPoint = Random.Range(0, points.Length);
        }

        GameObject newRat = Instantiate(ratObject, points[randomPoint].position, transform.rotation);
        newRat.transform.SetParent(ratFolder.transform);

    }
}
