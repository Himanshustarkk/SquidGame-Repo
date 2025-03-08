using System.Drawing;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class GrandAdManager : MonoBehaviour
{
    public static GrandAdManager instance;
    [SerializeField] bool showAds = true;
    public int adsAfter;

                                                                            //Declaring GGCoins and isWin Bool Variable

    public static int TotalGGCoinsEarned;
    public static int TotalScore;
    public static int score;
    public static string isWinOrLoseLevel;
    public static int counter=0;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        TotalGGCoinsEarned = 0;

    }

    [DllImport("__Internal")]
    private static extern void broadcastCustom(System.IntPtr message);
    public void ShowAd(string message)
    {
        if (showAds)
        {
            Debug.Log("Ads Function Called , Inside Condition");

            var utf8StrPtr = Marshal.StringToHGlobalAnsi(message);
            broadcastCustom(utf8StrPtr);
            Marshal.FreeHGlobal(utf8StrPtr);
        }
    }
    

}
