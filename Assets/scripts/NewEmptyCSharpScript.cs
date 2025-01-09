using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    private bool isOverlapping = false; // �v���C���[�Əd�Ȃ��Ă��邩��ǐ�
    private Transform leftHandTransform; // Left_Hand �� Transform ��ۑ�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOverlapping = true;

            Transform playerTransform = other.transform;
            // "Left_Hand" ���ċA�I�Ɍ���
            leftHandTransform = FindDeepChild(playerTransform, "Left_Hand");

            if (leftHandTransform != null)
            {
                Debug.Log("Found Left_Hand object.");
            }
            else
            {
                Debug.LogError("Left_Hand object not found under Player.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOverlapping = false;
            leftHandTransform = null;
            Debug.Log("Player is no longer overlapping with the object.");
        }
    }

    private void Update()
    {
        if (isOverlapping && Input.GetKeyDown(KeyCode.F))
        {
            if (leftHandTransform != null)
            {
                Debug.Log($"Attaching {gameObject.name} to {leftHandTransform.name}");
                transform.SetParent(leftHandTransform); // Left_Hand �̎q�I�u�W�F�N�g�ɐݒ�
                transform.localPosition = Vector3.zero; // �q�Ƃ��Ă̈ʒu�����Z�b�g
            }
            else
            {
                Debug.LogError("Cannot attach to Left_Hand. Transform is null.");
            }
        }
    }

    // �ċA�I�Ɏq�I�u�W�F�N�g���������郁�\�b�h
    private Transform FindDeepChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform result = FindDeepChild(child, childName);
            if (result != null)
                return result;
        }
        return null;
    }
}