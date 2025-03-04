using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class MIDI_IN : MonoBehaviour
{

    [DllImport("winmm.dll")]
    private static extern uint waveOutGetNumDevs();

    [DllImport("winmm.dll")]
    private static extern uint midiInGetNumDevs();

    [DllImport("winmm.dll")]
    private static extern uint  midiInGetDevCaps(uint index, uint blah, uint woof);

    [DllImport("DLLtry.dll")]
    private static extern int MIDI_Start();

    [DllImport("DLLtry.dll")]
    private static extern int MIDI_End();

    [DllImport("DLLtry.dll")]
    private static extern int getNote();
    [DllImport("DLLtry.dll")]
    private static extern int getVelo();

    // Start is called before the first frame update
    void Start()
    {
        

        int r = MIDI_Start();
        Debug.Log("myPuts " + r);


        uint i = waveOutGetNumDevs();

        Debug.Log("wave outs " + i);



        uint nMidiDeviceNum;
        nMidiDeviceNum = midiInGetNumDevs();

        Debug.Log("midi ins " + nMidiDeviceNum);

        uint res = 0;
        uint size= 256;
        uint ret = 0;

        for (uint j = 0; j < nMidiDeviceNum; ++j)
        {
            ret = midiInGetDevCaps(i, res , size);

            Debug.Log("caps " + ret);
           
        }


    }

    // Update is called once per frame
    void Update()
    {
        int n = getNote();
        int v = getVelo();
        Debug.Log("note " + n + " velo " + v);
    }

    private void OnDestroy()
    {
        MIDI_End();
    }
}
