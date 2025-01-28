using UnityEngine;
using UnityEngine.UI;

namespace Gley.MobileAds.Internal
{
    public class MobileAdsExample : MonoBehaviour
    {
       public static MobileAdsExample Instance { set; get; }

        /// <summary>
        /// Initialize the ads
        /// </summary>
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
  //          Gley.MobileAds.API.Initialize();
        }

        void Start()
        {
           
        }

        /// <summary>
        /// Show banner, assigned from inspector
        /// </summary>
        public void ShawBanner()
        {
            Gley.MobileAds.API.ShowBanner(BannerPosition.Bottom, BannerType.Banner);
        }

        /// <summary>
        /// Hide banner assigned from inspector
        /// </summary>
        public void HideBanner()
        {
            Gley.MobileAds.API.HideBanner();
        }


        /// <summary>
        /// Show Interstitial, assigned from inspector
        /// </summary>
        public void ShowInterstitial()
        {
            Gley.MobileAds.API.ShowInterstitial();
        }

        /// <summary>
        /// Show rewarded video, assigned from inspector
        /// </summary>
        public void ShowRewardedVideo()
        {
            Gley.MobileAds.API.ShowRewardedVideo(CompleteMethod);
        }


        /// <summary>
        /// This is for testing purpose
        /// </summary>
        void Update()
        {
            
        }

        /// <summary>
        /// Complete method called every time a rewarded video is closed
        /// </summary>
        /// <param name="completed">if true, the video was watched until the end</param>
        private void CompleteMethod(bool completed)
        {
            if (completed)
            {
               
            }

           
        }
    }
}
