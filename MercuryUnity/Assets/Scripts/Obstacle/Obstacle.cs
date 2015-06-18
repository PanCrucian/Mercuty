using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public float animationTime = 2f;
    public float destroyAfterTime = 5f;
    [HideInInspector]
    public float time;
    [HideInInspector]
    public float offset;
    [HideInInspector]
    public bool allowAnimate;

    private bool playerEntered = false;

    public virtual void Update()
    {
        if (!allowAnimate)
            return;
        time += Time.deltaTime;
        if (time >= animationTime)
            time = animationTime;
        offset = time / animationTime;
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<PlayerElement>() != null)
            OnPlayerEnter();
    }

    public virtual void OnTriggerExit(Collider coll)
    {
        if (coll.GetComponent<PlayerElement>() != null)
        {
            ObstacleController.Instance.AllowSpawn();
            Game.Instance.score++;
            StartCoroutine(DestroyNumerator());
        }
    }

    public virtual void OnPlayerEnter() {
        if (playerEntered)
            return;
        playerEntered = true;
        ObstacleController.Instance.NotAllowSpawn();
        allowAnimate = true;
    }

    IEnumerator DestroyNumerator()
    {
        yield return new WaitForSeconds(destroyAfterTime);
        if (Player.Instance.healthState == Player.HealthStates.Life)
            Destroy(gameObject);
    }
}
