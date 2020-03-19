using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disassembly : MonoBehaviour
{
    [SerializeField] public int requiredTool;
    [SerializeField] public Vector3 toolOffset;
    [SerializeField] GameObject[] dependentParts;



    public void DissasemblyPart(int toolIndex)
    {
        if(toolIndex == requiredTool && PartsChecker())
        {
            gameObject.SetActive(false);
            Debug.Log("Object disassembled!");
        }
    }

    public bool PartsChecker()
    {
        if (dependentParts==null)
        {
            return true;
        }
        else
        {
            foreach (GameObject part in dependentParts)
            {
                if (part.activeSelf == true)
                {
                    Debug.LogError("You must remove other parts first!");
                    return false;
                }
            }
        }
        return true;


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
