///
///     SpecOverview.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Type Def
using Lang = LanguageModifier;
using Key = LanguageModifier.UIString;

/// <summary>
/// Custom Editor Window for displaying the list of Specs available 
/// </summary>
public class SpecOverviewWindow : EditorWindow
{
    /*
     * Static Members
     */ 
    /// <summary>
    /// The path to the resource folder contianing all the Player Specs we are analysing
    /// </summary>
    private static string resourceFolderPath = "Assets/Prefabs/Player Specs";
    /// <summary>
    /// The list of PlayerSpecs that we have gotten a hold of since we last refreshed.
    /// </summary>
    private static List<string> guids = new List<string>();

    /// <summary>
    /// Function to show the Window. This will be called by Unity automatically when you try to open this Window
    /// </summary>
    [MenuItem("Gameplay/Spec Overview")]            // Tells Unity to show this option in the menu bar
    public static void ShowWindow()
    {
        // Get a reference to the Window
        var window = GetWindow(typeof(SpecOverviewWindow));

        // Set Title
        window.titleContent.text = Lang.GetString(Key.SpecOverviewTitle);
    }

    /// <summary>
    /// Defines all the UI Drawing here
    /// </summary>
    void OnGUI()
    {
        // Set Window Min Size
        minSize = new Vector2(300.0f, 200.0f);

        // Label (Language-Based)
        GUILayout.Label(Lang.GetString(Key.SpecPrefabFolder));
        // Get the resource folder path
        resourceFolderPath = GUILayout.TextField(resourceFolderPath);

        // Label
        GUILayout.Label("Specs");
        if (GUILayout.Button(Lang.GetString(Key.Refresh)))
        {
            // Show a progress bar
            EditorUtility.DisplayProgressBar(Lang.GetString(Key.Loading), Lang.GetString(Key.Loading), 0.5f);
            System.Threading.Thread.Sleep(200);         // For demoing

            // Clear the list
            guids.Clear();

            // Find all the specs in the prefabs folder
            guids.AddRange(AssetDatabase.FindAssets("t:GameObject", new string[] { resourceFolderPath } ));
            

            // We are done. Clear the Progress Bar from the screen
            EditorUtility.ClearProgressBar();
        }

        // List all the Specs
        // -- Calculate widths and more
        const float NAME_LABEL_LENGTH = 100.0f;
        const float HEIGHT = 18.0f;
        const float VERTICAL_SPACING = HEIGHT + 2.0f;
        const float PADDING = 5.0f;
        const float START_HEIGHT = 5 * HEIGHT;
        float width = (position.width - NAME_LABEL_LENGTH * 0.1f * 10.0f - PADDING) * 0.245f;
        float horizontalSpacing = (position.width - NAME_LABEL_LENGTH * 0.1f * 10.0f - PADDING) * 0.25f;
        // -- Draw UI for each Spec
        for (int i = 0; i < guids.Count; ++i)
        {
            // Load it
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(GameObject));
            // Check if it is a Player Spec
            var spec = prefab.GetComponent<PlayerSpec>();
            // Add it to the list
            if (spec != null)
            {
                // Draw Name
                GUI.Label(new Rect(5, START_HEIGHT + VERTICAL_SPACING * i, NAME_LABEL_LENGTH, HEIGHT), spec.name);
                // Draw Progress Bars showing stats
                EditorGUI.ProgressBar(new Rect(NAME_LABEL_LENGTH + horizontalSpacing * 0.0f, START_HEIGHT + VERTICAL_SPACING * i, width, HEIGHT), (float)spec.Strength / PlayerSpec.MAX_SINGLE_VALUE, Lang.GetString(Key.Strength));
                EditorGUI.ProgressBar(new Rect(NAME_LABEL_LENGTH + horizontalSpacing * 1.0f, START_HEIGHT + VERTICAL_SPACING * i, width, HEIGHT), (float)spec.Agility / PlayerSpec.MAX_SINGLE_VALUE, Lang.GetString(Key.Agility));
                EditorGUI.ProgressBar(new Rect(NAME_LABEL_LENGTH + horizontalSpacing * 2.0f, START_HEIGHT + VERTICAL_SPACING * i, width, HEIGHT), (float)spec.Accuracy / PlayerSpec.MAX_SINGLE_VALUE, Lang.GetString(Key.Accuracy));

                // Draw Button to show the object in inspector
                if (GUI.Button(new Rect(NAME_LABEL_LENGTH + horizontalSpacing * 3.0f, START_HEIGHT + VERTICAL_SPACING * i, width, HEIGHT), Lang.GetString(Key.Open)))
                {
                    Selection.activeGameObject = spec.gameObject;
                }
            }            
        }
    }
}
