using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Liquid", menuName = "SandSim/Elements/Create New Liquid")]
public class Liquid : BaseElement
{
    public override void Die()
    {
        //throw new System.NotImplementedException();
    }

    public override void DieAndReplace(BaseElement element)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnCreate()
    {
        Color = ColourRange.Evaluate(Random.value);
    }

    public override bool Tick(Vector2Int Position)
    {
        // throw new System.NotImplementedException();

        return false;
    }
}
