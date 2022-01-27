using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concrete : StaticSolid
{
    public override bool Step(Vector2Int Position)
    {
        return base.Step(Position);
    }

    public override void DieAndReplace(BaseElement element)
    {

        base.DieAndReplace(element);
    }
}