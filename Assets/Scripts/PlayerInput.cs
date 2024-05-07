using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Script for keyboard WSAD or Arrows input handler
*
* @author 
*/
public class PlayerInput : MonoBehaviour
{
    [SerializeField] MovementController frogPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            input.y += -1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            input.x += -1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            input.x += 1;
        }

        frogPlayer.MoveCreature(input);
    }
}
