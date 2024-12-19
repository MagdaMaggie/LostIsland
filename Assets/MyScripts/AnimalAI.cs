using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public enum State { Walking, Sitting }
    public State currentState = State.Walking;

    public NavMeshAgent agent; 
    public Animator animator; 
    public Transform[] waypoints; 

    public int currentWaypointIndex = 0; 
    public DayNightManager dayNightManager;

    void Start()
    {

        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();

        dayNightManager = FindObjectOfType<DayNightManager>();
        //if (isDay)
        //{
        //    ChangeState(State.Walking);
        //}
        //else
        //{
        //    ChangeState(State.Sitting);
        //}
    }

    void Update()
    {
        if (dayNightManager.isDay)
        {
            currentState = State.Walking;
        }
        else
        {
            currentState = State.Sitting;
        }

        switch (currentState)
        {
            case State.Walking:
                agent.isStopped = false;
                animator.SetBool("isWalking", true);
                animator.SetBool("isSitting", false);
                HandleWalkingState();
                break;
            case State.Sitting:
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isSitting", true);
                break;
        }
    }

    /*void ChangeState(State newState)
    {
        currentState = newState;


        animator.SetBool("isWalking", false);
        //animator.SetBool("isSitting", false);

        switch (newState)
        {
            case State.Walking:
                animator.SetBool("isWalking", true);
                agent.isStopped = false; 
                MoveToNextWaypoint(); 
                break;
            case State.Sitting:
                animator.SetBool("isSitting", true);
                agent.isStopped = true; 
                break;
        }
    }*/

    void HandleWalkingState()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= agent.stoppingDistance)
        {
            MoveToNextWaypoint(); 
        }
    }

    void MoveToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
    }

    /*public void ToggleDayNight(bool day)
    {
        isDay = day;
        if (isDay)
        {
            ChangeState(State.Walking); 
        }
        else
        {
            ChangeState(State.Sitting); 
        }
    }*/
}
