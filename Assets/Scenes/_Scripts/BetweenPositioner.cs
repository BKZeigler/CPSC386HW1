using TMPro;
using UnityEngine;

public class BetweenPositioner : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] allySpawnPoints;
    [SerializeField]
    private RectTransform[] characterPanels;

    [SerializeField]
    private StatRowUpdater statRowPrefab;

    void Start()
    {
        var allies = FindObjectsByType<AutoBattlerUnit>(FindObjectsSortMode.None);

        for (int i = 0; i < allies.Length; i++)
        {
            if (i < allySpawnPoints.Length)
            {
                allies[i].transform.position = allySpawnPoints[i].position; //make ally x go to spawn point x
                // display stats for that unit relative to spawn point
                createStatsDisplay(allies[i], characterPanels[i]);
            }
        }
    }

    void createStatsDisplay(AutoBattlerUnit unit, RectTransform panel)
    {
        var skillPointsObj = new GameObject("SkillPointsText");
        skillPointsObj.transform.SetParent(panel, false);
        var skillPointsText = skillPointsObj.AddComponent<TextMeshProUGUI>();
        skillPointsText.fontSize = 28;
        skillPointsText.alignment = TextAlignmentOptions.Center;
        skillPointsText.text = $"Skill Points: {unit.skillPoints}";

         unit.onSkillPointsChanged += (newValue) =>
        {
        skillPointsText.text = $"Skill Points: {newValue}";
        };

        CreateRow(panel, unit, "Health", () => unit.maxHealth, unit.increaseHealth);
        CreateRow(panel, unit, "Damage", () => unit.damage, unit.increaseAtk);
        CreateRow(panel, unit, "Speed", () => unit.thinkInterval, unit.increaseSpeed);
        CreateRow(panel, unit, "Range", () => unit.range, unit.increaseRange);
    }   

    void CreateRow(RectTransform parent, AutoBattlerUnit unit, string name, System.Func<float> getter, System.Action setter)
    {
        var row = Instantiate(statRowPrefab, parent);
        row.Initialize(unit, name, getter, setter);
    }
}
