using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    private bool isOverlapping = false; // プレイヤーと重なっているかを追跡
    private Transform leftHandTransform; // Left_Hand の Transform を保存

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOverlapping = true;

            Transform playerTransform = other.transform;
            // "Left_Hand" を再帰的に検索
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
                transform.SetParent(leftHandTransform); // Left_Hand の子オブジェクトに設定
                transform.localPosition = Vector3.zero; // 子としての位置をリセット
            }
            else
            {
                Debug.LogError("Cannot attach to Left_Hand. Transform is null.");
            }
        }
    }

    // 再帰的に子オブジェクトを検索するメソッド
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