using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour {
    // static
    private static List<Cover> covers = new List<Cover>();

    // non static
    public bool IsEmpty = true;

    private Transform attackPoint = null;
    public Transform AttackPoint { get => attackPoint; }

    public static Cover FindBestCover (Transform traget, RangedEnemy enemy, float enemyRange) {
        if (covers.Count == 0) { return null; }

        Cover bestCover = null;
        for (int i = 0; i < covers.Count; i++) {

            RaycastHit hit;
            Vector3 rayCastDirection = covers[i].transform.position - traget.position;
            if (Physics.Raycast(covers[i].transform.position, rayCastDirection, out hit)) {
                if (!hit.collider.CompareTag("Player") 
                    && covers[i].IsEmpty
                    && InDistance(traget, covers[i].transform, enemyRange) && InDistance(traget, covers[i].attackPoint, enemyRange)
                    && Vector3.Distance(bestCover.transform.position, enemy.transform.position) > 
                    Vector3.Distance(covers[i].transform.position, enemy.transform.position)) {
                    bestCover = covers[i];
                }
            }
        }

        return bestCover;
    }

    public static bool IsCoverValid (Cover cover, Transform tragetTransform) {
        if(cover == null) { return false; }

        RaycastHit hit;
        Vector3 rayCastDirection = cover.transform.position - tragetTransform.position;
        if (Physics.Raycast(cover.transform.position, rayCastDirection, out hit)) {
            if (!hit.collider.CompareTag(tragetTransform.tag)) {
                return true;
            }
        }

        return false;
    }

    private static bool InDistance (Transform trans1, Transform trans2, float maxDistance) {
        return Vector3.Distance(trans1.position, trans2.position) < maxDistance;
    }

    private void Awake () {
        covers.Add(this);
        attackPoint = transform.Find("AttackPoint");
    }

    private void OnDestroy () {
        covers.Remove(this);
    }

}
