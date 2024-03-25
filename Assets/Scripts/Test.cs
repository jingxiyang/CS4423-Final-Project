using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public Transform ObjectToMove;
    private float distance;
    public float tweenSpeed;
    public Transform followTransform;
    public Ease easeSetting;

    private void Start()
    {

    }

    void Update()
    {
        MoveTween();
    }

    private void MoveOnCurve()
    {
        //increase distance
        distance += 5 * Time.deltaTime;

        //calculate position and tangent
        Vector3 tangent;
        // ObjectToMove.position = GetComponent<BGCcMath>().CalcPositionAndTangentByDistance(distance, out tangent);
        // ObjectToMove.rotation = Quaternion.LookRotation(tangent);
    }

    private void MoveTween()
    {
        ObjectToMove.transform
         .DOMove(followTransform.transform.position, tweenSpeed)
         .SetEase(Ease.OutBounce);
    }
}
