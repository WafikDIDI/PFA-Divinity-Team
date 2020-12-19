using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(AIAnimationHandler))]
public abstract class Enemy : MonoBehaviour {

    protected AIAnimationHandler animationHandler = null;

    [Space]
    [Header("Speeds")]
    [SerializeField] protected float walkingSpeed = 1f;
    [SerializeField] protected float runningSpeed = 2.5f;

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
    [Header("States Stats")]
    [SerializeField] protected float detectionRange = 10f;
    [SerializeField] protected Color detectionRangeGizmoColor = Color.blue;
    [SerializeField] protected Transform tragetDetected = null;

    // Searching state variables
    protected Vector3? tragetLastPosition = null;
    protected bool isAtTragetLastPosition = false;
    protected bool isFirstTimeToSearch = true;
    [Space]
    [SerializeField] protected float searchingTime = 2f;

    [Space]
    [SerializeField] protected float agroRange = 20f;
    [SerializeField] protected Color agroRangeGizmoColor = Color.cyan;

    // Health of Enemy Handler
    [Space]
    [SerializeField] protected HealthBar healthBar = null;
    [SerializeField] protected HealthSystem healthSystem = new HealthSystem(100);


    protected virtual void Awake () {
        meshAgentComponent = GetComponent<NavMeshAgent>();
        animationHandler = GetComponent<AIAnimationHandler>();

        AIManager.Enemies.Add(this);
    }

    protected virtual void OnDestroy () {
        //AIManager.Instance.Enemies.Remove(this);
    }

    protected virtual void Start () => healthBar.Setup(healthSystem);

    protected virtual void Update () => StateCheck();

    protected virtual void StateCheck () {
        switch (currentState) {
            case AIState.OverWatch: OverWatch(); break;
            case AIState.Attack: AttackState(); break;
            case AIState.Searching: Searching(); break;
            case AIState.Patrol: Patrol(); break;
        }
    }

    protected bool OverLap (float radius, string tagtoCheck = "Player") {
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

    protected bool OverLap (float radius, out Transform hit, string tagtoCheck = "Player") {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, radius);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].gameObject.tag == tagtoCheck) {
                    hit = hits[i].transform;
                    return true;
                }
            }
        }

        hit = null;
        return false;
    }

    protected bool CheckIfPlayerInRange () {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, detectionRange);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].gameObject.tag == "Player") {
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

    protected IEnumerator SearchingRoutine () {
        yield return new WaitForSeconds((float)searchingTime);

        if (wayPoints.Count == 0) {
            currentState = AIState.OverWatch;
        } else {
            currentState = AIState.Patrol;
        }

        isFirstTimeToSearch = true;
    }

    protected virtual void AttackState () { }
    protected virtual void Patrol () {
        meshAgentComponent.isStopped = false;
        meshAgentComponent.SetDestination(wayPoints[currentWayPointIndex].position);

        meshAgentComponent.speed = walkingSpeed;

        if (meshAgentComponent.hasPath == false) {
            animationHandler.TriggerIdleAnimation();
            if (meshAgentComponent.pathPending == false) {
                currentWayPointIndex++;
                currentWayPointIndex %= wayPoints.Count;
            }
        } else {
            animationHandler.TriggerWalkAnimation();
        }

        if (CheckIfPlayerInRange()) {
            currentState = AIState.Attack;
        }
    }
    protected virtual void OverWatch () {
        meshAgentComponent.isStopped = true;
        animationHandler.TriggerIdleAnimation();

        if (OverLap(agroRange, out tragetDetected)) {
            currentState = AIState.Attack;
        }
    }

    protected virtual void Searching () {

        if (isAtTragetLastPosition == false) {
            meshAgentComponent.SetDestination((Vector3)tragetLastPosition);
            meshAgentComponent.isStopped = false;
        }

        if (meshAgentComponent.hasPath == false) {
            isAtTragetLastPosition = true;
            if (isFirstTimeToSearch) {
                isFirstTimeToSearch = false;
                StartCoroutine(SearchingRoutine());
            }
        }

        if (CheckIfPlayerInRange()) {
            currentState = AIState.Attack;
            isAtTragetLastPosition = false;
            isFirstTimeToSearch = true;
            StopCoroutine(SearchingRoutine());
        }
    }

    protected virtual void OnTriggerEnter (Collider other) {

        healthBar.gameObject.SetActive(true);

        if (other.CompareTag("Bullet")) {
            var bulletDamage = other.GetComponent<Bullet>().bulletDamage;
            healthSystem.Damage(bulletDamage, 2);
            var currentHealth = healthSystem.GetHealth();
            if (currentHealth <= 0) {
                gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.F)) {
                healthSystem.Damage(50, 2);
                var currentHealth = healthSystem.GetHealth();
                if (currentHealth <= 0) {
                    gameObject.SetActive(false);
                }
            }
        }
    }

}
