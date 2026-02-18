using UnityEngine;

// will handle the basic attack once a target is deemed in range
// will be attached to a Unit gameObject and called from RangeScanner script
// a model for later build of game where abilties are attached and called from RangeScanner

public class BasicAtkBehavior : MonoBehaviour
{                           

    private AutoBattlerUnit unit; //stores parent unit

    public BasicAtkBehavior(AutoBattlerUnit unit) // constructor to set parent unit
    {
        this.unit = unit;
    }                  
    public void Attack(AutoBattlerUnit target)
    {
        if (target != null) //if target is alive and valid
        {
            target.TakeDamage(unit.damage); // call enemies TakeDamage function with this unit's damage value
        }
    } 
}
