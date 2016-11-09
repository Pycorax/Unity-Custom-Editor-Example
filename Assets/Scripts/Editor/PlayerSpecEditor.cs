///
///     PlayerSpecEditor.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using UnityEditor;
using System;

// Type Def
using Lang = LanguageModifier;
using Key = LanguageModifier.UIString;

/// <summary>
/// Custom Editor for the PlayerSpec class
/// </summary>
[CustomEditor(typeof(PlayerSpec))]                  // Tells Unity that this will be replacing the default editor for PlayerSpec
[CanEditMultipleObjects]                            // Tells Unity that this that multiple instances of this object can be modified by this Editor
public class PlayerSpecEditor : Editor
{
    // Members
    SerializedProperty[] specProperties;            // Array of SerializedProperty of the properties we wish to create the UI for
    PlayerSpec spec;                                // A handle to the PlayerSpec that we are editing but casted for ease

    /// <summary>
    /// Function called when this editor is enabled
    /// </summary>
    void OnEnable()
    {
        // Setup the SerializedProperties.
        // -- Get the base property
        var specProperty = serializedObject.FindProperty("spec");
        // -- It is an array so we should populate our array with the SerializedProperties
        specProperties = new SerializedProperty[specProperty.arraySize];
        for (int i = 0; i < specProperties.Length; ++i)
        {
            specProperties[i] = specProperty.GetArrayElementAtIndex(i);
        }

        // Get a reference to the object we are editing
        spec = (PlayerSpec) target;
    }

    /// <summary>
    /// Defines the code for drawing the inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        // Make Read Only
        GUI.enabled = false;
        // Show the Current Total attributes
        EditorGUILayout.IntField(Lang.GetString(Key.Total), spec.GetTotal());
        // Unset Read Only
        GUI.enabled = true;

        /*
         * Min and Max Buttons
         */
        // Begin a horizontal group, the controls within it will move horizontally
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(Lang.GetString(Key.Min))) setAllValues(0);
        if (GUILayout.Button(Lang.GetString(Key.Average))) setAllValues(PlayerSpec.MAX_SINGLE_VALUE >> 1);
        if (GUILayout.Button(Lang.GetString(Key.Max))) setAllValues(PlayerSpec.MAX_SINGLE_VALUE);
        // End a horizontal group, the controls after it will resume normal vertical layouting
        EditorGUILayout.EndHorizontal();

        /*
         * Slider for every spec
         */ 
        for (int i = 0; i < specProperties.Length; ++i)
        {
            specProperties[i].intValue = EditorGUILayout.IntSlider(Lang.GetString(Key.Strength + i), specProperties[i].intValue, 0, PlayerSpec.MAX_SINGLE_VALUE);
        }

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI if modifying values via serializedObject.
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Function to set all the specs to the same specified value
    /// </summary>
    /// <param name="val">The value to set</param>
    private void setAllValues(int val)
    {
        for (int i = 0; i < specProperties.Length; ++i)
        {
            specProperties[i].intValue = val;
        }
    }
}