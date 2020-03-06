using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Mover : MonoBehaviour
{
    public NavMeshAgent agent;
    private Ray ray;
    private RaycastHit hit;
    public GameObject point;
    [SerializeField] private float stamina = 10f;
    public Slider staminaSlider;
    private float distanceCoverage = 0f;
    private float currentDistance;
    public static float staminaBuff = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
         MoveDesktop();
#endif

#if (UNITY_ANDROID)
        MoveMobile();
#endif
        //Debug.Log(GetDistance());
        //if (agent.velocity.magnitude == 0) Debug.Log("magnitude = 0");

        if (Input.GetKeyDown(KeyCode.S))
        {
            //ReadDatabase();
        }
    }

    private void MoveDesktop()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 100))
        {
            agent.SetDestination(hit.point);
            point.transform.position = hit.point;
            currentDistance = Vector3.Distance(transform.position, hit.point);
            //Debug.Log(DateTime.Now);
            //Debug.Log(DateTime.UtcNow);
        }

        if (stamina <= 0 && agent.velocity.magnitude != 0)
        {
            agent.isStopped = true;
            UpdateStamina();
        }
        else if (agent.velocity.magnitude != 0)
        {
            
            stamina -= agent.velocity.magnitude * Time.deltaTime;
            UpdateStamina();
            //Debug.Log(agent.remainingDistance);
        }
        else if (agent.isStopped && stamina > 5)
        {
            agent.isStopped = false;
            stamina = stamina > 10 ? 10f : stamina + staminaBuff * Time.deltaTime;
            UpdateStamina();
        }
        else
        {
            stamina = stamina > 10 ? 10f : stamina + staminaBuff * Time.deltaTime;
            UpdateStamina();
        }
        //navMeshAgent.SetDestination;
    }

    private void MoveMobile()
    {
        if (Input.touchCount > 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, 100))
            {
                agent.SetDestination(hit.point);
                point.transform.position = hit.point;
                //Debug.Log(DateTime.Now);
                //Debug.Log(DateTime.UtcNow);
            }
        }
    }

    //private void CalculateDistance()
    //{
        
    //}

    public void UpdateStamina()
    {
        staminaSlider.value = stamina / 10;
        //CalculateDistance();
        //distanceCoverage += GetDistance() % GetDistance();

    }

}
