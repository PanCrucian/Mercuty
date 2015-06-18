using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class CameraFollow : MonoBehaviour {

    public Vector3 cameraFollowOffset = new Vector3(0, 5, -5);
    public Transform target;
    public float smooth = 1f;

    private Vector3 positionVelo;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Vector3.zero;
    }

    void Update()
    {
        if (!Application.isPlaying || smooth == 0)
            transform.position = target.position + cameraFollowOffset;
        else
            transform.position = Vector3.SmoothDamp(transform.position, target.position + cameraFollowOffset, ref positionVelo, smooth * Time.deltaTime);
    }
}
