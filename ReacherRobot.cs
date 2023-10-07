using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;



public class ReacherRobot : Agent
{
    public GameObject pendulumA;
    public GameObject pendulumB;
    public GameObject pendulumC;
    public GameObject pendulumD;
    public GameObject pendulumE;
    public GameObject pendulumF;

    Rigidbody m_RbA;
    Rigidbody m_RbB;
    Rigidbody m_RbC;
    Rigidbody m_RbD;
    Rigidbody m_RbE;
    Rigidbody m_RbF;

    public GameObject Hand;
    public GameObject goal;

    public float m_GoalHeight = 1.2f;

    float m_GoalRadius;//Radius of the goal aera
    float m_Goaldegree;//How much the goal rotate
    float m_GoalSpeed;//speed of the goal rotation
    float m_GoalDeviation;//How much goal up and down from the goal height
    float m_GoalDevationFreq;//the freq of the goal up and down movement

    public override void Initialize()
    {
        m_RbA = pendulumA.GetComponent<Rigidbody>();
        m_RbB = pendulumB.GetComponent<Rigidbody>();
        m_RbC = pendulumC.GetComponent<Rigidbody>();
        m_RbD = pendulumD.GetComponent<Rigidbody>();
        m_RbE = pendulumE.GetComponent<Rigidbody>();
        m_RbF = pendulumF.GetComponent<Rigidbody>();

        SetResetParameters();
    }

  

    public override void OnEpisodeBegin()
    {
        pendulumA.transform.position = new Vector3(6.415421f, 0.2557043f, -1.704251f) + transform.position;
        pendulumA.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbA.velocity = Vector3.zero;
        m_RbA.angularVelocity = Vector3.zero;


        pendulumB.transform.position = new Vector3(6.265421f, 0.2557043f, -1.704251f) + transform.position;
        pendulumB.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbB.velocity =  Vector3.zero;
        m_RbB.angularVelocity =  Vector3.zero;


        pendulumC.transform.position = new Vector3(6.265421f, 1.080704f, -1.704251f) + transform.position;
        pendulumC.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbC.velocity =  Vector3.zero;
        m_RbC.angularVelocity =  Vector3.zero;


        pendulumD.transform.position = new Vector3(6.265421f, 1.080704f, -1.704251f) + transform.position;
        pendulumD.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbD.velocity =  Vector3.zero;
        m_RbD.angularVelocity =  Vector3.zero;


        pendulumE.transform.position = new Vector3(6.265421f, 1.705704f, -1.704251f) + transform.position;
        pendulumE.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbE.velocity =  Vector3.zero;
        m_RbE.angularVelocity =  Vector3.zero;


        pendulumF.transform.position = new Vector3(6.265421f, 1.815704f, -1.704251f) + transform.position;
        pendulumF.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbF.velocity =  Vector3.zero;
        m_RbF.angularVelocity =  Vector3.zero;

        SetResetParameters();

        m_Goaldegree += m_GoalSpeed;
        UpdateGoalPOsition();
    }
    public void SetResetParameters()
    {
        m_GoalRadius = Random.Range(1f,1.3f);
        m_Goaldegree = Random.Range(0f, 360f);
        m_GoalSpeed = Random.Range(0.5f,1.5f);
        m_GoalDeviation = Random.Range(-0.5f, 0.5f);
        m_GoalDevationFreq = Random.Range(0f, 1.57f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(pendulumA.transform.localPosition);
        sensor.AddObservation(pendulumA.transform.rotation);
        sensor.AddObservation(m_RbA.velocity);
        sensor.AddObservation(m_RbA.angularVelocity);

        sensor.AddObservation(pendulumB.transform.localPosition);
        sensor.AddObservation(pendulumB.transform.rotation);
        sensor.AddObservation(m_RbB.velocity);
        sensor.AddObservation(m_RbB.angularVelocity);

        sensor.AddObservation(pendulumC.transform.localPosition);
        sensor.AddObservation(pendulumC.transform.rotation);
        sensor.AddObservation(m_RbC.velocity);
        sensor.AddObservation(m_RbC.angularVelocity);

        sensor.AddObservation(pendulumD.transform.localPosition);
        sensor.AddObservation(pendulumD.transform.rotation);
        sensor.AddObservation(m_RbD.velocity);
        sensor.AddObservation(m_RbD.angularVelocity);

        sensor.AddObservation(pendulumE.transform.localPosition);
        sensor.AddObservation(pendulumE.transform.rotation);
        sensor.AddObservation(m_RbE.velocity);
        sensor.AddObservation(m_RbE.angularVelocity);

        sensor.AddObservation(pendulumF.transform.localPosition);
        sensor.AddObservation(pendulumF.transform.rotation);
        sensor.AddObservation(m_RbF.velocity);
        sensor.AddObservation(m_RbF.angularVelocity);

        sensor.AddObservation(goal.transform.localPosition);
        sensor.AddObservation(Hand.transform.localPosition);

        sensor.AddObservation(m_GoalSpeed);

    }

    public override void OnActionReceived(ActionBuffers Action)
    {
        var torque = Mathf.Clamp(Action.ContinuousActions[0], -1f, 1f) * 150f;
        m_RbA.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(Action.ContinuousActions[1], -1f, 1f) * 150f;
        m_RbB.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(Action.ContinuousActions[2], -1f, 1f) * 150f;
        m_RbC.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(Action.ContinuousActions[3], -1f, 1f) * 150f;
        m_RbD.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(Action.ContinuousActions[4], -1f, 1f) * 150f;
        m_RbE.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(Action.ContinuousActions[5], -1f, 1f) * 150f;
        m_RbF.AddTorque(new Vector3(0f, torque, 0f));

        m_Goaldegree += m_GoalSpeed;
        UpdateGoalPOsition();
    }

    void UpdateGoalPOsition()
    {
        var m_Goaldegree_rad = m_Goaldegree * Mathf.PI / 180f;
        var GoalX = m_GoalRadius * Mathf.Cos(m_Goaldegree_rad);
        var GoalZ = m_GoalRadius * Mathf.Sin(m_Goaldegree_rad);
        var GoalY = m_GoalHeight + m_GoalDeviation * Mathf.Cos(m_GoalDevationFreq * m_Goaldegree_rad);

        goal.transform.position = new Vector3(GoalX+6.2f, GoalY, GoalZ-2f) + transform.position;
    }
}
