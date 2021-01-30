using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Image image;
    public Material material;
    public Transform transform;

    private float worldx, worldy;
    private int MinMapWidth, MinMapHeight;
    private float scal_x, scal_y;
    private Texture2D tex_black;
    
    // Start is called before the first frame update
    void Start()
    {
        worldx = 500;
        worldy = 500;
        MinMapWidth = (int) image.rectTransform.sizeDelta.x;
        MinMapHeight = (int) image.rectTransform.sizeDelta.y;
        scal_x = worldx / MinMapWidth;
        scal_y = worldy / MinMapHeight;
        tex_black = new Texture2D(MinMapWidth, MinMapHeight, TextureFormat.ARGB32, false);
        tex_black = clearTexture(tex_black);
        material.mainTexture = tex_black;
        image.material = material;

        
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int) transform.position.x;
        int y = (int) transform.position.y;

        //drawPoint(tex_black, x + MinMapWidth, y + MinMapHeight, 5, Color.red);

        drawPoint(tex_black, MinMapWidth, MinMapHeight, 5, Color.red);

        updateImage();
    }

    private void updateImage ()
    {
        material.mainTexture = tex_black;
        image.material = material;
    }

    private Texture2D clearTexture(Texture2D texture)
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, Color.blue);
            }
        }
        texture.Apply();
        return texture;
    }

    public void drawPoint(Texture2D tex, int x, int y, int size, Color c)
    {
        for (int n = -size; n <= size; n++)
        {
            for (int m = -size; m <= size; m++)
            {
                tex.SetPixel(x + n, y + m, c);
            }
        }
        tex.Apply();
    }
}

