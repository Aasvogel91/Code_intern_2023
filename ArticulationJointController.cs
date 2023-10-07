using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection { None = 0, Positive = 1, Negative = -1 }; //This enum represents the possible rotation directions - None, Positive, and Negative.

public class ArticulationJointController : MonoBehaviour
{
    public RotationDirection rotationState = RotationDirection.None; //This variable holds the current rotation direction that the joint should move in.
    public float speed = 300.0f; // This variable determines the speed of rotation.

    private ArticulationBody articulation; // This variable holds the reference to the ArticulationBody component attached to the GameObject.


    // LIFE CYCLE

    void Start()
    {
        articulation = GetComponent<ArticulationBody>(); //gets the reference to the ArticulationBody component of the GameObject.
    }

    void FixedUpdate() 
    {
        if (rotationState != RotationDirection.None) {
            float rotationChange = (float)rotationState * speed * Time.fixedDeltaTime; //control the rotation of the joint based on the rotationState and speed
            float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange; //calculates the desired rotation (rotationGoal) based on the current rotation
            RotateTo(rotationGoal);
        }


    }


    // READ

    public float CurrentPrimaryAxisRotation()
    {
        float currentRotationRads = articulation.jointPosition[0];
        float currentRotation = Mathf.Rad2Deg * currentRotationRads; //returns the current rotation of the joint around its primary axis in degrees.
        return currentRotation;
    }


    // CONTROL

    public void ForceToRotation(float rotation)
    {
        // set target
        RotateTo(rotation);
        
        // force position
        float rotationRads = Mathf.Deg2Rad * rotation;
        ArticulationReducedSpace newPosition = new ArticulationReducedSpace(rotationRads);
        articulation.jointPosition = newPosition; 

        // force velocity to zero
        ArticulationReducedSpace newVelocity = new ArticulationReducedSpace(0.0f);
        articulation.jointVelocity = newVelocity;
        
    }


    // MOVEMENT HELPERS

    void RotateTo(float primaryAxisRotation)
    {
        var drive = articulation.xDrive;
        drive.target = primaryAxisRotation;
        articulation.xDrive = drive;
    }




}
