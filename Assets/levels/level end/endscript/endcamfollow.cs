﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endcamfollow : MonoBehaviour
{
    GameObject playercContainer;
    Vector3 offset;
    public float speed;
    public Transform winfollow;
    endplayerctr playercont;
    // Start is called before the first frame update
    void Start()
    {
        playercont = GameObject.FindObjectOfType<endplayerctr>();
        if (playercont != null)
        {
            playercContainer = playercont.gameObject;
            offset = transform.position - playercContainer.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playercContainer != null && !playercont.stopfolow)
        {
            transform.position = Vector3.Lerp(transform.position, playercContainer.transform.position + offset, speed * Time.deltaTime);
        }

        //if (playercont.winlevel)
        //{
        //    transform.position = Vector3.Lerp(transform.position, winfollow.position, 5 * Time.deltaTime);
        //}

    }
}
