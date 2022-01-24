using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Chunk[,] Chunks = new Chunk[GameData.WorldSizeX, GameData.WorldSizeY];

    public BaseElement GetElementAt(Vector2Int worldPos)
    {
        int chunkx = worldPos.x / GameData.WorldSizeX;
        int chunky = worldPos.y / GameData.WorldSizeY;

        int tileX = worldPos.x - chunkx;
        int tileY = worldPos.y - chunky;

        return Chunks[chunkx, chunky].Tiles[tileX, tileY];
    }

    public void SetElementAt(Vector2Int worldPos, BaseElement element)
    {
        int chunkx = worldPos.x / GameData.WorldSizeX;
        int chunky = worldPos.y / GameData.WorldSizeY;

        int tileX = worldPos.x - chunkx;
        int tileY = worldPos.y - chunky;

        Chunks[chunkx, chunky].Tiles[tileX, tileY] = element;
    }
}
