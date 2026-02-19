using UnityEngine;

public class AutoBattlerUnit : MonoBehaviour
{
    public UnitTeam team;
    public float thinkInterval = 0.3f;

    public float currentHealth = 100f;
    public float maxHealth = 100f;

    public float range = 1f;
    public float atkSpd = 1f;

    public float damage = 10f;

    private float thinkTimer;
    private GridManager grid;
    private Pathfinder pathfinder;
    private UnitMovementController mover;

    private RangeScanner rangeScanner;

    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        rangeScanner = GetComponent<RangeScanner>();
    }
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = team == UnitTeam.Ally ? Color.blue : Color.red; // if ally make blue, else red

        currentHealth = maxHealth; // On start, make sure health is at maximum
        healthBar.UpdateHealthBar(currentHealth, maxHealth); // update it visually

        grid = FindFirstObjectByType<GridManager>(); // intialize the scene grid it lives on
        pathfinder = new Pathfinder(grid); // this unit uses A* pathfinding, so we use that as the pathfinder
        mover = GetComponent<UnitMovementController>(); // intialize the mover which changes the unit's position visually

        grid.RegisterUnit(gameObject);
    }

    private void OnDestroy()
    {
        grid.UnregisterUnit(gameObject);
    }

    private void Update()
    {
        thinkTimer -= Time.deltaTime;
        if (thinkTimer <= 0f)
        {
            thinkTimer = thinkInterval;
            Think();
        }
    }

    private void Think()
    {
        // Movement Portion

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

        // Attack Portion
        if (rangeScanner != null)
        {
            rangeScanner.CheckRange();
        } else
        {
            Debug.LogWarning("RangeScanner component not found on " + gameObject.name);
        }

    }

    public AutoBattlerUnit FindClosestEnemy()
    {
        AutoBattlerUnit[] units = FindObjectsByType<AutoBattlerUnit>(FindObjectsSortMode.None);

        AutoBattlerUnit closest = null;
        float minDist = Mathf.Infinity;

        foreach (var u in units) // for each unit on the board
        {
            if (u == this || u.team == this.team) continue; // if only units on board are you or your team, skip rest of loop

            float d = Vector3.Distance(transform.position, u.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = u;
            }
        }

        return closest;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Destroy(gameObject);
        }
    }
}