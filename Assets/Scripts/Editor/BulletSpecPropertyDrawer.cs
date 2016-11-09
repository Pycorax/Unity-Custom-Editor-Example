///
///     BulletSpecPropertyDrawer.cs
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
/// CustomPropertyDrawer for BulletSpec
/// </summary>
[CustomPropertyDrawer(typeof(BulletSpec))]      // Tells Unity that we will use this class as the PropertyDrawer for BulletSpec
public class BulletSpecPropertyDrawer : PropertyDrawer
{
    private const float WIDTH_PER_LABEL_LETTER = 10.0f;
    private const float FULL_LABEL_CUTOFF = 100.0f;

    /// <summary>
    /// Function to draw the UI for a single property
    /// </summary>
    /// <param name="position">The position where Unity will start drawing this property</param>
    /// <param name="property">The property that Unity is modifying</param>
    /// <param name="label">The label of this property. Could be a variable name in a component that serializes this property.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the reference to the Properties which we will be changing
        var damageProp = property.FindPropertyRelative("damage");
        var speedProp = property.FindPropertyRelative("speed");

        // Indicate that we are starting to draw the UI for a single property
        // -- We must do this as Unity will treat every field as one property if we do not do this. 
        // -- If Unity does that, every single value will be marked as changed when this property is part of a prefab
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, label);
        position.width *= 0.5f;

        // Unity auto-indents. But we don't want to indent the following as it is a single property. So we save it first and set it to 0 manually.
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        /*
         * Draw fields based on the BulletSpec's fields
         */
        // Create the field for the speed property
        string propLabel = position.width > FULL_LABEL_CUTOFF ? Lang.GetString(Key.Speed) : Lang.GetString(Key.SpeedShortForm);
        // -- Set the label width according to the length of the label
        EditorGUIUtility.labelWidth = propLabel.Length * WIDTH_PER_LABEL_LETTER;
        // -- Use PropertyField to generate a field automatically                
        EditorGUI.PropertyField(position, speedProp, new GUIContent(propLabel));
        // -- Increment the position so that we know where to draw the next field                
        position.x += position.width;

        // Create the field for the damage property
        propLabel = " " + (position.width > FULL_LABEL_CUTOFF ? Lang.GetString(Key.Damage) : Lang.GetString(Key.DamageShortForm));
        // -- Set the label width according to the length of the label
        EditorGUIUtility.labelWidth = propLabel.Length * WIDTH_PER_LABEL_LETTER;
        // -- Damage cannot be negative, so we must handle this manually using IntField to give us more control of the output
        // -- IntField() gives us a value set by the user which we can assign the property manually
        // -- PropertyField() does not and sets the value automatically
        var userSetDamage = EditorGUI.IntField(position, propLabel, damageProp.intValue);
        // -- Clamp the value to 0 and Max and set it
        damageProp.intValue = Mathf.Clamp(userSetDamage, 0, Int32.MaxValue);

        // Set indent back to what it was so that Unity can continue it's auto-indents normally on other properties.
        EditorGUI.indentLevel = indent;

        // Indicate that we have finished drawing the UI for this property
        EditorGUI.EndProperty();
    }
}