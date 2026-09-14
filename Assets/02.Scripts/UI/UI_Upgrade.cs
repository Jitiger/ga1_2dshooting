using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrade : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _scoreCostText;

    public void OnClick()
    {
        UpgradeManager.Instance.LevelUp(_index);
    }

    public void Refresh()
    {
        Upgrade upgrade = UpgradeManager.Instance.Upgrades[_index];

        _titleText.text = $"{upgrade.Name} Lv.{upgrade.Level}";
        _valueText.text = $"{upgrade.CurrentValue} -> {upgrade.NextValue}";
        _titleText.text = $"{upgrade.Cost:N0}";
    }
}