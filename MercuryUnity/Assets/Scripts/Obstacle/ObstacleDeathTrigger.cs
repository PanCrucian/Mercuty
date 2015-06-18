using UnityEngine;
using System.Collections;

public class ObstacleDeathTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<PlayerElement>() != null)
                Player.Instance.Die();
    }
}
