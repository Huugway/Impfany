using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
   
    void LateUpdate()
    {
        if (player == null)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            if (gameObject == null) return;          // noch kein Spieler, nächsten Frame nochmal
            player = gameObject;
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

    }
}
