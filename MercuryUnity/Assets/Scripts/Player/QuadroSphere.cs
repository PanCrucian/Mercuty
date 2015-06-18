using UnityEngine;
using System.Collections;

public class QuadroSphere : Sphere {

    public MoveSides moveSide;
    public DoubleSphere doubleSphere;

    Vector3 startPosition;

    void ToDoubleSpheres()
    {
        minimumScale = Player.Instance.data.sphere.transform.localScale / (Player.Instance.scaleFactor * 2f);
        transform.localPosition = doubleSphere.transform.localPosition;
        startPosition = transform.localPosition;
        transform.localScale = doubleSphere.transform.localScale;
    }

    public override void Update()
    {
        base.Update();
        //двигаем
        transform.localPosition = new Vector3(
            startPosition.x + Player.Instance.quadroDistance * Mathf.Sin(theta) * (moveSide == MoveSides.Right ? 1f : -1f),
            startPosition.y,
            startPosition.z
            );
        //меняем размер
        transform.localScale = Vector3.SmoothDamp(
            transform.localScale,
            offset < Player.Instance.scaleGate ? minimumScale : doubleSphere.transform.localScale,
            ref separateScaleVelocity,
            Player.Instance.scaleSpeed * Time.deltaTime);

        if (progressTime == Player.Instance.separateTime)
        {
            Off();
            Player.Instance.SeparateToDouble();
            ResetProgressTime();
        }
    }

    public override void On()
    {
        ToDoubleSpheres();
        base.On();
    }
}
