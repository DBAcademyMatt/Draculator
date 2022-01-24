using UnityEngine;

public abstract class BaseElement
{
    public Chunk Chunk;
    public Vector2Int Position;
    public Color Color;

    public abstract void Step();
    public abstract void Die();
    public abstract void DieAndReplace(BaseElement element);
    public abstract Color GetColor();
}
