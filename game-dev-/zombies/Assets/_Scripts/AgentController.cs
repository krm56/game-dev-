using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour 
{
    public enum AgentType { Chaser, Patroller }
    [Header("Zombie Type")]
    public AgentType zombieType = AgentType.Chaser;

    public enum AgentState { Idle, Patrolling, Chasing }
    public AgentState state = AgentState.Idle;

    [Header("Settings")]
    public Transform target; 
    public float detectionRange = 30f;
    public float attackRange = 3.0f; 
    public float rotationSpeed = 2.0f; 
    public Transform[] waypoints; 

    private NavMeshAgent navMeshAgent;
    private Animator animController;
    private int speedHashId;
    private int attackTriggerId; 
    private int currentWaypoint = 0;
    private float oldRemainingDistance = float.PositiveInfinity;

    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        
        
        speedHashId = Animator.StringToHash("walkingSpeed");
        attackTriggerId = Animator.StringToHash("Attack"); 
        
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (Time.timeScale == 0) {
            navMeshAgent.isStopped = true;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < detectionRange) {
            state = AgentState.Chasing;
        } else if (zombieType == AgentType.Patroller) {
            state = AgentState.Patrolling;
        } else {
            state = AgentState.Idle;
        }

        // Execute States
        switch (state) {
            case AgentState.Chasing: Chase(); break;
            case AgentState.Patrolling: Patrol(); break;
            case AgentState.Idle: Idle(); break;
        }
    }

    private float GetRemainingDistance() {
        if (navMeshAgent.pathPending) return oldRemainingDistance;
        if (!navMeshAgent.hasPath) return float.PositiveInfinity;

        float distance = 0;
        Vector3[] corners = navMeshAgent.path.corners;
        for (int i = 0; i < corners.Length - 1; i++) {
            distance += Vector3.Distance(corners[i], corners[i + 1]);
        }
        oldRemainingDistance = distance;
        return distance;
    }

    void Chase() {
        navMeshAgent.isStopped = false;
        navMeshAgent.stoppingDistance = 1.5f; 
        navMeshAgent.SetDestination(target.position);

        float dist = GetRemainingDistance();

        if (dist <= attackRange) {
            animController.SetTrigger(attackTriggerId);
        }

        
        if (dist <= navMeshAgent.stoppingDistance) {
            navMeshAgent.isStopped = true;
            UpdateAnimation(0f);
            RotateTowardsTarget();
        } else {
            navMeshAgent.isStopped = false;
            UpdateAnimation(1f);
        }
    }

    void RotateTowardsTarget() {
        Vector3 planarDifference = (target.position - transform.position);
        planarDifference.y = 0;
        if (planarDifference != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(planarDifference.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Patrol() {
        if (waypoints.Length == 0) return;
        
        navMeshAgent.isStopped = false;
        navMeshAgent.stoppingDistance = 0;
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 1.0f) {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        UpdateAnimation(0.5f); 
    }

    void Idle() {
        navMeshAgent.isStopped = true;
        UpdateAnimation(0f);
    }

    void UpdateAnimation(float speed) {
        if (animController != null) {
            animController.SetFloat(speedHashId, speed);
        }
    }
}