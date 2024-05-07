using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField] float speed = 1f;

    [SerializeField] Rigidbody2D rb;

    public enum CreatureMovementTypeEnum { tf, physics };
    [SerializeField] CreatureMovementTypeEnum movementType = CreatureMovementTypeEnum.physics;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCreature(Vector3 direction)
    {
        if (movementType == CreatureMovementTypeEnum.physics)
        {
            MoveCreatureRb(direction);

        }
        else if (movementType == CreatureMovementTypeEnum.tf)
        {
            MoveCreatureTransform(direction);
        }
    }

    public void MoveCreatureRb(Vector3 direction)
    {
        Vector3 currentVelocity = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = (currentVelocity) + (direction * speed);
    }

    public void MoveCreatureTransform(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * speed;
    }
}
