using UnityEngine;
using System.Collections;

public class SimpleObstacle : Obstacle {
    [System.Serializable]
    public class CustomData
    {
        public ObstacleDeathTrigger cubeLeft;
        public ObstacleDeathTrigger cubeRight;
    }
    public CustomData data;

    Vector3 cubeLeftStartPosition;
    Vector3 cubeRightStartPosition;
    float offsetUp;
    float offsetSlide;
    bool isUped;

    void Start()
    {
        UpdateStartCubePositions();
    }

    public override void Update()
    {
        base.Update();
        offsetUp = Mathf.Clamp01(offset * 2f);
        if (offsetUp >= 1f) {
            if (!isUped)
                OnUp();

            offsetSlide = (offset * 2f - offset - 0.5f) * 2f;

            data.cubeLeft.transform.localPosition = Vector3.Lerp(
                cubeLeftStartPosition,
                new Vector3(0f, data.cubeLeft.transform.localPosition.y, data.cubeLeft.transform.localPosition.z),
                offsetSlide);
            data.cubeRight.transform.localPosition = Vector3.Lerp(
                cubeRightStartPosition,
                new Vector3(0f, data.cubeRight.transform.localPosition.y, data.cubeRight.transform.localPosition.z),
                offsetSlide);
        }
        else
        {
            data.cubeLeft.transform.localPosition = Vector3.Lerp(
                cubeLeftStartPosition,
                new Vector3(data.cubeLeft.transform.localPosition.x, 0f, data.cubeLeft.transform.localPosition.z),
                offsetUp);
            data.cubeRight.transform.localPosition = Vector3.Lerp(
                cubeRightStartPosition,
                new Vector3(data.cubeRight.transform.localPosition.x, 0f, data.cubeRight.transform.localPosition.z),
                offsetUp);
        }
    }

    void OnUp()
    {
        isUped = true;
        UpdateStartCubePositions();
    }

    void UpdateStartCubePositions()
    {
        cubeLeftStartPosition = data.cubeLeft.transform.localPosition;
        cubeRightStartPosition = data.cubeRight.transform.localPosition;
    }
}
