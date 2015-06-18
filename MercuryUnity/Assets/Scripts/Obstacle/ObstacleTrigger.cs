using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ObstacleTrigger : MonoBehaviour {

    private bool enterCalled;
    private bool exitCalled;

    void OnTriggerEnter(Collider coll)
    {
        Obstacle[] obstacles = transform.parent.GetComponents<Obstacle>();
        if (obstacles.Length > 0 && !enterCalled)
        {
            enterCalled = true;
            foreach (Obstacle obstacle in obstacles)
                obstacle.OnTriggerEnter(coll);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        Obstacle[] obstacles = transform.parent.GetComponents<Obstacle>();
        if (obstacles.Length > 0 && !exitCalled)
        {
            exitCalled = true;
            foreach (Obstacle obstacle in obstacles)
                obstacle.OnTriggerExit(coll);
        }
    }
}
