using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using FMODUnity;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    StudioEventEmitter emit;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        emit = GetComponent<StudioEventEmitter>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Walk()
    {
        ResetAnimParams();
       
        anim.SetBool("Walk", true);        
        return true;

    }

    public bool Idle()
    {
        ResetAnimParams();
        
        anim.SetBool("Idle", true);        
        return true;

    }

    public bool Sneak()
    {
        ResetAnimParams();
        
        anim.SetBool("Sneak", true);        
        return true;

    }

    public bool Run()
    {
        ResetAnimParams();
        
        anim.SetBool("Run", true);
        return true;

    }

    public void ResetAnimParams()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Sneak", false);
        anim.SetBool("Run", false);

    }
    public void TriggerSound()
    {
        Debug.Log("trigger sound");


        resetSound();

        emit.Play();

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;  //ground layer

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 10, layerMask))
        {
                        

                if (hit.transform.tag == "grass")        //grass
                {
                    emit.SetParameter("grass", 1.0f);
                }
                else if (hit.transform.tag == "dirt")    //dirt 
                {
                    emit.SetParameter("dirt", 1.0f);
                }
                else if (hit.transform.tag == "stone")    //stone
                {
                    emit.SetParameter("stone", 1.0f);
                }
                

                Debug.Log(hit.transform.tag);

            
        }
    }

    void resetSound()
    {
        emit.SetParameter("grass", 0.0f);
        emit.SetParameter("dirt", 0.0f);
        emit.SetParameter("stone", 0.0f);
        
    }
}
