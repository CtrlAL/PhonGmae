using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAppereance : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image MassageColor, MassageBackroundImage, UserAvatarImage;
    void Start()
    {
        MassageSeettingsDBController.ReadAppereance();
        LoadBackGround();
        LoadColor();
        //LoadUserAvatar();
    }

    // Update is called once per frame
    void LoadBackGround(){
        if(Appereance.backgroundImage == null){
            return;
        }
        MassageBackroundImage.sprite = Appereance.backgroundImage.SpriteFromImageByte();
        MassageBackroundImage.pixelsPerUnitMultiplier = 1;
        MassageBackroundImage.type = Image.Type.Tiled;
    }

    void LoadUserAvatar(){
        if(Appereance.userAvatarImage == null){
            return;
        }
        Sprite mySprite = Appereance.userAvatarImage.SpriteFromImageByte();
        MassageBackroundImage.sprite = mySprite;
    }

    void LoadColor(){
        Color message_color;
        ColorUtility.TryParseHtmlString(Appereance.message_color, out message_color);
        MassageColor.color = message_color;
        
    }
}
