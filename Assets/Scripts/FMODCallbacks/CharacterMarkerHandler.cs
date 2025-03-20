using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMarkerHandler : AudioTimelineMarkerHandler
{
    //spatialized audio object to manipulate in response, bring up from/send down to hell
    [SerializeField] Transform objAudio;



    private void Start()
    {
        //to attenuate volume we will simply move the audio emmiter down/up from hell
        objAudio.localPosition = Vector3.down * 666;

    
    }

    public override void dothat()
    {
        base.dothat();

        //do something


    }

    public override void HandleIt(bool onoff)
    {
        Debug.Log(transform.name + " Handled It!!");
        Talk(onoff);
    }

    public void Talk(bool isTalking)
    {
        //basically we want to "volume up" only when this character is speaking, on our event emmiter
        //so bring it up.
        

        //GetComponent<Animator>().SetBool("isTalking", isTalking);

        if (isTalking)
        {
            objAudio.localPosition = Vector3.up * 2;
        }
        else 
        {
            objAudio.localPosition = Vector3.down * 666;
        }

    }

    public FMODUnity.StudioEventEmitter getEmmitter()
    {
        return objAudio.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    bool start = true;
    //FMOD needs time to build itself before the timeline actually starts
    private void LateUpdate()
    {
        if(start)
        {
            //perhaps this goes in a trigger event?
            GetComponent<ScriptUsageTimeline>().startTimeline(0);
            start = false;
        }
    }
}
