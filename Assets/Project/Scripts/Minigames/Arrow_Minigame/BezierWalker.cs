using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Minigames.Arrow_Minigame;
using UnityEngine;

public class BezierWalker : MonoBehaviour
{
    [SerializeField] private BezierCurve curve;
    [SerializeField] private float speed;
    [SerializeField] private float slowSpeed;
    private float t = 0f;
    private float currentSpeed;
    private GameObject arrowTarget;
    
    private void Start()
    {
        currentSpeed = speed;
        arrowTarget = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(t >= .69f && t <= .89f)
        {
            t += Time.deltaTime * slowSpeed;
            if(!arrowTarget.activeSelf)
                arrowTarget.SetActive(true);
        }
        else
        {
            t += Time.deltaTime * speed;
            if(arrowTarget.activeSelf)
                arrowTarget.SetActive(false);
        }
        var position = curve.GetPoint(t);
        transform.position = position;
        var direction = curve.GetDirection(t);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
