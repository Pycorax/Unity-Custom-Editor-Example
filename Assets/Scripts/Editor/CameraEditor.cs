///
///     CameraEditor.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using UnityEditor;

// Type Def
using Lang = LanguageModifier;
using Key = LanguageModifier.UIString;  

/// <summary>
/// A Custom Editor for the built-in Unity Camera component
/// </summary>
[CustomEditor(typeof(Camera))]                  // Tells Unity that this will be replacing the default editor for Camera
[CanEditMultipleObjects]                        // Tells Unity that this that multiple instances of this object can be modified by this Editor
public class CameraEditor : Editor
{
    // Constants
    private const float DEFAULT_LOOK_AT_DISTANCE = 4.0f;

    // Members
    private Camera camera;                  // A handle to the camera that we are editing but casted for ease
    private Vector3 camLookAt;              // A Vector representing the position which the camera is looking at

    // Assets
    private static GUIStyle header;         // Stores the style of a header in the inspector

    /// <summary>
    /// Function that gets called at the start 
    /// </summary>
    void Awake()
    {
        // Create the style for the header
        header = new GUIStyle()
        {
            fontStyle = FontStyle.Bold
        };
    }

    /// <summary>
    /// Function that is called when the Inspector first appears on screen
    /// </summary>
    void OnEnable()
    {
        // Get a casted reference first
        camera = (target as Camera);
        // Calculate the look at position first
        camLookAt = camera.transform.position + camera.transform.forward * DEFAULT_LOOK_AT_DISTANCE;
    }

    /// <summary>
    /// Function that defines how the inspector should be drawn
    /// </summary>
    public override void OnInspectorGUI()
    {
        // Use this function to draw the default fields
        DrawDefaultInspector();

        // Add additional fields here
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(Lang.GetString(Key.Others), header);
        camLookAt = EditorGUILayout.Vector3Field(Lang.GetString(Key.LookAt), camLookAt);
    }

    /// <summary>
    /// Functions that defines additional UI elements for the Scene View. 
    /// Use Handle class instead of GUI classes for creating the UI
    /// </summary>
    public void OnSceneGUI()
    {
        /*
         * Updating UI Values
         */ 
        // Update this position according to the current position and rotation
        camLookAt = camera.transform.position + camera.transform.forward * (camera.transform.position - camLookAt).magnitude;

        /*
         * Drawing UI
         */ 
        // Define the colour of the UI
        Handles.color = Color.yellow;
        // Draw a line in the look at direction
        Handles.DrawLine(camera.transform.position, camLookAt);
        // Draws a sphere to show where the camera is looking at
        Handles.SphereCap(0, camLookAt, Quaternion.identity, 0.2f);

        // Tell Unity that we are going to change something so start checking if we've changed anything
        EditorGUI.BeginChangeCheck();

        // Create a handle for users to adjust the look at position and store it
        camLookAt = Handles.PositionHandle(camLookAt, Quaternion.identity);

        // Check if there was anything changed
        if (EditorGUI.EndChangeCheck())
        {
            // We will be changing the object based on the new camLookAt so ask Unity to record the change
            Undo.RegisterCompleteObjectUndo(camera.transform, Lang.GetString(Key.TranslatedLookAt));
            // Update the Look At with the position that the user has changed
            camera.transform.LookAt(camLookAt);
        }
    }
}