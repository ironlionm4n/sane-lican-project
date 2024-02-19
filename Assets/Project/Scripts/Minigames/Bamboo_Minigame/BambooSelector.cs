using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooSelector : MonoBehaviour
{
    private BambooMingameController m_Controller;


    // Start is called before the first frame update
    void Start()
    {
        m_Controller =transform.parent.parent.GetComponent<BambooMingameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
           // m_Controller.SetNormalWin(true);
        }

        if(collision.CompareTag("Crit"))
        {
            //m_Controller.SetCriticalWin(true);
        }
    }
}
