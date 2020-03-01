using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    private Tilemap tm;
    public Texture2D map;
    public int startX, startY, zOffset;
    public bool isBackground;
    public ColorToTile[] colorMappings;
    // Awake is called once, before Start
    // Swapped to Awake so that everything can be referenced in the Start method of other objects
    void Awake()
    {
        tm = GetComponent<Tilemap>();
        zOffset = 0;
        if (isBackground)
        {
            //Changes the z offset in positioning. Set to 0 as to not interfere with background components.
            zOffset = 0;
            //Render object in background
            Destroy(this.GetComponent<TilemapCollider2D>());
        }
        GenerateLevel();
    }

    private void LateUpdate()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            //Pixel is transparent, thus we ignore it
            return;
        }

        foreach (ColorToTile colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                //Creates a tile at the position
                Vector3Int position = new Vector3Int(x + startX, y + startY, zOffset);
                Tile toSet = colorMapping.tile;
                tm.SetTile(position, toSet);
            }
        }
    }

}
