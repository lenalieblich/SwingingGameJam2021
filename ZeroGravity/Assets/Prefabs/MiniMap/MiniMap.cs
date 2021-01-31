using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Image image;
    public Material material;
    public Image imageObjs;
    public Material materialObjs;
    public Transform transform;
    public Transform mark1;
    public Transform mark2;
    public Transform mark3;
    public Transform markBase;
    public string TagObjs;
    public int visual_range = 25;

    public Transform test;

    private float worldx, worldy;
    private int MinMapWidth, MinMapHeight;
    private float scal_x, scal_y;
    private Texture2D tex_black, tex_objs;
    private GameObject[] objs;

    // Start is called before the first frame update
    void Start()
    {
        worldx = Vector2.Distance(mark1.position, mark2.position);
        worldy = Vector2.Distance(mark2.position, mark3.position);
        MinMapWidth = (int)image.rectTransform.sizeDelta.x;
        MinMapHeight = (int)image.rectTransform.sizeDelta.y;
        scal_x = MinMapWidth / worldx;
        scal_y = MinMapHeight / worldy;
        tex_black = new Texture2D(MinMapWidth, MinMapHeight, TextureFormat.ARGB32, false);
        tex_black = clearTexture(tex_black, Color.white);
        material.mainTexture = tex_black;
        image.material = material;
        ini_objTex();
    }

    private void ini_objTex()
    {
        objs = GameObject.FindGameObjectsWithTag(TagObjs);
        tex_objs = new Texture2D(MinMapWidth, MinMapHeight, TextureFormat.ARGB32, false);
        tex_objs = clearTexture(tex_objs, Color.clear);
        materialObjs.mainTexture = tex_objs;
        image.material = materialObjs;

        //analysePositionObjs();
        scanForObjs();
    }

    private void scanForObjs ()//scan nach Collidern
    {
        for (int x = 0; x < Vector2.Distance(mark1.position, mark2.position); x++)
        {
            for (int y = 0; y < Vector2.Distance(mark2.position, mark3.position); y++)
            {
                Vector2 pos = new Vector2(mark1.position.x + x, mark1.position.y - y);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, 1.5f);

                if (hit.point != null)
                {
                    float tmpx = ((hit.point.x - markBase.position.x) * scal_x) + (MinMapWidth / 2);
                    float tmpy = ((hit.point.y - markBase.position.y) * scal_y) + (MinMapHeight / 2);
                    drawPoint(tex_objs, (int) tmpx, (int) tmpy, 1, Color.red);
                }
            }
        }

        material.mainTexture = tex_objs;
        image.material = material;
    }

    private void analysePositionObjs ()//Auslesen anhand der Position
    {
        foreach (GameObject g in objs)
        {
            float x = ((g.transform.position.x - markBase.position.x) * scal_x) + (MinMapWidth / 2);
            float y = ((g.transform.position.y - markBase.position.y) * scal_y) + (MinMapHeight / 2);
            if ((x >= 0 && x <= MinMapWidth) && (y >= 0 && y <= MinMapHeight))
            {
                drawPoint(tex_objs, (int)x, (int)y, 5, Color.red);
            }
        }
        material.mainTexture = tex_objs;
        image.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        updateImage();
    }

    private void updateImage ()
    {
        float x = ((transform.position.x - markBase.position.x) * scal_x) + (MinMapWidth / 2);
        float y = ((transform.position.y - markBase.position.y) * scal_y) + (MinMapHeight / 2);
        if ((x>=0 && x <= MinMapWidth) && (y>=0 && y<=MinMapHeight))
        {
            drawPoint(tex_black, (int)x, (int)y, visual_range, Color.clear);
        }

        material.mainTexture = tex_black;
        image.material = material;
    }

    private Texture2D clearTexture(Texture2D texture, Color c)
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, c);
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

