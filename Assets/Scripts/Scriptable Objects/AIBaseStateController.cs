using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBaseStateController : MonoBehaviour {

    // References
    private NavMeshAgent agent = null;
    public NavMeshAgent Agent { get => agent; }

    // Settings
    public List<Transform> wayPointsList = new List<Transform>();
    public int NextWayPoint = 0;

    // Inputs from Inspector
    [SerializeField] private bool isAIActive = false;
    [SerializeField] private State currentstate = null;
    [SerializeField] private State remainState = null;

    [HideInInspector] public float Timer = 0f;
    public float SearchWaitingTime = 3f;

    public bool IsPlayerDetected { get; private set; } = false;
    public GameObject Target { get; private set; } = null;
    public Vector3? TargetLastSeenPosition { get; private set; } = null;

    private void Awake () {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetupAI (bool aiActiveState, List<Transform> wayPointsList) {
        isAIActive = aiActiveState;
        this.wayPointsList = wayPointsList;

        if (isAIActive) {
            agent.enabled = true;
        } else {
            agent.enabled = false;
        }
    }

    private void Update () {
        if (!isAIActive)
            return;

        currentstate.UpdateState(this);
    }

    public void StateTransition (State nextState) {
        if(nextState != remainState) {
            currentstate = nextState;
            Debug.Log(currentstate.StateName);
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            IsPlayerDetected = true;
            print("Player");
            Target = other.gameObject;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player")) {
            IsPlayerDetected = false;
            TargetLastSeenPosition = Target.transform.position;
            Target = null;
        }
    }

}
