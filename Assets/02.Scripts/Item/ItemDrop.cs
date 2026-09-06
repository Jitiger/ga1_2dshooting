using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [System.Serializable]
    private class DropItemData
    {
        public GameObject ItemPrefab;
        public int Weight = 1;
    }

    [Header("아이템 드랍 확률")]
    [SerializeField] private float _dropChance = 0.3f;

    [Header("드랍 아이템")]
    [SerializeField] private DropItemData[] _dropItems;

    public void Drop()
    {
        // 30% 확률로 아이템 드랍
        float randomValue = Random.value;

        if (randomValue > _dropChance)
        {
            return;
        }

        GameObject selectedItem = SelectRandomItem();

        if (selectedItem == null)
        {
            return;
        }

        Instantiate(
            selectedItem,
            transform.position,
            Quaternion.identity
        );
    }

    private GameObject SelectRandomItem()
    {
        if (_dropItems == null || _dropItems.Length == 0)
        {
            Debug.LogError("Drop Items가 비어 있습니다.");
            return null;
        }

        int totalWeight = 0;

        foreach (DropItemData item in _dropItems)
        {
            if (item.Weight > 0)
            {
                totalWeight += item.Weight;
            }
        }

        if (totalWeight <= 0)
        {
            Debug.LogError("아이템 Weight의 합은 0보다 커야 합니다.");
            return null;
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (DropItemData item in _dropItems)
        {
            if (item.Weight <= 0)
            {
                continue;
            }

            randomValue -= item.Weight;

            if (randomValue < 0)
            {
                return item.ItemPrefab;
            }
        }

        return _dropItems[_dropItems.Length - 1].ItemPrefab;
    }
}