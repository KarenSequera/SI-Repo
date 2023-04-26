using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    public float dropDestroyDelay ; 
    public float heartOffset;
    public GameObject heartPrefab;

    private bool hitByHay;
    private bool hasDropped;
    private Collider myCollider;
    private Rigidbody myRigidbody;
    private SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start(){
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

    private void HitByHay(){
        sheepSpawner.RemoveSheepFromList (gameObject);
        hitByHay = true; 
        runSpeed = 0; 
        Destroy(gameObject, gotHayDestroyDelay); 

        //Create the heart effect
        Instantiate(heartPrefab, transform.position + new Vector3(0,heartOffset,0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;

        //Play the sound 
        SoundManager.Instance.PlaySheepHitClip();

        //+1 to the number of sheeps saved
        GameStateManager.Instance.SavedSheep();
    }

    private void OnTriggerEnter (Collider other){

        if (other.CompareTag("Hay") && !hitByHay){
            Destroy(other.gameObject); 
            HitByHay(); 
        }
        else if (other.CompareTag("DropSheep")){
            Drop();
        }
    }

    private void Drop(){
        //Remove the sheep
        if(!hasDropped){
            hasDropped = true;
            sheepSpawner.RemoveSheepFromList(gameObject);
            myRigidbody.isKinematic = false; 
            myCollider.isTrigger = false; 
            Destroy(gameObject, dropDestroyDelay ); 

            //Play sound 
            SoundManager.Instance.PlaySheepDroppedClip();

            //+1 to the number of sheeps droped
            GameStateManager.Instance.DroppedSheep();

            //Camera Shake
            CameraClass.Instance.Shake();
        }

    }
    
}
