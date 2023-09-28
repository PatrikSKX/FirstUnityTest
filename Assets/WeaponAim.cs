using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{

    // need to move hero and weapon sprites
    public SpriteRenderer characterSprite, weaponSprite;
    public Animator weaponAnim;
    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        Vector2 scale = transform.localScale;
        if (dir.x < 0)
        {
            scale.y = -1;
        } else if (dir.x >= 0) {
            scale.y = 1;
        }
        transform.localScale = scale;


        // Weapon behind or ahead based on Y position
        if (dir.y > 0)
        {
            // Player order must be at least 1
            weaponSprite.sortingOrder = characterSprite.sortingOrder - 1;
        }
        else
        {
            weaponSprite.sortingOrder = characterSprite.sortingOrder + 1;
        }

        bool attk = Input.GetKeyDown(KeyCode.Mouse0);
        if (attk)
        {
            Debug.Log("Attack");
            weaponAnim.SetTrigger("Attack");
        }

    }
}
