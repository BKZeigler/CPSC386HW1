using UnityEngine;

// will call attack behaviors once a valid target is in range
// will be attached to a Unit gameObject
// in later build, will parse through attached ability components "Ability_X.cs" to check for range
public class RangeScanner : MonoBehaviour
{

    private AutoBattlerUnit unit; //stores parent unit

    public RangeScanner(AutoBattlerUnit unit) // constructor to set parent unit
    {
        this.unit = unit;
    }
    public void CheckRange()
    {
        // will be called from AutoBattlerUnit's Think() function after target is found and path is generated
        if (unit == null) return; // if no parent unit, return
        AutoBattlerUnit closestEnemy = unit.FindClosestEnemy();
        if (closestEnemy != null) // if there is an enemy
        {
            float distance = Vector3.Distance(transform.position, closestEnemy.transform.position);
            if (distance <= unit.range) //check if in range
            {
                GetComponent<BasicAtkBehavior>().Attack(closestEnemy); // calls the basic attack behavior for now, will be changed to parse through attached abilities in later build
            }
        }
    }
}
