using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform targetPlayer;
    public Vector3 offset = new Vector3(0f, 5f, -10f);

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) targetPlayer = player.transform;
    }

    private void LateUpdate()
    {
        if (!targetPlayer) return;
        transform.position = targetPlayer.position + offset;
    }
}
