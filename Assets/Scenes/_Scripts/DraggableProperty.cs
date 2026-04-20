using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DragAndDropNewInput2D : MonoBehaviour
{
    [Header("Drag Control")]
    public bool canDrag = true;

    [Header("Raycast")]
    public LayerMask draggableLayers = ~0; 

    Camera cam;
    bool isDragging = false;
    Vector3 offset;
    float zDepth;

    Grid grid;

    void Awake()
    {
        cam = Camera.main;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (!canDrag) return;
        if (Mouse.current == null) return;

        var mouse = Mouse.current;

        if (mouse.leftButton.wasPressedThisFrame)
            TryBeginDrag(mouse.position.ReadValue());

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            if (isDragging)
                SnapToNearestHex();

            isDragging = false;
        }   

        if (isDragging)
        {
            Vector3 mouseWorld = GetMouseWorldPosition(mouse.position.ReadValue());
            transform.position = mouseWorld + offset;
        }
    }

    void TryBeginDrag(Vector2 screenPos)
    {
        Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, draggableLayers);

        if (hit.collider != null && hit.transform == transform)
        {
            zDepth = cam.WorldToScreenPoint(transform.position).z;
            offset = transform.position - GetMouseWorldPosition(screenPos);
            isDragging = true;
        }
    }

    Vector3 GetMouseWorldPosition(Vector2 screenPos)
    {
        Vector3 sp = new Vector3(screenPos.x, screenPos.y, zDepth);
        return cam.ScreenToWorldPoint(sp);
    }

    void SnapToNearestHex()
    {
        Vector3Int cell = grid.WorldToCell(transform.position); // get nearest cell to position

        Vector3 center = grid.GetCellCenterWorld(cell); // get the center of the cell

        transform.position = center; // make the position of unit the center of the cell
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        grid = FindFirstObjectByType<Grid>();
        cam = Camera.main;
    }
}