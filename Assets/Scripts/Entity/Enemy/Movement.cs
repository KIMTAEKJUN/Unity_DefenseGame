using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }
}