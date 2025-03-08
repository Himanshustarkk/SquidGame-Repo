using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level_3_Start : MonoBehaviour
{
    public GameObject panel_arrow;
    public GameObject panel_start;
    Level_3_Conroller control_script;
    public GameObject InfoButton;

    // Start is called before the first frame update
    void Start()
    {
        control_script = FindObjectOfType<Level_3_Conroller>();
    }
    
    public void btn_start()
    {
        panel_start.SetActive(false);
        Time.timeScale = 1f;

        Debug.Log("m Pressed Anto");
        control_script.game_run = true;
        control_script.can_pull = true;

        InfoButton.SetActive(true);
        panel_arrow.SetActive(true);
        TextMeshProUGUI _GGCoinTextConsistent = GameObject.FindGameObjectWithTag("GGConsistenCoin").GetComponent<TextMeshProUGUI>();
        if (_GGCoinTextConsistent != null)
        {
            _GGCoinTextConsistent.text = GrandAdManager.TotalGGCoinsEarned.ToString();
        }


    }
}
