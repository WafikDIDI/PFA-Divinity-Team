using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour {

    //Stats
    [SerializeField] private float movementSpeed;
    [SerializeField] private int damageToGive;
    [SerializeField] private float lifeTime;

    private void Update () {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void OnEnable () {
        StartCoroutine(LifeTimeRoutine);
    }

    public void SetDamage (float value) {
        float value1 = value;

    }
    public float GetDamage () {
        return damageToGive;
    }

    private IEnumerator LifeTimeRoutine {
        get {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter (Collider collision) {
        if (collision.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerHealth>().healthSystem.Damage(damageToGive, 1);

        }

        Debug.Log("7it");
        gameObject.SetActive(false);

    }

}
