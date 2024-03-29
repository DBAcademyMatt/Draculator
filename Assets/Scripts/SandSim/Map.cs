using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;
    public Chunk[,] Chunks = new Chunk[GameData.WorldSizeX, GameData.WorldSizeY];
    public Vector2Int Offset = Vector2Int.zero;

    public Vector2 Gravity = new Vector2(0, -0.98f);

    private void Start()
    {
        Instance = this;
        for (int y = 0; y < Chunks.GetLength(1); y++)
        {
            for (int x = 0; x < Chunks.GetLength(0); x++)
            {
                Chunks[x, y] = new Chunk(x,y);
            }
        }
    }

    private void Update()
    {
        for(int x = 0; x < Chunks.GetLength(0); x++)
        {
            for (int y = 0; y < Chunks.GetLength(1); y++)
            {
                if (Chunks[x, y].Simulated)
                {
                    Chunks[x, y].SimulateChunk();
                }
            }
        }

        for (int x = 0; x < Chunks.GetLength(0); x++)
        {
            for (int y = 0; y < Chunks.GetLength(1); y++)
            {
                if (Chunks[x, y].NeedsDrawn)
                    Chunks[x, y].RedrawChunk();
            }
        }
    }

    public Chunk GetChunkAt(Vector2Int worldPos)
    {
        int chunkx = worldPos.x / GameData.ChunkSize;
        int chunky = worldPos.y / GameData.ChunkSize;

        if (!IsWithinBounds(worldPos))
            return null;

        return Chunks[chunkx, chunky];
    }

    public BaseElement GetElementAt(Vector2Int worldPos)
    {
        int chunkx = worldPos.x / GameData.ChunkSize;
        int chunky = worldPos.y / GameData.ChunkSize;

        int tileX = worldPos.x % GameData.ChunkSize;
        int tileY = worldPos.y % GameData.ChunkSize;

        if (!IsWithinBounds(worldPos))
            return null;

        return Chunks[chunkx, chunky].Tiles[tileX, tileY];
    }

    public void SetElementAt(Vector2Int worldPos, BaseElement element)
    {   
        int chunkx = worldPos.x / GameData.ChunkSize;
        int chunky = worldPos.y / GameData.ChunkSize;

        int tileX = worldPos.x % GameData.ChunkSize;
        int tileY = worldPos.y % GameData.ChunkSize;

        if (IsWithinBounds(worldPos))
        {
            Chunks[chunkx, chunky].Tiles[tileX, tileY] = element;
            Chunks[chunkx, chunky].Simulated = true;
            Chunks[chunkx, chunky].NeedsDrawn = true;
        }
    }

    public bool IsWithinBounds(Vector2Int pos)
    {
        int chunkx = pos.x / GameData.ChunkSize;
        int chunky = pos.y / GameData.ChunkSize;

        int tileX = pos.x % GameData.ChunkSize;
        int tileY = pos.y % GameData.ChunkSize;

        if (chunkx < 0 || chunky < 0 
            || chunkx >= GameData.WorldSizeX || chunky >= GameData.WorldSizeY 
            || tileX < 0 || tileY < 0 
            || tileX >= GameData.ChunkSize * GameData.WorldSizeX || tileY >= GameData.ChunkSize * GameData.WorldSizeY)
        {
            return false;
        }

        return true;
    }

    public bool IsSolid(Vector2Int pos)
    {
        int chunkx = pos.x / GameData.ChunkSize;
        int chunky = pos.y / GameData.ChunkSize;

        int tileX = pos.x % GameData.ChunkSize;
        int tileY = pos.y % GameData.ChunkSize;

        if (chunkx < 0
            || chunky < 0
            || chunkx >= GameData.WorldSizeX
            || chunky >= GameData.WorldSizeY
            || tileX < 0
            || tileY < 0
            || tileX >= GameData.ChunkSize * GameData.WorldSizeX
            || tileY >= GameData.ChunkSize * GameData.WorldSizeY)
        {
            return true;
        }
        else
        { 
            BaseElement element = Chunks[chunkx, chunky].Tiles[tileX, tileY];
            if (element != null
                && (element.GetType() == typeof(DynamicSolid)
                || element.GetType() == typeof(StaticSolid)))
            {
                return true;
            }
        }

        return false;
    }
}