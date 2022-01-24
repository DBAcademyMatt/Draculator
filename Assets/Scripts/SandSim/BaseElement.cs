using UnityEngine;

public abstract class BaseElement
{
    public abstract void Step();
    public abstract void Die();
    public abstract void DieAndReplace(BaseElement element);

    public Chunk Chunk;
    public Vector2Int Position;
}
