///
///     MultiTooltipProperty.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom Property Drawer for the MultiTooltip
/// </summary>
[CustomPropertyDrawer(typeof(MultiTooltip))]
public class MultiTooltipProperty : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Mark that we have started drawing the UI
        EditorGUI.BeginProperty(position, label, property);

        // First get the attribute since it contains the range for the slider
        MultiTooltip tooltip = attribute as MultiTooltip;

        // Create the property field with the information from the tooltip
        var content = new GUIContent(label.text, tooltip.GetMessage(LanguageModifier.Language));
        EditorGUI.PropertyField(position, property, content);

        // Mark that we have stopped drawing the UI
        EditorGUI.EndProperty();
    }
}