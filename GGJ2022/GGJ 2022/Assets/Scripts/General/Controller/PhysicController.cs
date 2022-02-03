using System;
using UnityEngine;

[Serializable]
public class PhysicController 
{
    [Header("Requirements")]
    [Space]
    [SerializeField] private Rigidbody body = default;

    [Header("Behaviour")]
    [Space]
    [SerializeField] private ForceMode forceMode = default;

    public void MoveXZ(float qty, char direction){
        //$"Move in {direction} with {qty}".Print();
        switch (direction)
        {
            case 'x':
                body.AddForce(Vector3.right * qty, forceMode);
                break;
            case 'z':
                body.AddForce(Vector3.forward * qty, forceMode);
                break;
        }
    }

    public void MoveInDirection(float qty, Vector3 direction)
    {
        body.AddForce(qty * direction, forceMode);
    }
}
