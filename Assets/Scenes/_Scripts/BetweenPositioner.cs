using UnityEngine;

public class BetweenPositioner : MonoBehaviour
{
    [SerializeField]
    private Transform[] allySpawnPoints;

    void Start()
    {
        var allies = FindObjectsByType<AutoBattlerUnit>(FindObjectsSortMode.None);

        for (int i = 0; i < allies.Length; i++)
        {
            if (i < allySpawnPoints.Length)
            {
                allies[i].transform.position = allySpawnPoints[i].position; //make ally x go to spawn point x
            }
        }
    }
}
