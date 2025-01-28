using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class livechance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gamemanager.instance.getLevel() == 0)
        {
            gamemanager.instance.setlive(0);
        }
        int a = 3 - gamemanager.instance.getlive();
        transform.GetChild(0).GetComponent<Text>().text = a.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
