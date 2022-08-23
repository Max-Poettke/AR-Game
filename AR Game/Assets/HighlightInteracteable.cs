using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightInteracteable : MonoBehaviour
{

    public OVRInput.Controller controllerR;
    public OVRInput.Controller controllerL;
    public GameObject helper;
    public GameObject quad;
    private GameObject iQuad;
    private RaycastHit hit;

    void Start(){
        iQuad = Instantiate(quad);
        //iQuad.transform.localScale = new Vector3(0,0,0);
    }
    // Update is called once per frame
    void Update()
    {
        DetectSurface(controllerR);
        DetectSurface(controllerL);
    }

    void DetectSurface (OVRInput.Controller controller){
        if(Physics.Raycast(OVRInput.GetLocalControllerPosition(controller), OVRInput.GetLocalControllerRotation(controller).eulerAngles, out hit, 10)){
            
            //iQuad.transform.localScale = new Vector3 (0.5f, 0.5f, 1f);
            iQuad.transform.position = hit.point + (hit.normal * 0.03f);
            iQuad.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
            helper.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            var localVectorToHitPoint = helper.transform.InverseTransformDirection(hit.transform.position);
            
            float [] arr = new float [3];
            arr[0] = localVectorToHitPoint.x;
            arr[1] = localVectorToHitPoint.y;
            arr[2] = localVectorToHitPoint.z;

            float maximumFloat = arr[0];
            for(var i = 0; i < 1; i ++){
                if(maximumFloat > arr[i + 1]){
                    arr[i + 1] = 0;
                } else {
                    maximumFloat = arr[i + 1];
                    arr[i] = 0;
                }
            }
            
            Vector3 expansionDirection = new Vector3(arr[0], arr[1], arr[2]);
        }
    }
}
