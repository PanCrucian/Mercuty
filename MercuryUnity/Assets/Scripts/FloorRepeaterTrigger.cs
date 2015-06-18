using UnityEngine;
using System.Collections;

public class FloorRepeaterTrigger : MonoBehaviour {

    private bool allowTrigger = true;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<PlayerElement>() != null)
            if (allowTrigger)
            {
                GetComponentInParent<FloorRepeater>().MoveAStep();
                allowTrigger = false;
                StartCoroutine(AllowTriggerNumerator());
            }
    }

    IEnumerator AllowTriggerNumerator()
    {
        yield return new WaitForEndOfFrame();
        allowTrigger = true;
    }
}
