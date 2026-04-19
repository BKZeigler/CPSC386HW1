using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatRowUpdater : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Button plusButton;

    private AutoBattlerUnit unit;
    private System.Func<float> getter;
    private System.Action setter;
    private string statName;

    public void Initialize(AutoBattlerUnit unit, string statName, System.Func<float> getter, System.Action setter)
    {
        this.unit = unit;
        this.getter = getter;
        this.setter = setter;
        this.statName = statName;

        plusButton.onClick.AddListener(OnPlusClicked);
        Refresh();
    }

    private void OnPlusClicked()
    {
        if (unit.skillPoints <= 0) // if no skill points, do not do anything
            return;

        setter.Invoke(); // set the stat to updated value
        Refresh(); // update the label
        unit.spendSkillPoint();
    }

    private void Refresh()
    {
        label.text = $"{statName}: {getter()}";
    }
}