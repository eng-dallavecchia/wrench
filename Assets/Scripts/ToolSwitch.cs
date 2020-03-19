using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwitch : MonoBehaviour
{

    private int currentTool = 0;

    private void ChangeTool()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool = 0;

            int i = 0;
            foreach (Transform tool in transform)
            {
                if (i == currentTool)
                {
                    tool.gameObject.SetActive(true);
                }
                else
                {
                    tool.gameObject.SetActive(false);
                }
                i++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = 1;

            int i = 0;
            foreach (Transform tool in transform)
            {
                if (i == currentTool)
                {
                    tool.gameObject.SetActive(true);
                }
                else
                {
                    tool.gameObject.SetActive(false);
                }
                i++;
            }
        }

    }


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        ChangeTool();
        }
    
}
