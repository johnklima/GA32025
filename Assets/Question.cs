using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{

    public Transform[] Answers;

    public string text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Answer " + text);

        if (other.tag == "Player")
        {
            for(int i = 0; i < Answers.Length; i++)
            {
                Answers[i].gameObject.SetActive(true);
                Debug.Log("option " + i + " " + Answers[i].name);

            }

        }
    }

}
