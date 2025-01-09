using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 5f;          // 移動速度
    public float stopDistance = 1f;  // プレイヤーにどれくらい近づくか
    public float rotationSpeed = 5f; // 回転速度
    private Transform player;        // プレイヤーのTransform

    private void Start()
    {
        // Playerタグのオブジェクトを探す
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Please ensure the Player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // プレイヤーとの距離を計算
            Vector3 direction = player.position - transform.position;

            // Y軸を無視する（高さの違いを無視）
            direction.y = 0;

            float distance = direction.magnitude;

            // 指定距離より遠い場合は移動
            if (distance > stopDistance)
            {
                // プレイヤー方向への移動
                Vector3 moveDirection = direction.normalized;
                transform.position += moveDirection * speed * Time.deltaTime;

                // プレイヤー方向への回転
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}