using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concrete : StaticSolid
{
    public override Vector2Int Step(Vector2Int Position)
    {

        return base.Step(Position);
    }

    public override void DieAndReplace(BaseElement element)
    {

        base.DieAndReplace(element);
    }
}