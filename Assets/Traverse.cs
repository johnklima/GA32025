using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Traverse : MonoBehaviour
{

    public string text;
    public Transform SharedBranch;
    public Text guiText;

    int mydepth = 0;

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
        if (other.tag == "Player")
        {
            
            recurseToDramaManager(transform);
            

            string pre = "";
            for(int i = 0 ; i < mydepth; i++)
            {
                pre = pre + "   "; 
            }

            guiText.text = guiText.text  + "\n" + pre + text;

            if (transform.childCount == 0)
            {
               
                if(SharedBranch == null)
                {
                    guiText.text = guiText.text + "\nThe End";
                    return;
                }

                SharedBranch.gameObject.SetActive(true);
                return;
                
            }

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            


        }
    }

    void recurseToDramaManager(Transform node)
    {
        if(node == null)
        {
            return;
        }

        mydepth++;

        DramaManager man = node.GetComponent<DramaManager>();
        if(man)
        {
            man.depth++;
            man.currentNode = this.transform;
            
        }

        recurseToDramaManager(node.parent);
        

    }
}
