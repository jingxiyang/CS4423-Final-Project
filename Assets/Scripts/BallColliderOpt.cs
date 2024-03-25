using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColliderOpt : MonoBehaviour
{

    private MoveWords moveWordsScript;
    private bool onceFlag;

    // Start is called before the first frame update
    void Start()
    {
        moveWordsScript = GameObject.FindObjectOfType<MoveWords>();
        onceFlag = true;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ActiveBalls" && onceFlag)
        {
            onceFlag = false;

            this.GetComponent<Rigidbody>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            this.gameObject.tag = "ActiveBalls";
            this.gameObject.layer = LayerMask.NameToLayer("ActiveBalls");

            // Get a vector from the center of the collided ball to the contact point
            ContactPoint contact = other.contacts[0];
            Vector3 CollisionDir = contact.point - other.transform.position;

            int currentIdx = other.transform.GetSiblingIndex();

            float angle = Vector3.Angle(CollisionDir, other.transform.forward);
            if (angle > 90)
                moveWordsScript.AddNewBallAt(this.gameObject, currentIdx + 1, currentIdx);
            else
                moveWordsScript.AddNewBallAt(this.gameObject, currentIdx, currentIdx);

            this.gameObject.GetComponent<BallColliderOpt>().enabled = false;
        }
    }
}
