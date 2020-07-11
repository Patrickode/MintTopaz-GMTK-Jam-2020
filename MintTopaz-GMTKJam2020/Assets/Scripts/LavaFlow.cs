using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    [Tooltip("How fast this object should move, in units per frame.")]
    [SerializeField] [Range(0, 10)] private float unitsMovedPerFrame = 1;
    [Tooltip("How fast this object rotates to face its next destination, in pi radians per frame " +
        "(i.e., \"this multiplied by pi\" radians.")]
    [SerializeField] [Range(0, 2)] private float piRadiansRotatedPerFrame = 1;
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

                //Get the direction to our destination and see if it's parallel to transform.forward.
                //Only rotate if they're not parallel.
                Vector3 destinationDir = waypoints[nextWaypointIndex].transform.position - transform.position;
                float destinationDot = Vector3.Dot(transform.forward, destinationDir);

                if (!Mathf.Approximately(Mathf.Abs(destinationDot), 1))
                {
                    //If destinationDir is pointing away from transform.forward, we should rotate 
                    //transform.forward to face destinationDir's opposite instead.
                    if (destinationDot < 0)
                    {
                        destinationDir = Vector3.Reflect(destinationDir, transform.forward);
                    }

                    transform.forward = Vector3.RotateTowards
                    (
                        transform.forward,
                        destinationDir,
                        piRadiansRotatedPerFrame * Mathf.PI * Time.deltaTime,
                        0.0f
                    );
                }
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
