using UnityEngine;

public class AutoBattlerUnit : MonoBehaviour
{
    public UnitTeam team; // stores team of the unit
    public float thinkInterval = 0.3f; //seconds interval between moving/attacking

    public float currentHealth = 100f; //stores current hp
    public float maxHealth = 100f; // stores max hp

    public float range = 1f; // Not fully implemented, but still used for basic atk behavior range
    public float atkSpd = 1f; // not implemented yet

    public float damage = 10f; // the damage inflicted every think interval given an enemy is in range

    private float thinkTimer; // stores the decreasing timer that when hits zero causes a unit to "think"
    private GridManager grid; // provides the current grid in scene to the manager
    private Pathfinder pathfinder; // applies A* pathfinding to the current grid, updates manager on unit movement/positions
    private UnitMovementController mover; // moves the unit visually on the scene grid

    private RangeScanner rangeScanner; // used every "think" to check if targeted enemy is in range

    private HealthBar healthBar; // the updating visual health bar

    private BattleState battleState; // used to keep track of win/loss conditions; keep track of number of units

    private void Awake() //initializes components and their respective variables
    {
        healthBar = GetComponentInChildren<HealthBar>();
        rangeScanner = GetComponent<RangeScanner>();
    }
    private void Start() // Colors units based on team, starts units at full hp, and gives units other components
    {

        if(tag == "Ally") // double check for random spawn
        {
            team = UnitTeam.Ally;
        } else if (tag == "Enemy")
        {
            team = UnitTeam.Enemy;
        }

        GetComponent<SpriteRenderer>().color = team == UnitTeam.Ally ? Color.blue : Color.red; // if ally make blue, else red

        currentHealth = maxHealth; // On start, make sure health is at maximum
        healthBar.UpdateHealthBar(currentHealth, maxHealth); // update it visually

        grid = FindFirstObjectByType<GridManager>(); // intialize the scene grid it lives on
        pathfinder = new Pathfinder(grid); // this unit uses A* pathfinding, so we use that as the pathfinder
        mover = GetComponent<UnitMovementController>(); // intialize the mover which changes the unit's position visually
        battleState = FindFirstObjectByType<BattleState>(); //intialize the battle state to keep track of win/loss conditions

        if (team == UnitTeam.Ally) // keep track of number of units for win/loss conditions
        {
            battleState.AllyCount++; // if an ally, increment the ally count
        } else if (team == UnitTeam.Enemy)
        {
            battleState.EnemyCount++; // if an enemy, increment the enemy count
        }

        grid.RegisterUnit(gameObject); // register the unit on the manager's grid data to track position and occupancy
    }

    private void OnDestroy() // when a unit is destroyed on game stop or unit death
    {
        grid.UnregisterUnit(gameObject); // unregister from manager's grid
        if (team == UnitTeam.Ally)
        {
            battleState.AllyCount--; // if an ally death, decrement the ally count
        } else if (team == UnitTeam.Enemy)
        {
            battleState.EnemyCount--; // if an enemy death, decrement the enemy count
        }
    }

    private void Update() // called every frame to manage the think timer
    {
        thinkTimer -= Time.deltaTime; // decrease the think timer by the time between frames
        if (thinkTimer <= 0f) // when the timer hits zero,
        {
            thinkTimer = thinkInterval; // set the timer back to the interval
            Think(); // and make the unit "think"
        }
    }

    private void Think()
    {
        // Movement Portion done by Microsoft Copilot

        AutoBattlerUnit target = FindClosestEnemy(); //finds closest enemy and sets it as the target
        if (target == null) return;

        Vector3Int start = grid.WorldToCell(transform.position); // set the start position
        Vector3Int goal = grid.WorldToCell(target.transform.position); // set the goal position

        var path = pathfinder.FindPath(start, goal); //use those positions to find best path using A*
        if (path == null || path.Count < 2) return; // if no path or at target, do nothing

        Vector3Int nextCell = path[1]; //the cell it wants to move to is the 2nd cell in A*'s path or queue (1 is current)

        if (!grid.IsOccupied(nextCell)) // if the next cell is not occupied
        {
            grid.UpdateUnitPosition(gameObject, start, nextCell); //move on grid's data
            StopAllCoroutines(); // do not let other units move onto the same cell
            StartCoroutine(mover.MoveToCell(nextCell)); // move the unit visually on the scene grid/board
        }

        // Attack Portion done by me
        if (rangeScanner != null) // if unit has range scanner component
        {
            rangeScanner.CheckRange(); // check range for targeted enemy
        } else
        {
            Debug.LogWarning("RangeScanner component not found on " + gameObject.name);
        }

    }

    public AutoBattlerUnit FindClosestEnemy() // function done by Microsoft Copilot to find closest enemy
    {
        AutoBattlerUnit[] units = FindObjectsByType<AutoBattlerUnit>(FindObjectsSortMode.None); //make an array of all units

        AutoBattlerUnit closest = null; // default is that no units are present
        float minDist = Mathf.Infinity; // any unit will be closer than infinity distance

        foreach (var u in units) // for each unit on the board
        {
            if (u == this || u.team == this.team) continue; // do not keep track of myself or units on the same team

            float dist = Vector3.Distance(transform.position, u.transform.position); // calculate distance me and every enemy
            if (dist < minDist) // at first, will be closer than infinity so create an inital target (closest)
            {
                minDist = dist; // set the new minimum distance to beat (another unit needs to be closer to be new target)
                closest = u; // set this current unit as the closest target for now
            }
        }

        return closest; // returns closest enemy
    }

    public void TakeDamage(float amount) // function called by other sources to damage this unit
    {
        currentHealth -= amount; // decrease health by incoming damage amount
        healthBar.UpdateHealthBar(currentHealth, maxHealth); // update the health bar visually
        if (currentHealth <= 0f) // if health is zero
        {
            currentHealth = 0f; // avoid a negative health value for health bar
            Destroy(gameObject); // destroy this unit (death)
        }
    }
}