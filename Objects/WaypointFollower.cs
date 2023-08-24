using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour {

    [SerializeField] private GameObject[] waypoints;
    private int currIdx = 0;
    [SerializeField] private float speed = 2f;

    private void Update() {
        GameObject currWaypoint = waypoints[currIdx];
        Vector3 platformPosition = transform.position;
        if (Vector2.Distance(currWaypoint.transform.position, platformPosition) < .1f) {
            currIdx++;
            if (currIdx >= waypoints.Length) {
                currIdx = 0;
            }
        }
        transform.position = Vector2
            .MoveTowards(platformPosition, currWaypoint.transform.position, Time.deltaTime * speed);
    }
}
