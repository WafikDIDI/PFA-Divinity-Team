using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBaseStateController : MonoBehaviour {

    private AIHandler aiHandler = null;
    // References
    private NavMeshAgent agent = null;
    public NavMeshAgent Agent { get => agent; }

    public static PlayerBase Player { get; private set; } = null;

    // Settings
    public List<Transform> wayPointsList = new List<Transform>();
    public int NextWayPoint = 0;

    // Inputs from Inspector
    [SerializeField] private bool isAIActive = false;
    [SerializeField] private State currentState = null;
    [SerializeField] private State remainState = null;

    public State CurrentState { get => currentState; }

    [HideInInspector] public float Timer = 0f;
    public float SearchWaitingTime = 3f;
    public bool isInCover = false;

    [Header("Sight Settings")]
    [Space]
    [SerializeField] private float sightDistance = 10f;

    public bool IsPlayerDetected { get; private set; } = false;
    public GameObject Target = null;
    public Vector3? TargetLastSeenPosition { get; private set; } = null;

    public float DetectionRange;
    public float ShootingRange = 30;
    // Cover player is hiding in or going to
    public Cover Cover = null;

    private void Awake () {
        if(Player == null) Player = FindObjectOfType<PlayerBase>();

        aiHandler = FindObjectOfType<AIHandler>();
        agent = GetComponent<NavMeshAgent>();

        AIHandler.AddAI(this);
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

        currentState.UpdateState(this);
    }

    public void StateTransition (State nextState) {
        if(nextState != remainState) {
            currentState = nextState;
            Debug.Log(currentState.StateName);
        }
    }

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player")) {
            var rayDirection = other.transform.position - transform.position;

            if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hitInfo)) {
                if (hitInfo.collider.CompareTag("Player")) {
                    IsPlayerDetected = true;
                    print("Player");
                    Target = other.gameObject;
                } else {
                    IsPlayerDetected = false;
                    print("Non Player");
                }
            }
        }
    }

}
