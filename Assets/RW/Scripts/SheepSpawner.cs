using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; // keep spawning sheep

    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>(); // lists from where sheep will be spawned
    public float timeBetweenSpawns; // time between spawning

    private float i = 0f;

    private List<GameObject> sheepList = new List<GameObject>(); // alived sheep

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Spawn sheeps */
    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; // get rand position
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); // create new sheep 
        sheepList.Add(sheep); // add the reference to the list of alive sheeps
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }


    /* Coroutine */
    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        { 
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns - i*0.025f); // pause and resume execution
            i += 1;
        }
    }


    /* Remove a sheep from sheeps List  */
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }


    /* Remove and destroy all sheeps */ 
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep); 
        }

        sheepList.Clear(); 
    }
}
