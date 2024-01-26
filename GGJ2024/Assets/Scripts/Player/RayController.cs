using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
public class RayController : MonoBehaviour
{
    [SerializeField] private float distance;
    private void Awake()
    {
        Locator.Register(this);
    }

    public RaycastHit IsHitPC()
    {
        RaycastHit hit;
        var camera = Camera.main;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance);

        return hit;
    }
}