using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Transform fixedTarget;

    public void LockTo(Transform target) => fixedTarget = target;
    public void Unlock() => fixedTarget = null;

    void LateUpdate()
    {
        if (fixedTarget != null)
        {
            transform.position = new Vector3(fixedTarget.position.x, fixedTarget.position.y, transform.position.z);
            return;
        }

        if (player == null)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            if (gameObject == null) return;
            player = gameObject;
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}