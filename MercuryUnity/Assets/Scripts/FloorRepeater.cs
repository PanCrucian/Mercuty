using UnityEngine;
using System.Collections;

public class FloorRepeater : MonoBehaviour {

    public int index;
    public Vector3 step;

    public void MoveAStep()
    {
        index += 2;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        transform.localPosition = step * index;
    }
}
