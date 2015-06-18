using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    public float scoreForRandomRotate = 10;
    public float waitForNextRotate = 10f;
    public float speed = 17.5f;
    public int[] rotations = new int[] {0,90,180,270};

    private float velo;
    private int selectedAngle;
    private float lastRotateTime;

    void Update()
    {
        if (Game.Instance.score >= scoreForRandomRotate)
        {
            if (Game.Instance.score % scoreForRandomRotate == 0)
            {
                if (Mathf.Abs(Time.time - lastRotateTime) >= waitForNextRotate || lastRotateTime == 0)
                    Rotate();

            }
            transform.rotation = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y,
                        Mathf.SmoothDamp(transform.rotation.eulerAngles.z, selectedAngle, ref velo, speed * Time.deltaTime));
        }
    }

    void Rotate()
    {
        lastRotateTime = Time.time;
        selectedAngle = rotations[Random.Range(0, rotations.Length)];
    }
}
