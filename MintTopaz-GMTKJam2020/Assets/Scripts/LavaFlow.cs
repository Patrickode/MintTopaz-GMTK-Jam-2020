using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    [Tooltip("How fast this object should move, in units per frame.")]
    [SerializeField] [Range(0, 10)] private float unitsMovedPerFrame = 1;
    [Tooltip("Whether to go backwards through the list of waypoints after reaching the end of it. This will " +
        "make the flow loop back and forth indefinitely.")]
    [SerializeField] private bool pingPong = false;
    [SerializeField] private List<GameObject> waypoints = null;
    private int nextWaypointIndex = 0;

    private void Update()
    {
        if (nextWaypointIndex < waypoints.Count)
        {
            if (Vector3.Distance(transform.position, waypoints[nextWaypointIndex].transform.position) > 0.01)
            {
                transform.position = Vector3.MoveTowards
                (
                    transform.position,
                    waypoints[nextWaypointIndex].transform.position,
                    unitsMovedPerFrame * Time.deltaTime
                );
            }
            else
            {
                nextWaypointIndex++;
            }
        }
        else if (pingPong)
        {
            waypoints.Reverse();
            nextWaypointIndex = 0;
        }
    }
}
