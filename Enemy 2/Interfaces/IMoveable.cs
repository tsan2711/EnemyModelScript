using UnityEngine;

public interface IMoveable
{
    public Rigidbody RB {get; set;}
    public float Speed {get; set;}
    public bool IsMoving {get; set;}
    public void Move(Vector3 direction);

}
