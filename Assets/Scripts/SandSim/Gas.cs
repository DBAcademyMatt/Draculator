using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gas", menuName = "SandSim/Elements/Create New Gas")]
public abstract class Gas : BaseElement
{
    public override void OnCreate()
    {
        Color = ColourRange.Evaluate(Random.value);
    }
}
