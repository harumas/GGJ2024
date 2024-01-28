using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnomaly : AbstractEvent
{
    [SerializeField] private Transform[] scaleObjects;
    [SerializeField] private Vector3 RotationgSpeed;
    [SerializeField] private Animator HumanAnimator;
    private Quaternion rotation;
    private bool RotationCheck = false;
    // Start is called before the first frame update
    public override void Play()
    {
        foreach (Transform scaleObject in scaleObjects)
        {
            rotation = scaleObject.transform.localRotation;
         
        }
        if (HumanAnimator != null)
        {
            HumanAnimator.enabled = false;
        }
        RotationCheck = true;
    }

    public override void Rewind()
    {
        foreach (Transform scaleObject in scaleObjects)
        {
            scaleObject.transform.localRotation = rotation;
        }
        if (HumanAnimator != null)
        {
            HumanAnimator.enabled = true;
        }
        RotationCheck = false;
    }

    private void Update()
    {
        if(RotationCheck)
        {
            foreach (Transform scaleObject in scaleObjects)
            {
                scaleObject.transform.Rotate(RotationgSpeed);
            }
        }
      
    }
}
