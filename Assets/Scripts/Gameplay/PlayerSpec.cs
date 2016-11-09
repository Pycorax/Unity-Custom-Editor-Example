///
///     PlayerSpec.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class that defines a Player's specifications
/// </summary>
public class PlayerSpec : MonoBehaviour
{
    /// <summary>
    /// Enums that indicate the different types of Specs
    /// </summary>
    public enum Spec
    {
        Strength,
        Agility,
        Accuracy
    }

    // Constants
    public const int MAX_TOTAL_VALUES = 150;
    public const int MAX_SINGLE_VALUE = 150;

    /// <summary>
    /// Stores the 3 Specs
    /// </summary>
    [SerializeField]
    private int[] spec = new int[Enum.GetNames(typeof(Spec)).Length];

    /*
     * Getters
     */ 
    public int Strength { get { return spec[(int)Spec.Strength]; } }
    public int Agility { get { return spec[(int)Spec.Agility]; } }
    public int Accuracy { get { return spec[(int)Spec.Accuracy]; } }

    /// <summary>
    /// Function to calculate and return the default values
    /// </summary>
    /// <returns>The total attributes added together</returns>
    public int GetTotal()
    {
        var val = 0;
        foreach (var i in spec)
        {
            val += i;
        }

        return val;
    }
}
