using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody2D rb2d;
    private bool isOnShelf = false;

    public string destinationTag = "Shelf";
    public float snapRadius = 0.5f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        if (isOnShelf) isOnShelf = false;

        rb2d.bodyType = RigidbodyType2D.Kinematic; 
        offset = transform.position - MouseWorldPosition();
    }

    void OnMouseDrag()
    {
        rb2d.MovePosition(MouseWorldPosition() + offset); 
    }

    void OnMouseUp()
    {
        
        Collider2D shelfCollider = Physics2D.OverlapCircle(transform.position, snapRadius, LayerMask.GetMask("Default"));

        if (shelfCollider != null && shelfCollider.CompareTag(destinationTag))
        {
            rb2d.MovePosition(shelfCollider.transform.position + new Vector3(0, 0, -0.01f));
            isOnShelf = true;
        }

        rb2d.bodyType = isOnShelf ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic; 
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, snapRadius); 
    }
}
