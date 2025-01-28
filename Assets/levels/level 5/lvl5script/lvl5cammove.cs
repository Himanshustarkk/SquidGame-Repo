using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl5cammove : MonoBehaviour
{
    Vector3 startpos,offset;
    public bool followmbl;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        startpos = transform.position;
        offset = transform.position - FindObjectOfType<lvl5marbleinstant>().transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!FindObjectOfType<lvl5marbleinstant>().win || !FindObjectOfType<lvl5marbleinstant>().lose)
        {
            if (GameObject.FindGameObjectWithTag("marble") != null && followmbl)
            {
                Transform tr = GameObject.FindGameObjectWithTag("marble").transform;
                transform.position = Vector3.Lerp(transform.position, tr.position + offset, 20 * Time.deltaTime);
            }
            else if(GameObject.FindGameObjectWithTag("marble")==null && !followmbl)
            {
                transform.position = Vector3.Lerp(transform.position, startpos, 100 * Time.deltaTime);
            }
        }
        

    }
}
