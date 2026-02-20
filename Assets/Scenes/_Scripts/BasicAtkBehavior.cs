using UnityEngine;

// will handle the basic attack once a target is deemed in range
// will be attached to a Unit gameObject and called from RangeScanner script
// a model for later build of game where abilties are attached and called from RangeScanner

public class BasicAtkBehavior : MonoBehaviour
{                           

    private AutoBattlerUnit unit; //stores parent unit

    private void Start()
    {
        unit = GetComponent<AutoBattlerUnit>(); // get the unit I am atttached to
    }              
    public void Attack(AutoBattlerUnit target) // can only attack units currently
    {
        if (target != null) //if target is alive and valid
        {
            target.TakeDamage(unit.damage); // call enemies TakeDamage function with this unit's damage value
        }
    } 
}
