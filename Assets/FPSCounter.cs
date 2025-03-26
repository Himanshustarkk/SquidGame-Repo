using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private GUIStyle style = new GUIStyle();
    private Rect rect;

    
    void Awake()
    {
        
      

        DontDestroyOnLoad(gameObject); // Keep it persistent across scenes

        //QualitySettings.vSyncCount = 0; // Disable VSync
        Application.targetFrameRate = 60; // Adjust based on devic

        // Set the target frame rate
        // Set up the style for the text
        style.fontSize = 100;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.UpperRight;

        // Position (Top-right corner)
        rect = new Rect(Screen.width - 150, 0, 10, 40);
    }

    void Update()
    {
        // FPS Calculation (Smoothed)
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        float fps = 1.0f / deltaTime;
        GUI.Label(rect, $"FPS: {Mathf.RoundToInt(fps)}", style);
    }
}
