using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour 
{
    public enum AgentState { Idle, Chasing }
    public AgentState state = AgentState.Chasing;
    public Transform target; // Drag your FPS Controller here

    private NavMeshAgent navMeshAgent;
    private Animator animController;
    private int speedHashId;

    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        speedHashId = Animator.StringToHash("walkingSpeed");
        
        // Find player automatically if you forgot to drag it
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (state == AgentState.Chasing) Chase();
        else Idle();
    }

    void Chase() {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.position);
        
        // Play walk anim if moving, idle if stopped at distance
        float speed = (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) ? 0f : 1f;
        animController.SetFloat(speedHashId, speed);
    }

    void Idle() {
        navMeshAgent.isStopped = true;
        animController.SetFloat(speedHashId, 0f);
    }
}