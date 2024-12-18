using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public enum State { Walking, Sitting }
    public State currentState;

    public NavMeshAgent agent; 
    public Animator animator; 
    public Transform[] waypoints; 

    private int currentWaypointIndex = 0; 
    public bool isDay = true; 

    void Start()
    {

        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();


        if (isDay)
        {
            ChangeState(State.Walking);
        }
        else
        {
            ChangeState(State.Sitting);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                HandleWalkingState();
                break;
            case State.Sitting:
               
                break;
        }
    }

    void ChangeState(State newState)
    {
        currentState = newState;


        animator.SetBool("isWalking", false);
        animator.SetBool("isSitting", false);

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
    }

    void HandleWalkingState()
    {
       
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToNextWaypoint(); 
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
    }

    public void ToggleDayNight(bool day)
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
    }
}
