using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Liquid", menuName = "SandSim/Elements/Create New Liquid")]
public abstract class Liquid : BaseElement
{
    public override void OnCreate()
    {
        Color = ColourRange.Evaluate(Random.value);
    }
}
