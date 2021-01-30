using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Hiting
{
    private Ray _ray;
    public RaycastHit _hit;
    private int hitDistance;

    public Hiting(int hitDistance)
    {
        this.hitDistance = hitDistance;
    }

    public Hiting()
    {
        hitDistance = 20;
    }

    public bool getHit()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits =  Physics.RaycastAll(_ray, 20);
        if (Physics.Raycast(_ray, out _hit, hitDistance))
        {
            Debug.Log(_hit.collider.gameObject);
            return true;
        }

        return false;
    }

}
