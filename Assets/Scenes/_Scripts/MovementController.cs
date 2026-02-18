using UnityEngine;
using System.Collections;

public class UnitMovementController : MonoBehaviour
{
    public float moveSpeed = 4f;

    private GridManager grid;

    private void Start()
    {
        grid = FindFirstObjectByType<GridManager>();
    }

    public IEnumerator MoveToCell(Vector3Int targetCell)
    {
        Vector3 start = transform.position;
        Vector3 end = grid.CellToWorldCenter(targetCell);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.position = end;
    }
}
