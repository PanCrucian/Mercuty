using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {

    public enum MoveSides
    {
        Left,
        Right
    }
    [HideInInspector]
    public float theta = 0;
    [HideInInspector]
    public Vector3 minimumScale;
    [HideInInspector]
    public float progressTime;
    [HideInInspector]
    public Vector3 separateScaleVelocity;
    [HideInInspector]
    public float offset;

    public virtual void Start()
    {
        progressTime = 0;
        offset = 0;
        separateScaleVelocity = Vector3.zero;
    }
    
    public virtual void Update()
    {
        if (Player.Instance.healthState == Player.HealthStates.Death)
            return;
        progressTime += Time.deltaTime;
        if (progressTime >= Player.Instance.separateTime)
            progressTime = Player.Instance.separateTime;
        offset = (float)(progressTime / Player.Instance.separateTime);
        theta = offset * Mathf.PI;
    }

    public void SeparateDouble()
    {
        Player player = Player.Instance;
        player.data.sphere.Off();
        foreach (DoubleSphere sphere in player.data.doubleSpheres)
            sphere.On();
    }

    public void SeparateQuadro()
    {
        Player player = Player.Instance;
        foreach (DoubleSphere sphere in player.data.doubleSpheres)
            sphere.Off();
        foreach (QuadroSphere sphere in player.data.quadroSpheres)
            sphere.On();
    }

    public void Off()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public virtual void On()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        separateScaleVelocity = Vector3.zero;
    }

    public void ResetProgressTime()
    {
        progressTime = 0;
    }
}
