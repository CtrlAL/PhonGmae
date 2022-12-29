using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAppereance : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image MassageColor, MassageBackroundImage;
    void Start()
    {
        MassageSeettingsDBController.ReadAppereance();
        LoadBackGround();
    }

    // Update is called once per frame
    void LoadBackGround(){
        Texture2D texCopy = new(Appereance.image_width, Appereance.image_height, TextureFormat.DXT1, false); //, tex.format, tex.mipmapCount > 1
        //Debug.Log(Appereance.backgound_bytecode);
        texCopy.LoadRawTextureData(Appereance.backgound_bytecode);
        texCopy.Apply();
        Sprite mySprite = Sprite.Create(texCopy, new Rect(0.0f, 0.0f, Appereance.image_width, Appereance.image_height), new Vector3(Appereance.pivot_x, Appereance.pivot_y, 100));

        MassageBackroundImage.sprite = mySprite;
        MassageBackroundImage.pixelsPerUnitMultiplier = 1;
        MassageBackroundImage.type = Image.Type.Tiled;
    }

    void LoadColor(){
        Color massage_color;
        ColorUtility.TryParseHtmlString(Appereance.massage_color, out massage_color);
        MassageColor.color = massage_color;
        
    }
}
