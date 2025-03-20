using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SineWind : MonoBehaviour
{
    public float targwind = 0 ;
    public float startwind = 0;
    StudioEventEmitter emit;
    float start = -1;

    // Start is called before the first frame update
    void Start()
    {
        start = Time.time;
        emit = transform.GetComponent<StudioEventEmitter>();    
    }


    float t = 0;
    float rate = 1.0f;
    // Update is called once per frame
    void Update()
    {

        float usewind =  Mathf.Lerp(startwind, targwind, t);

        t = t + Time.deltaTime * ( rate  +  Random.Range(0.1f, 0.3f) ) ;

        emit.SetParameter("Wind2", usewind);

        if(t >= 1.0f)
        {
            rate = Random.Range(2.0f, 4.0f);
            startwind = targwind;
            targwind = Random.Range(0.3f, 1.0f);
            t = 0;
        }
    }
}
