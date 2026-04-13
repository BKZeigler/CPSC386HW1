using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDropNewInput2D : MonoBehaviour
{
    [Header("Drag Control")]
    public bool canDrag = true;

    [Header("Raycast")]
    public LayerMask draggableLayers = ~0; // assign "Units" layer here

    Camera cam;
    bool isDragging = false;
    Vector3 offset;
    float zDepth;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!canDrag) return;
        if (Mouse.current == null) return;

        var mouse = Mouse.current;

        if (mouse.leftButton.wasPressedThisFrame)
            TryBeginDrag(mouse.position.ReadValue());

        if (mouse.leftButton.wasReleasedThisFrame)
            isDragging = false;

        if (isDragging)
        {
            Vector3 mouseWorld = GetMouseWorldPosition(mouse.position.ReadValue());
            transform.position = mouseWorld + offset;
        }
    }

    void TryBeginDrag(Vector2 screenPos)
    {
        Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));

        // 2D raycast (point cast)
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
}