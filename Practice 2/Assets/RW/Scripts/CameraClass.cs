using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClass : MonoBehaviour
{
    public static CameraClass Instance;
    public bool start;
    public AnimationCurve curve;
    private float duration = 0.5f;

    // Update is called once per frame
    void Awake(){
        Instance = this;
        start = false;
    }

    void Update()
    {
        if(start){
            start = false;
            StartCoroutine(Shaking());
        }
    }
    
    IEnumerator Shaking(){
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while(elapsedTime<duration){
            elapsedTime += Time.deltaTime;
            float strengh = curve.Evaluate(elapsedTime/duration);
            transform.position = startPosition + Random.insideUnitSphere*strengh;
            yield return null;
        }
        transform.position = startPosition;
    }

    public void Shake(){
        start = true;
    }
}
