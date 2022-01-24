using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Texture2D Tex;
    public bool Simulated; // only simulated when a tile moves into or next to chunk.

    public BaseElement[,] Tiles = new BaseElement[GameData.ChunkSize, GameData.ChunkSize];
}
