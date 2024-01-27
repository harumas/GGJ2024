using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnomaly : AbstractEvent
{
    [SerializeField] private Transform[] scaleObjects;
    [SerializeField] private Vector3 RotationgSpeed;
    private Quaternion rotation;
    private bool RotationCheck = false;
    // Start is called before the first frame update
    public override void Play()
    {
        foreach (Transform scaleObject in scaleObjects)
        {
            rotation = scaleObject.transform.localRotation;
        }
        RotationCheck = true;
    }

    public override void Rewind()
    {
        foreach (Transform scaleObject in scaleObjects)
        {
            scaleObject.transform.localRotation = rotation;
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
