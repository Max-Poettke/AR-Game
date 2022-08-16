using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCube : MonoBehaviour
{
//  ANIMATE A SCALED CUBE BETWEEN CONTROLLERS, GRAB POINTS ON OPPOSING BOX FACES.
//  PASS IT TRANSFORM OF AN AXIS ALIGNED CUBE MODEL ( 1M x 1M x 1M ) WITH ITS MODEL ORIGIN IN ITS CENTER.
//  PASS IN TWO TRANSFORMS OF CONTROLLER POINTS.

private static Transform tHelper = null;
public GameObject cube;
private GameObject cube1;
private Vector3 temporaryAnchorpoint;
public OVRInput.Button buttonBL;
public OVRInput.Button buttonBR;
public OVRInput.Controller controllerL;
public OVRInput.Controller controllerR;

private void Update()
{
    var leftPressed = false;
    if(OVRInput.Get(buttonBL, controllerL)){
        leftPressed = true;
        temporaryAnchorpoint = OVRInput.GetLocalControllerPosition(controllerL);
        if (!tHelper)
        {
            GameObject oHelper = new GameObject();
            oHelper.name = "AnimateStretchyCube_helper";
            tHelper = oHelper.transform;
        }
    }

    if(OVRInput.GetUp(buttonBL,controllerL)) leftPressed = false;

    if(OVRInput.GetDown(buttonBR, controllerR)){
        cube1 = Instantiate(cube);
    }

    if(OVRInput.Get(buttonBR, controllerR) && leftPressed){
        AnimateStretchyCube(cube1.transform, temporaryAnchorpoint);
    }
}

private void AnimateStretchyCube(Transform tCube, Vector3 anchorPoint)
{
    tHelper.position = (anchorPoint + OVRInput.GetLocalControllerPosition(controllerR)) * 0.5f;
    tHelper.LookAt(OVRInput.GetLocalControllerPosition(controllerR), Vector3.up);

    tCube.SetPositionAndRotation (tHelper.position, tHelper.rotation);

    //  GET CONTROLLER POS IN CUBESPACE
    Vector3 vConL = tHelper.InverseTransformPoint(anchorPoint);
    Vector3 vConR = tHelper.InverseTransformPoint(OVRInput.GetLocalControllerPosition(controllerR));

    //  SCALE CUBE
    float fScale = (vConL - vConR).magnitude;
    tCube.localScale = new Vector3(fScale, fScale, fScale);
}


}
