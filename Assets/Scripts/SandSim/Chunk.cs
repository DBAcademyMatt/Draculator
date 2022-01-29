using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    Vector2Int ChunkPos;
    GameObject spriteObj;
    SpriteRenderer sr;
    public Texture2D Tex;
    public bool Simulated; // only simulated when a tile moves into or next to chunk.
    public bool NeedsDrawn; // has this been updated somehow and needs redrawn?

    public BaseElement[,] Tiles = new BaseElement[GameData.ChunkSize, GameData.ChunkSize];

    public Chunk(int _x, int _y)
    {
        Simulated = false;

        ChunkPos = new Vector2Int(_x, _y);

        Tex = new Texture2D(GameData.ChunkSize, GameData.ChunkSize, TextureFormat.RGBA32, false);
        Tex.filterMode = FilterMode.Point;
        spriteObj = new GameObject("tile" + _x + ", " + _y);
        spriteObj.transform.position = new Vector3(_x, _y, 0);
        spriteObj.transform.SetParent(Map.Instance.transform);
        sr = spriteObj.AddComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(Tex, new Rect(0, 0, GameData.ChunkSize, GameData.ChunkSize), Vector2.zero, GameData.ChunkSize);
        
        for (int y = 0; y < Tiles.GetLength(1); y++)
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                Tiles[x, y] = null;
            }
        }

        RedrawChunk();
    }


    public void SimulateChunk()
    {
        NeedsDrawn = true;

        bool updatedThisFrame = false;
        for (int y = 0; y < Tiles.GetLength(1); y++)
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                if (Tiles[x, y] != null && Tiles[x,y])
                {
                    Vector2Int pos = new Vector2Int(ChunkPos.x * GameData.ChunkSize + x, 
                                                        ChunkPos.y * GameData.ChunkSize + y);

                    if (Tiles[x, y].Tick(pos))
                    {
                        updatedThisFrame = true;
                    }
                }
            }
        }

        if (!updatedThisFrame)
            Simulated = false;
        
        //debug outline
        Vector3 a = new Vector3(spriteObj.transform.position.x, spriteObj.transform.position.y, 0);
        Vector3 b = a + new Vector3(1, 1, 0);
        Debug.DrawLine(a, a + new Vector3(1, 0, 0), Color.cyan);
        Debug.DrawLine(a, a + new Vector3(0, 1, 0), Color.cyan);
        Debug.DrawLine(b, b - new Vector3(1, 0, 0), Color.cyan);
        Debug.DrawLine(b, b - new Vector3(0, 1, 0), Color.cyan);
    }


    public void RedrawChunk()
    {
        for (int y = 0; y < Tiles.GetLength(1); y++)
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                Color c = Color.black;// Random.ColorHSV();
                if (Tiles[x, y] != null)
                {
                    Tiles[x, y].AlreadyUpdated = false;
                    c = Tiles[x, y].Color;
                }

                Tex.SetPixel(x, y, c);
            }
        }
        Tex.Apply();
        sr.sprite = Sprite.Create(Tex, new Rect(0, 0, GameData.ChunkSize, GameData.ChunkSize), Vector2.zero, GameData.ChunkSize);

        NeedsDrawn = false;
    }
}