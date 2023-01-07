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
        LoadColor();
    }

    // Update is called once per frame
    void LoadBackGround(){
        if(Appereance.backgroundImage == null){
            return;
        }
        Texture2D texCopy = new(Appereance.backgroundImage.image_width, Appereance.backgroundImage.image_height, TextureFormat.DXT1, false); //, tex.format, tex.mipmapCount > 1
        //Debug.Log(Appereance.backgound_bytecode);
        texCopy.LoadRawTextureData(Appereance.backgroundImage.imageData);
        texCopy.Apply();
        Sprite mySprite = Sprite.Create(texCopy, new Rect(0.0f, 0.0f, Appereance.backgroundImage.image_width, Appereance.backgroundImage.image_height), new Vector3(0.5f, 0.5f, 100));

        MassageBackroundImage.sprite = mySprite;
        MassageBackroundImage.pixelsPerUnitMultiplier = 1;
        MassageBackroundImage.type = Image.Type.Tiled;
    }

    void LoadColor(){
        Color message_color;
        ColorUtility.TryParseHtmlString(Appereance.message_color, out message_color);
        MassageColor.color = message_color;
        
    }
}
