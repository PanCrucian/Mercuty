using UnityEngine;
using System.Collections;

public class AdvancedObstacle : Obstacle {
    [System.Serializable]
    public class CustomData
    {
        public ObstacleDeathTrigger middleCubeLeft;
        public ObstacleDeathTrigger middleCubeRight; 
        public ObstacleDeathTrigger cubeLeft;
        public ObstacleDeathTrigger cubeRight;
    }
    public CustomData data;

    Vector3 middleCubeLeftStartPosition;
    Vector3 middleCubeRightStartPosition;
    Vector3 cubeLeftStartPosition;
    Vector3 cubeRightStartPosition;

    float middleOffsetUp;
    float middleOffsetSlide;
    bool middleIsUped;

    void Start()
    {
        UpdateMiddleStartCubePositions();
        cubeLeftStartPosition = data.cubeLeft.transform.localPosition;
        cubeRightStartPosition = data.cubeRight.transform.localPosition;
    }

    public override void Update()
    {
        base.Update();
        middleOffsetUp = Mathf.Clamp01(offset * 2f);
        if (middleOffsetUp >= 1f)
        {
            if (!middleIsUped)
                OnMiddleUp();

            middleOffsetSlide = (offset * 2f - offset - 0.5f) * 2f;

            data.middleCubeLeft.transform.localPosition = Vector3.Lerp(
                middleCubeLeftStartPosition,
                new Vector3(0f, data.middleCubeLeft.transform.localPosition.y, data.middleCubeLeft.transform.localPosition.z),
                middleOffsetSlide);
            data.middleCubeRight.transform.localPosition = Vector3.Lerp(
                middleCubeRightStartPosition,
                new Vector3(0f, data.middleCubeRight.transform.localPosition.y, data.middleCubeRight.transform.localPosition.z),
                middleOffsetSlide);
        }
        else
        {
            data.middleCubeLeft.transform.localPosition = Vector3.Lerp(
                middleCubeLeftStartPosition,
                new Vector3(data.middleCubeLeft.transform.localPosition.x, 0f, data.middleCubeLeft.transform.localPosition.z),
                middleOffsetUp);
            data.middleCubeRight.transform.localPosition = Vector3.Lerp(
                middleCubeRightStartPosition,
                new Vector3(data.middleCubeRight.transform.localPosition.x, 0f, data.middleCubeRight.transform.localPosition.z),
                middleOffsetUp);
        }
        data.cubeLeft.transform.localPosition = Vector3.Lerp(
                cubeLeftStartPosition,
                new Vector3(data.cubeLeft.transform.localPosition.x, 0, data.cubeLeft.transform.localPosition.z),
                offset);
        data.cubeRight.transform.localPosition = Vector3.Lerp(
                cubeRightStartPosition,
                new Vector3(data.cubeRight.transform.localPosition.x, 0, data.cubeRight.transform.localPosition.z),
                offset);
    }

    void OnMiddleUp()
    {
        middleIsUped = true;
        UpdateMiddleStartCubePositions();
    }

    void UpdateMiddleStartCubePositions()
    {
        middleCubeLeftStartPosition = data.middleCubeLeft.transform.localPosition;
        middleCubeRightStartPosition = data.middleCubeRight.transform.localPosition;
    }

    public override void OnTriggerExit(Collider coll)
    {
        if (GetComponent<SimpleObstacle>() == null)
            base.OnTriggerExit(coll);
    }
}
