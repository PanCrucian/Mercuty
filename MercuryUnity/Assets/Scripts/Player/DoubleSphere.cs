using UnityEngine;
using System.Collections;

public class DoubleSphere : Sphere {

    public MoveSides moveSide;

    public override void Start()
    {
        base.Start();
        minimumScale = Player.Instance.data.sphere.transform.localScale / Player.Instance.scaleFactor;
    }

    public override void Update()
    {
        base.Update();
        //двигаем
        transform.localPosition = new Vector3(
            Player.Instance.doubleDistance * Mathf.Sin(theta) * (moveSide == MoveSides.Right ? 1f : -1f),
            transform.localPosition.y,
            transform.localPosition.z
            );
        //меняем размер
        transform.localScale = Vector3.SmoothDamp(
            transform.localScale,
            offset < Player.Instance.scaleGate ? minimumScale : Player.Instance.data.sphere.transform.localScale,
            ref separateScaleVelocity,
            Player.Instance.scaleSpeed * Time.deltaTime);

        if (progressTime == Player.Instance.separateTime)
        {
            Off();
            Player.Instance.ReturnToNormal();
            ResetProgressTime();
        }
    }
}
