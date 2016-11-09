///
///     Player.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [MultiTooltip(
        EN: "The movement speed of the player.",
        JP: "プレイヤーの移動速度"
    )]
    [SerializeField]
    public float moveSpeed = 5.0f;
    [MultiTooltip(
        EN: "The default PlayerSpec used for this player's characteristics",
        JP: "プレイヤーの既定なPlayerSpec"
    )]
    [SerializeField]
    private PlayerSpec defaultSpec;

    [SerializeField]
    private List<BulletSpec> bullets;

    // Components
    private Camera camera;

    /*
     * Getters
     */ 
    public Camera Camera
    {
        get
        {
            if (camera == null)
            {
                camera = GetComponentInChildren<Camera>();
            }

            return camera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        movement.Normalize();
        movement *= moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    /// <summary>
    /// A Context Menu function that clears bullets from the List<BulletSpec>
    /// </summary>
    [ContextMenu("Clear Bullets")]                  // Tells Unity to put this function as an item in the Inspector Context Menu
    public void ClearBullets()
    {
        if (bullets == null) return;

        bullets.Clear();
    }
}
