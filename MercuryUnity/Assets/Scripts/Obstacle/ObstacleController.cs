using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

    public Obstacle[] obstacles;
    public bool allowSpawn = false;
    public float spawnDelay = 1f;
    float lastAllowTime;

    public static ObstacleController Instance
    {
        get
        {
            return _instance;
        }
    }
    private static ObstacleController _instance;

    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (obstacles.Length == 0)
            return;
        if (allowSpawn && Mathf.Abs(Time.time - lastAllowTime) >= spawnDelay)
            Spawn();
    }

    void Spawn()
    {
        allowSpawn = false;
        int randomIndex = Random.Range(0, obstacles.Length);

        GameObject newObstacleGo = (GameObject)Instantiate(obstacles[randomIndex].gameObject);
        newObstacleGo.transform.parent = transform;
        newObstacleGo.transform.position = Player.Instance.transform.position;
    }

    public void NotAllowSpawn()
    {
        allowSpawn = false;
    }

    public void AllowSpawn()
    {
        allowSpawn = true;
        lastAllowTime = Time.time;
    }
}
