using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    public GameObject Endeffector;
    public bool pincher1 = false;
    public bool pincher2 = false;


    public RobotAgent parentAgent;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Endeffector")
        {
            if (parentAgent != null)
            {
                if (pincher1 && pincher2)
                {
                    Debug.Log("Touch Detected!");
                    parentAgent.JackpotReward();
                }
            }

        }
        if (collision.transform.gameObject.tag == "Pincher1")
        {
            pincher1 = true;
        }

        if (collision.transform.gameObject.tag == "Pincher2")
        {
            pincher2 = true;
        }
    }


    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Pincher1")
        {
            pincher1 = false;
        }

        if (collision.transform.gameObject.tag == "Pincher2")
        {
            pincher2 = false;
        }
    }
    
}

    

    

