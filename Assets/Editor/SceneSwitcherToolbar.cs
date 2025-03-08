#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
using System.Linq;
using System.IO;

[InitializeOnLoad]
public static class SceneSwitcherToolbar
{
    private static string[] sceneNames = new string[0];
    private static int selectedIndex = 0;

    private static float positionOffset = 350f; // Move closer to Play button
    private static float dropdownBoxHeight = 20f; // Dropdown button height

    private static bool fetchAllScenes
    {
        get => EditorPrefs.GetBool("SceneSwitcher_FetchAllScenes", false);
        set => EditorPrefs.SetBool("SceneSwitcher_FetchAllScenes", value);
    }

    static SceneSwitcherToolbar()
    {
        RefreshSceneList();
        EditorApplication.delayCall += AddToolbarUI;
    }

    static void AddToolbarUI()
    {
        var toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
        if (toolbarType == null) return;

        var toolbars = Resources.FindObjectsOfTypeAll(toolbarType);
        if (toolbars.Length == 0) return;

        var toolbar = toolbars[0];
        var rootField = toolbarType.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
        if (rootField == null) return;

        var root = rootField.GetValue(toolbar) as VisualElement;
        if (root == null) return;

        var leftContainer = root.Q("ToolbarZoneLeftAlign");
        if (leftContainer == null) return;

        IMGUIContainer container = new IMGUIContainer(OnGUI);
        container.style.marginLeft = positionOffset;

        leftContainer.Add(container);
    }

    static void OnGUI()
    {
        CheckAndRefreshScenes();

        if (selectedIndex >= sceneNames.Length)
            selectedIndex = 0;

        GUILayout.BeginHorizontal();

        // Fetch all scenes toggle button
        bool newFetchAllScenes = GUILayout.Toggle(fetchAllScenes, "All Scenes", "Button", GUILayout.Height(dropdownBoxHeight));
        if (newFetchAllScenes != fetchAllScenes)
        {
            fetchAllScenes = newFetchAllScenes;
            RefreshSceneList();
        }

        // Scene dropdown
        GUIStyle popupStyle = new GUIStyle(EditorStyles.popup)
        {
            fixedHeight = dropdownBoxHeight
        };

        int newIndex = EditorGUILayout.Popup(selectedIndex, sceneNames, popupStyle, GUILayout.Width(150), GUILayout.Height(dropdownBoxHeight));

        if (newIndex != selectedIndex)
        {
            selectedIndex = newIndex;
            LoadScene(sceneNames[selectedIndex]);
        }

        GUILayout.EndHorizontal();
    }

    static void RefreshSceneList()
    {
        if (fetchAllScenes)
        {
            sceneNames = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories)
                .Select(path => Path.GetFileNameWithoutExtension(path))
                .ToArray();
        }
        else
        {
            sceneNames = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                .ToArray();
        }
    }

    static void CheckAndRefreshScenes()
    {
        string[] currentScenes;
        if (fetchAllScenes)
        {
            currentScenes = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories)
                .Select(path => Path.GetFileNameWithoutExtension(path))
                .ToArray();
        }
        else
        {
            currentScenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                .ToArray();
        }

        if (!currentScenes.SequenceEqual(sceneNames))
        {
            sceneNames = currentScenes;
        }
    }

    static void LoadScene(string sceneName)
    {
        Debug.Log("<b><color=green>Thank You Ajay</color></b>");
        string scenePath;

        if (fetchAllScenes)
        {
            scenePath = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories)
                .FirstOrDefault(path => Path.GetFileNameWithoutExtension(path) == sceneName);
        }
        else
        {
            scenePath = EditorBuildSettings.scenes
                .FirstOrDefault(scene => scene.enabled && scene.path.Contains(sceneName))?.path;
        }

        if (!string.IsNullOrEmpty(scenePath))
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
        }
    }
}
#endif
