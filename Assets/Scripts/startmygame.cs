using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this for working with UI components

public class startmygame : MonoBehaviour
{
    public Slider loadingSlider; // Reference to the UI slider for showing progress
    public GameObject loadingPanel; // Reference to the loading panel

    void Start()
    {
        if (PlayerPrefs.HasKey("firsttime"))
        {
            PlayerPrefs.DeleteKey("firsttime");
            PlayerPrefs.SetInt("level", 0);
        }
        GrandAdManager.TotalGGCoinsEarned = 0;
        GrandAdManager.score = 0;
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        // Show the loading panel
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        int nextSceneIndex;
        if (gamemanager.instance.getLevel() + 1 > 7)
        {
            gamemanager.instance.setLevel(0);
            nextSceneIndex = 1;
        }
        else
        {
            nextSceneIndex = gamemanager.instance.getLevel() + 1;
        }

        gamemanager.instance.setlive(0);

        // Begin loading the next scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneIndex);

        // While the scene is still loading, update the slider
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (loadingSlider != null)
            {
                loadingSlider.value = progress;
            }
            yield return null;
        }

        // Hide the loading panel when loading is complete
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Optional: Add logic here if needed
    }
}