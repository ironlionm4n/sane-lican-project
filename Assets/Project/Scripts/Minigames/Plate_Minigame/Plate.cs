using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private bool crit = false;
    private bool hit = false;

    [Header("Rope Animation Settings:")]
    public AnimationCurve movementCurve;

    //How high the plate goes into the air during it's arc (0 for straight line)
    float heightY = 0f;

    //How long the arc takes
    float duration = 0;



    private Transform endZone;
    private Transform plateSpawn;

    private Queue<Plate> currentPlates;

    public float percentTraveled;


    // Start is called before the first frame update
    void Start()
    {

        //StartProjectile(0.5f, 0f);

    }

    public IEnumerator Curve(Vector3 start, Vector2 target)
    {

        float timePassed = 0f;

        //temp variable
        Vector2 end = target;

        while (timePassed < duration)
        {
            float linearT = timePassed / duration; //0 to 1 time
            percentTraveled = linearT;

            float heightT = movementCurve.Evaluate(linearT); //value from curve

            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height); //adding value of y axis

            timePassed += Time.deltaTime;

            yield return null;

        }

        //Made it to end so destroy
        Destroy(gameObject);
    }

    public void StartProjectile(float speed, float arcHeight, Transform endTarget)
    {
        duration = speed;
        heightY = arcHeight;

        endZone = endTarget;
        plateSpawn = GameObject.Find("PlateSpawnPoint").transform;

        StartCoroutine(Curve(plateSpawn.position, endZone.position));
    }

    public void Hit()
    {
        if(crit)
        {
            Debug.Log("Crit");
            Destroy(gameObject);
            return;
        }

        if (hit)
        {
            Debug.Log("Hit");
            Destroy(gameObject);
            return;
        }

        //Missed
        Debug.Log("Miss");
        Destroy(gameObject);
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crit"))
        {
            crit = true;
        }

        if (collision.CompareTag("Win"))
        {
            hit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Crit"))
        {
            crit = false;
        }

        if (collision.CompareTag("Win"))
        {
            hit = false;
        }
    }
}
