///
///     MultiTooltip.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using System;

/// <summary>
/// Attribute that is like the default Unity Tooltip attribute but can be toggled between languages.
/// </summary>
public class MultiTooltip : PropertyAttribute
{
    /*
     * Attribute Fields
     */ 
    /// <summary>
    /// Stores the messages
    /// </summary>
    public readonly string[] message = new string[Enum.GetNames(typeof(Language)).Length];
 
    /// <summary>
    /// Constructor / How the attribute is defined
    /// </summary>
    /// <param name="EN">The English tooltip to show</param>
    /// <param name="JP">The Japanese tooltip to show</param>
    public MultiTooltip(string EN = "", string JP = "")
    {
        // Set the messages according to what is passed in
        message[(int)Language.English] = EN;
        message[(int)Language.日本語] = JP;

        // Set the order. This should be applied last.
        order = 1000;
    }

    /// <summary>
    /// Function to obtain the message based 
    /// </summary>
    /// <param name="lang">The language of the tooltip.</param>
    /// <returns>The string with the tooltip in the specified language</returns>
    public string GetMessage(Language lang)
    {
        return message[(int)lang];
    }
}