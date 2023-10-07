using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincherFingerController : MonoBehaviour
{
   
    public float closedZ; 

    Vector3 openPosition;
    ArticulationBody articulation;


    // INIT

    void Start()
    {
        openPosition = transform.localPosition;
        articulation = GetComponent<ArticulationBody>();
        SetLimits();
    }

    //sets the joint limits of the ArticulationBody's Z-drive 

    void SetLimits()
    {
        float openZTarget = ZDriveTarget(0.0f);
        float closedZTarget = ZDriveTarget(1.0f);
        float min = Mathf.Min(openZTarget, closedZTarget);
        float max = Mathf.Max(openZTarget, closedZTarget);

        var drive = articulation.zDrive;
        drive.lowerLimit = min;
        drive.upperLimit = max;
        articulation.zDrive = drive;
    }


    // Read the current grip

    public float CurrentGrip()
    {
        float grip = Mathf.InverseLerp(openPosition.z, closedZ, transform.localPosition.z);
        return grip;
    }

    public Vector3 GetOpenPosition()
    {
        return openPosition;
    }

    // Control the grip

    public void UpdateGrip(float grip)
    {
        float targetZ = ZDriveTarget(grip);
        var drive = articulation.zDrive;
        drive.target = targetZ;
        articulation.zDrive = drive;
    }

    public void ForceOpen(Transform transform)
    {
        transform.localPosition = openPosition;
        UpdateGrip(0.0f);
    }

    // HELPERS

    float ZDriveTarget(float grip)
    {
        float zPosition = Mathf.Lerp(openPosition.z, closedZ, grip);
        float targetZ = (zPosition - openPosition.z) * transform.parent.localScale.z;
        return targetZ;
    }


}
