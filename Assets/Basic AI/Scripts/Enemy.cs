using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour {

    public enum AIState {
        OverWatch,
        Patrol,
        Attack,
        Searching,
    }

    // State
    [SerializeField] protected AIState currentState = AIState.Patrol;

    //Components
    protected NavMeshAgent meshAgentComponent = null;

    // Patrol Settigns
    [SerializeField] protected List<Transform> wayPoints = new List<Transform>();
    [SerializeField] protected int currentWayPointIndex = 0;

    // Enemy Stats
    [Header("Stats")]
    [SerializeField] protected float detectionRange = 10f;
    [SerializeField] protected Color detectionRangeGizmoColor = Color.blue;
    [SerializeField] protected Transform tragetDetected = null;

    [Space]
    [SerializeField] protected float agroRange = 20f;
    [SerializeField] protected Color agroRangeGizmoColor = Color.cyan;

    protected virtual void Awake () {
        meshAgentComponent = GetComponent<NavMeshAgent>();
        //AIManager.Instance.Enemies.Add(this);
    }

    protected virtual void OnDestroy () {
        //AIManager.Instance.Enemies.Remove(this);
    }

    protected virtual void Update () => StateCheck();

    private void StateCheck () {
        switch (currentState) {
            case AIState.OverWatch: OverWatch(); break;
            case AIState.Attack: AttackState(); break;
            case AIState.Searching: Searching(); break;
            case AIState.Patrol: Patrol(); break;
        }
    } 

    protected bool OverLap(float radius, string tagtoCheck = "Player") {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, radius);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].gameObject.tag == tagtoCheck) {
                    return true;
                }
            }
        }

        return false;
    }

    protected bool CheckIfPlayerInRange () {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, detectionRange);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if(hits[i].gameObject.tag == "Player") {
                    tragetDetected = hits[i].transform;
                    return true;
                }
            }
        }

        tragetDetected = null;
        return false;
    }

    protected virtual void OnDrawGizmos () {
        Gizmos.color = detectionRangeGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, detectionRange);

        Gizmos.color = agroRangeGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, agroRange);
    }

    protected abstract void AttackState ();
    protected abstract void Patrol ();
    protected abstract void OverWatch ();
    protected abstract void Searching ();
}
