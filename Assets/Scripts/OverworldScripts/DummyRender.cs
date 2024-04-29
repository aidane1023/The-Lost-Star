using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRender : MonoBehaviour
{
    public SpriteRenderer sprite;
    public SpriteRenderer spriteShadow;

    // Update is called once per frame
    void Update()
    {
        if (TrainingDummy.cleared)
        {
            sprite.sortingOrder = 0;
            spriteShadow.sortingOrder = -1;
        }
    }
}
