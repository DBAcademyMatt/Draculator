using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Static Solid", menuName = "SandSim/Elements/Create New Static Solid")]
public class StaticSolid : Solid
{
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void DieAndReplace(BaseElement element)
    {
        throw new System.NotImplementedException();
    }

    public override Color GetColor()
    {
        return Color;
    }

    public override bool Step(Vector2Int Position)
    {
        //do nothing
        return false; 
    }
}
