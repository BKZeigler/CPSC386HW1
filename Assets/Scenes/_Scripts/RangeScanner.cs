using UnityEngine;

// will call attack behaviors once a valid target is in range
// will be attached to a Unit gameObject
// in later build, will parse through attached ability components "Ability_X.cs" to check for range
public class RangeScanner : MonoBehaviour
{

    private AutoBattlerUnit unit; //stores parent unit


    private void Start()
    {
        unit = GetComponent<AutoBattlerUnit>(); // get the unit I am attached to
    }
    public void CheckRange() // called every think interval from AutoBattlerUnit
    {
        if (unit == null) {
            return; // if no parent unit, return
        }
        AutoBattlerUnit closestEnemy = unit.FindClosestEnemy(); // use AutoBattlerUnit's find closest enemy
        if (closestEnemy != null) // if there is an enemy
        {
            float distance = Vector3.Distance(transform.position, closestEnemy.transform.position); // calc distance between
            if (distance <= unit.range) //check if in range, that distance < range
            {
                GetComponent<BasicAtkBehavior>().Attack(closestEnemy); // calls the basic attack behavior for now, will be changed to parse through attached abilities in later build
            }
        }
    }

    public bool InRange()
    {
            if (unit == null) {
                return false; // if no parent unit, return false
            }
            AutoBattlerUnit closestEnemy = unit.FindClosestEnemy(); // use AutoBattlerUnit's find closest enemy
            if (closestEnemy != null) // if there is an enemy
            {
                float distance = Vector3.Distance(transform.position, closestEnemy.transform.position); // calc distance between
                if (distance <= unit.range) //check if in range, that distance < range
                {
                    return true; // return true if in range
                }
            }
        return false; // return false if not in range
    }
}
