using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.gravityScale = 1; 
    }

    void OnMouseDown()
    {
        isDragging = true;
        rb.gravityScale = 0; 
        rb.linearVelocity = Vector2.zero; 
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            transform.position = mousePosition + offset;
        }
    }

    void OnMouseUp()
{
    isDragging = false;
    rb.gravityScale = 1; 


    Collider2D shelfCollider = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Shelf"));
    if (shelfCollider != null && shelfCollider.CompareTag("Shelf"))
    {
        
        Vector3 shelfPosition = shelfCollider.transform.position;
        transform.position = new Vector3(transform.position.x, shelfPosition.y, shelfPosition.z);
        rb.gravityScale = 0; 
        rb.linearVelocity = Vector2.zero; 
    }
}

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
