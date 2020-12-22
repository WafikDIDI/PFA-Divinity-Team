using DivinityPFA.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem = new HealthSystem(100);
    [SerializeField] private HealthBar healthBar;

    public enum State {
        Idle,
        Shooting,
    }

    [Space]
    private State state;
    private bool targetLocked;
    private bool isReadyToShoot;
    private List<GameObject> missiles;

    //References
    private Transform target;
    [SerializeField] private GameObject missileRef;
    private Transform shootingPointRef;

    //Stats
    [Space]
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private Color shootingRangeColor = Color.red;
    [Space]
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float damage;

    private void Awake () {
        state = State.Idle;
        isReadyToShoot = true;
        targetLocked = false;
        missiles = new List<GameObject>();
        shootingPointRef = transform.Find("FirePoint");

        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
    }

    private void OnDrawGizmos () {
        Gizmos.color = shootingRangeColor;
        Gizmos.DrawWireSphere(this.transform.position, shootingRange);

    }

    private void Update () {
        if (OverLap(shootingRange)) {
            targetLocked = true;
        } else {
            targetLocked = false;
        }

        switch (state) {
            case State.Idle:
                Idle();
                break;
            case State.Shooting:
                Shooting();
                break;
        }
    }

    private bool OverLap (float radius, string tagtoCheck = "Player") {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, radius);

        if (hits != null) {
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].gameObject.tag == tagtoCheck) {
                    target = hits[i].transform;
                    return true;
                }
            }
        }

        target = null;
        return false;
    }

    private void Idle () {

        if (targetLocked) {
            state = State.Shooting;
            return;
        }

        transform.Rotate(0, 2, 0);

    }

    private void Shooting () {

        if (!targetLocked) {
            state = State.Idle;
            return;
        }

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        if (isReadyToShoot) {
            StartCoroutine(BoolWaitRoutine);
            if (missiles.Count != 0) {
                if (missiles[0].activeSelf == true) {
                    CreateMissile();
                } else {
                    missiles[0].transform.position = shootingPointRef.position;
                    missiles[0].transform.rotation = shootingPointRef.rotation;
                    missiles[0].SetActive(true);
                    missiles.Add(missiles[0]);
                    missiles.RemoveAt(0);
                }
            } else {
                CreateMissile();
            }
        }

    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Bullet")) {
            healthSystem.Damage(other.GetComponent<Bullet>().bulletDamage, -1);
            healthBar.gameObject.SetActive(true);
            if (healthSystem.GetHealth() == 0) {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator BoolWaitRoutine {
        get {
            isReadyToShoot = false;
            yield return new WaitForSeconds(timeBetweenShots);
            isReadyToShoot = true;
        }
    }

    private void CreateMissile () {
        missiles.Add(Instantiate(missileRef, shootingPointRef.position, shootingPointRef.rotation) as GameObject);
        //missiles[missiles.Count - 1].GetComponent<Missile>().SetDamage(damage);
    }

}
