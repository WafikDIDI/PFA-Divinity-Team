using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour {

    private static Transform playerTransform = null;
    private static List<Cover> covers = new List<Cover>();

    [SerializeField] public bool isValid = true;


    /// <summary>
    /// Search for the closest valid cover from AI position.
    /// "Valid" meaning if the AI can hide behind it 
    /// </summary>
    /// <param name="aiTransform">Referee to the AI Transform, used to fetch current position</param>
    /// <returns></returns>
    public static Cover NearestValidCover (Transform aiTransform) {
        Cover closestCover;

        closestCover = covers[0];

        foreach (Cover cover in covers) {

            if (cover.isValid) {
                if (Vector3.Distance(aiTransform.position, cover.transform.position)
                < Vector3.Distance(aiTransform.position, closestCover.transform.position) ||
                !closestCover.isValid) {
                    closestCover = cover;
                }
            }

        }

        return closestCover;
    }

    public static void CheckIfValid (Transform playerTransform) {
        foreach (Cover cover in covers) {
            var rayDirection = playerTransform.position - cover.transform.position;

            if (Physics.Raycast(cover.transform.position, rayDirection, out RaycastHit hit)) {
                if (hit.collider.tag == "Player") {
                    cover.isValid = false;
                } else {
                    cover.isValid = true;
                }
            }
        }
    }

    private void Awake () {
        if (playerTransform == null) {
            playerTransform = FindObjectOfType<PlayerBase>().GetComponent<Transform>();
        }

        covers.Add(this);
    }
}
