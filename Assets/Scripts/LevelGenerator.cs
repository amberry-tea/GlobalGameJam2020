using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    private float blockSize;

    public Texture2D map;
    public float startX, startY;
    public bool isBackground;
    public ColorToPrefab[] colorMappings;

    private Color dark;

    // Awake is called once, before Start
    // Swapped to Awake so that everything can be referenced in the Start method of other objects
    void Awake()
    {
        if (isBackground)
        {
            dark = new Color(1F, 1F, 1F);
        }
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
                if (pixelColor.Equals(Color.red))
                { //if the object is Smol_House_Populated
                    yOffset = 0.46f;
                }
                else if (pixelColor.Equals(Color.black))
                { //if the object is James
                    yOffset = -0.05f;
                }
                else if (pixelColor.Equals(new Color(0xc3 / (float)0xff, 0xc3 / (float)0xff, 0xc3 / (float)0xff)))
                { // if the object is Smol
                    yOffset = -0.05f;
                }

                Vector2 position = new Vector2(x * blockSize + startX, y * blockSize + startY + yOffset);
                GameObject toSet = colorMapping.prefab;
                GameObject block = Instantiate(toSet, position, Quaternion.identity, transform);

                if(pixelColor.Equals(Color.green)){
                    if(SceneManager.GetActiveScene().name == "Mountain1"){
                        block.gameObject.GetComponent<ChangeSceneTrigger>().sceneName = "Mountain2";
                    }
                    else if(SceneManager.GetActiveScene().name == "Mountain2"){
                        block.gameObject.GetComponent<ChangeSceneTrigger>().sceneName = "Boopland";
                    }
                    else if(SceneManager.GetActiveScene().name == "Robo1"){
                        block.gameObject.GetComponent<ChangeSceneTrigger>().sceneName = "Credits";
                    }
                } 
                
                //broken conversion code for Mountain2 death triggers to cloud sprites

                //else if(pixelColor.Equals(new Color(0xff /(float)0xff, (float)0x32 / 0xff,(float)0x32 / 0xff)) && SceneManager.GetActiveScene().name == "Mountain2") {
                //     print("WOA");
                //     SpriteRenderer sr;
                //     sr = block.gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
                //     sr.sprite = Resources.Load("Cloud") as Sprite;
                // }

                if (isBackground)
                {
                    //Render object in background
                    block.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
                    DestroyImmediate(block.GetComponent<BoxCollider2D>(), true);
                    block.GetComponent<SpriteRenderer>().color = dark;
                }
            }
        }
    }

}
