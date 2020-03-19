using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFromCenter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
