using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{

    [SerializeField] private Camera FPSCam;
    Disassembly partToDisassembly;
    private int currentTool;
    private Animator anim;
    private bool freeForAction = true;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && freeForAction)
        {
            Interact();
        }
    }

    private void Interact()
    {
        currentTool = this.transform.GetSiblingIndex();

        RaycastHit hit;
        Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit,10f);
        partToDisassembly = hit.transform.GetComponent<Disassembly>();


        if (partToDisassembly != null 
            && partToDisassembly.PartsChecker()
            && partToDisassembly.requiredTool == 1
            && currentTool == 1
            && freeForAction)
        {
            StartCoroutine("RotateWrench");
        }

        if (partToDisassembly != null
        && partToDisassembly.PartsChecker()
        && partToDisassembly.requiredTool == 0
        && currentTool == 0
        && freeForAction)
        {
            StartCoroutine("GrabItOut");
        }
    }

    private IEnumerator RotateWrench()
    {

        freeForAction = false;
        Quaternion startRotation = Quaternion.Euler(135, 0, 0);
        Quaternion endRotation = Quaternion.Euler(225, 0, 0);
        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 endPosition = new Vector3(.02f, 0, 0);
        float duration = 1f;

        Vector3 onHitCameraPosition = FPSCam.transform.position;
        Quaternion onHitCameraRotation = FPSCam.transform.rotation;

        Vector3 standardParentPosition = gameObject.transform.parent.position;
        Quaternion standardParentRotation = gameObject.transform.parent.rotation;
        Vector3 standardPosition = gameObject.transform.localPosition;
        Quaternion standardRotation = gameObject.transform.localRotation;

        gameObject.transform.parent.rotation = partToDisassembly.gameObject.transform.rotation;
        gameObject.transform.parent.position = partToDisassembly.gameObject.transform.position + partToDisassembly.toolOffset;

        //First Movement
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            gameObject.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t/duration);

            FPSCam.transform.position = onHitCameraPosition;
            FPSCam.transform.rotation = onHitCameraRotation;

            partToDisassembly.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            partToDisassembly.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t / duration);

            gameObject.transform.parent.position = partToDisassembly.transform.position + partToDisassembly.toolOffset;

            yield return null;
        }

        partToDisassembly.DissasemblyPart(currentTool);
        gameObject.transform.parent.position = standardParentPosition;
        gameObject.transform.parent.rotation = standardParentRotation;
        gameObject.transform.localPosition = standardPosition;
        gameObject.transform.localRotation = standardRotation;

        freeForAction = true;

    }




    private IEnumerator GrabItOut()
    {

        freeForAction = false;
        Quaternion startRotation = Quaternion.Euler(135, 0, 0);
        Quaternion endRotation = Quaternion.Euler(225, 0, 0);
        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 endPosition = new Vector3(-.02f, 0, 0);
        float duration = 1f;

        Vector3 onHitCameraPosition = FPSCam.transform.position;
        Quaternion onHitCameraRotation = FPSCam.transform.rotation;

        Vector3 standardParentPosition = gameObject.transform.parent.position;
        Quaternion standardParentRotation = gameObject.transform.parent.rotation;
        Vector3 standardPosition = gameObject.transform.localPosition;
        Quaternion standardRotation = gameObject.transform.localRotation;

        gameObject.transform.parent.rotation = partToDisassembly.gameObject.transform.rotation;
        gameObject.transform.parent.position = partToDisassembly.gameObject.transform.position;
        gameObject.transform.localRotation = Quaternion.Euler(90, 0, 0);


        //First Movement
        for (float t = 0; t < duration; t += Time.deltaTime)
        {

            FPSCam.transform.position = onHitCameraPosition;
            FPSCam.transform.rotation = onHitCameraRotation;


            partToDisassembly.transform.localPosition = Vector3.Lerp(startPosition, endPosition, t / duration);
            gameObject.transform.parent.position = partToDisassembly.transform.position + partToDisassembly.toolOffset;


            yield return null;
        }

        partToDisassembly.DissasemblyPart(currentTool);
        gameObject.transform.parent.position = standardParentPosition;
        gameObject.transform.parent.rotation = standardParentRotation;
        gameObject.transform.localPosition = standardPosition;
        gameObject.transform.localRotation = standardRotation;

        freeForAction = true;

    }
}
