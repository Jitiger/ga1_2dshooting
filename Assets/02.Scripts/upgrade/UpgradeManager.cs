using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance = null;
    public static UpgradeManager Instance => _instance;

    [Header("업그레이드 데이터")]
    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

    [Header("업그레이드 UI")]
    [SerializeField] private UI_Upgrade[] _uiUpgrades;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }


    private void Start()
    {
        RefreshUI();
    }


    public void LevelUp(int index)
    {
        _upgrades[index].LevelUp();

        RefreshUI();
    }


    private void RefreshUI()
    {
        foreach (UI_Upgrade uiUpgrade in _uiUpgrades)
        {
            uiUpgrade.Refresh();
        }
    }
}