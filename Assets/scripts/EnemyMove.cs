using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 5f;          // �ړ����x
    public float stopDistance = 1f;  // �v���C���[�ɂǂꂭ�炢�߂Â���
    public float rotationSpeed = 5f; // ��]���x
    private Transform player;        // �v���C���[��Transform

    private void Start()
    {
        // Player�^�O�̃I�u�W�F�N�g��T��
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
            // �v���C���[�Ƃ̋������v�Z
            Vector3 direction = player.position - transform.position;

            // Y���𖳎�����i�����̈Ⴂ�𖳎��j
            direction.y = 0;

            float distance = direction.magnitude;

            // �w�苗����艓���ꍇ�͈ړ�
            if (distance > stopDistance)
            {
                // �v���C���[�����ւ̈ړ�
                Vector3 moveDirection = direction.normalized;
                transform.position += moveDirection * speed * Time.deltaTime;

                // �v���C���[�����ւ̉�]
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}