using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private float blockSize;

    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float startX, startY;

    // Start is called before the first frame update
    void Start()
    {
        blockSize = 0.32F;
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

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                float yOffset = 0;
                if (pixelColor.Equals(Color.red)) { //if the object is Smol_House_Populated
                    yOffset = 0.46f;
                } else if (pixelColor.Equals(Color.black)) {
                    yOffset = -0.05f;
                } else if (pixelColor.Equals(Color.white)) {
                    yOffset = -0.05f;
                }
                Vector2 position = new Vector2(x * blockSize + startX, y * blockSize + startY + yOffset);
                GameObject toSet = colorMapping.prefab;
                Instantiate(toSet, position, Quaternion.identity, transform);
            }
        }
    }

}
