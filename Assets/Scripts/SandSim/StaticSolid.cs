using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSolid : Solid
{
    public StaticSolid(string _name, Color _color)
    {
        Color = _color;
    }

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

    public override void Step()
    {
        throw new System.NotImplementedException();
    }
}
