using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCollider : MonoBehaviour
{

    public RobotAgent parentAgent;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("RobotInternal"))
        {
            if (parentAgent != null)
                parentAgent.GroundHitPenalty();
        }

    }
}
