using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    [System.Serializable]
    public class CustomData
    {
        public Sphere sphere;
        public DoubleSphere[] doubleSpheres;
        public QuadroSphere[] quadroSpheres;
    }
    public CustomData data;

    public enum HealthStates
    {
        Life,
        Death
    }

    public enum SeparateStates
    {
        Normal,
        Double,
        Quadro
    }
    /// <summary>
    /// живой или мертвый
    /// </summary>
    public HealthStates healthState;
    /// <summary>
    /// Состояние деления игрока
    /// </summary>
    public SeparateStates separateState;
    /// <summary>
    /// Скорость движения
    /// </summary>
    public float moveSpeed = 7.5f;
    /// <summary>
    /// Время деления на части и обратно
    /// </summary>
    public float separateTime = 1f;
    /// <summary>
    /// Во сколько раз сфера будет меньше родителя
    /// </summary>
    public float scaleFactor = 2f;
    /// <summary>
    /// Скорость изменения масштаба
    /// </summary>
    public float scaleSpeed = 11.75f;
    /// <summary>
    /// Точка отступа после которой меняется размер в "нормальный" (большой)
    /// </summary>
    [Range(0f, 1f)]
    public float scaleGate = 0.7f;
    /// <summary>
    /// На какое растояние будут отделяться двоичные сферы
    /// </summary>
    public float doubleDistance = 1.35f;
    /// <summary>
    /// На какое растояние будут отделяться четверичные сферы
    /// </summary>
    public float quadroDistance = 0.75f;

    public float DistanceTraveled
    {
        get
        {
            return Vector3.Distance(startPosition, transform.position);
        }
    }

    private Vector3 separateScaleVelocity;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }
    private static Player _instance;
    private Vector3 startPosition;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (healthState == HealthStates.Life && Game.Instance.gameState == Game.GameStates.Game)
            MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
            Separate();
    }

    public void Separate()
    {
        if (healthState == HealthStates.Life && Game.Instance.gameState == Game.GameStates.Game)
        {
            switch (separateState)
            {
                case SeparateStates.Normal:
                    SeparateToDouble();
                    break;
                case SeparateStates.Double:
                    SeparateToQuadro();
                    break;
            }
        }        
    }
    
    /// <summary>
    /// Разделиться на 2 части
    /// </summary>
    public void SeparateToDouble()
    {
        separateState = SeparateStates.Double;
        data.sphere.SeparateDouble();
    }

    /// <summary>
    /// Разделиться на 4 части
    /// </summary>
    void SeparateToQuadro()
    {
        separateState = SeparateStates.Quadro;
        foreach (DoubleSphere sphere in data.doubleSpheres)
            sphere.SeparateQuadro();
    }

    public void ReturnToNormal()
    {
        separateState = SeparateStates.Normal;
        data.sphere.On();
    }
    
    /// <summary>
    /// Двигаем игрока
    /// </summary>
    void MovePlayer()
    {
        transform.localPosition += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    public void Die()
    {
        if (healthState == HealthStates.Death)
            return;
        healthState = HealthStates.Death;
        Game.Instance.GameOver();
    }

    public void Restart()
    {
        healthState = HealthStates.Life;
        transform.position = startPosition;
    }
}
