using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concrete : StaticSolid
{
    public override bool Tick(Vector2Int Position)
    {
        return base.Tick(Position);
    }

    public override void DieAndReplace(BaseElement element)
    {

        base.DieAndReplace(element);
    }
}