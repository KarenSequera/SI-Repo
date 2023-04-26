using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; 
    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>(); 
    public float timeBetweenSpawns ;
    private List<GameObject> sheepList = new List<GameObject>(); 
    private int numberSheepsSpawned = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep(){
        Vector3 randomPosition = sheepSpawnPositions [Random.Range(0, sheepSpawnPositions .Count)].position; 
        GameObject sheep = Instantiate(sheepPrefab, randomPosition ,
        sheepPrefab.transform.rotation); 
        sheepList.Add(sheep); 
        sheep.GetComponent<Sheep>().SetSpawner(this); 
        numberSheepsSpawned = numberSheepsSpawned +1;
        
        //if the total number of sheeps spawned is 10, we make the time between spawns smaller
        if(numberSheepsSpawned==20){
            timeBetweenSpawns = 1;
        }
        if(numberSheepsSpawned==30){
            timeBetweenSpawns = 0.5f;
        }
    }

    private IEnumerator SpawnRoutine(){
        while (canSpawn) 
        {
            SpawnSheep(); 
            yield return new WaitForSeconds(timeBetweenSpawns); 
        }
    }

    public void RemoveSheepFromList (GameObject sheep){
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep(){
        foreach (GameObject sheep in sheepList){
            Destroy(sheep); // 2
        }

        sheepList.Clear();
    }

}
