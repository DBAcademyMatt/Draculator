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
        bool updatedThisFrame = false;
        for (int x = 0; x < Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                if (Tiles[x, y] != null && Tiles[x,y])
                {
                    Vector2Int lastPos = new Vector2Int(ChunkPos.x * GameData.ChunkSize + x, 
                                                        ChunkPos.y * GameData.ChunkSize + y);

                    Vector2Int newPos = Tiles[x, y].Step(lastPos);
                    if (newPos != lastPos)
                    {
                        Map.Instance.SetElementAt(newPos, Tiles[x, y]);
                        Map.Instance.SetElementAt(lastPos, null);
                        updatedThisFrame = true;
                    }

                    //if we are next to a chunk on any side, also simulate them.
                    if (x == 0 && ChunkPos.x > 0)
                        Map.Instance.Chunks[ChunkPos.x - 1, ChunkPos.y].Simulated = true;

                    if (x == GameData.ChunkSize - 1 && ChunkPos.y < Map.Instance.Chunks.GetLength(0) - 1)
                        Map.Instance.Chunks[ChunkPos.x + 1, ChunkPos.y].Simulated = true;

                    if (y == 0 && ChunkPos.y > 0)
                        Map.Instance.Chunks[ChunkPos.x, ChunkPos.y - 1].Simulated = true;

                    if (y == GameData.ChunkSize - 1 && ChunkPos.y < Map.Instance.Chunks.GetLength(1) - 1)
                        Map.Instance.Chunks[ChunkPos.x, ChunkPos.y + 1].Simulated = true;
                }
            }
        }

        RedrawChunk();

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

    void RedrawChunk()
    {
        for (int y = 0; y < Tiles.GetLength(1); y++)
        {
            Debug.Log("Redraw Y : " + y);
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                Color c = Color.black;// Random.ColorHSV();
                if (Tiles[x, y] != null)
                {
                    c = Tiles[x, y].Color;
                }

                Tex.SetPixel(x, y, c);
            }
        }
        Tex.Apply();
        sr.sprite = Sprite.Create(Tex, new Rect(0, 0, GameData.ChunkSize, GameData.ChunkSize), Vector2.zero, GameData.ChunkSize);
    }
}