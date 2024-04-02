using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattlerAnimator : MonoBehaviour
{
    SpriteRenderer renderer;
    public SpriteState spriteState;
    public Sprite neutral, guard, thinking, hurt, cooldown;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator OnHurt()
    {
        if(spriteState != SpriteState.Guard)
        {
            renderer.sprite = hurt;
            yield return new WaitForSeconds(0.4f);
            if (spriteState != SpriteState.Think) renderer.sprite = neutral;
        }
    }

    public void OnGuard()
    {
        renderer.sprite = guard;
        spriteState = SpriteState.Guard;
    }

    public void OnThink()
    {
        renderer.sprite = thinking;
        spriteState = SpriteState.Think;
    }

    public void OnNeutral()
    {
        if (spriteState != SpriteState.Hurt)
        {
            renderer.sprite = neutral;
            spriteState = SpriteState.Neutral;
        }
    }

    public void OnCooldown()
    {
        if(spriteState != SpriteState.Hurt)
        {
            renderer.sprite = cooldown;
            spriteState = SpriteState.Cooldown;
        }

    }
}
public enum SpriteState
{
    Neutral,
    Guard,
    Think,
    Hurt,
    Cooldown
}

