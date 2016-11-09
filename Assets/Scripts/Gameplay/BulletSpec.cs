///
///     BulletSpec.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using System;

/// <summary>
/// Simple class holding 2 variabels to demonstrate CustomPropertyDrawer
/// </summary>
[Serializable]   // Mark this class as Serializable, this lets it be editable in the Inspector in Unity
public class BulletSpec
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float speed;
}
