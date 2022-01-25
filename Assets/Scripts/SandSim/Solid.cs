using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Solid : BaseElement
{
    public override void OnCreate()
    {
        Color = ColourRange.Evaluate(Random.value);
    }
}
