using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveApereanceOnExit : MonoBehaviour
{

    [SerializeField] Image MassageColor, MassageBackroundImage;
    // Start is called before the first frame update
   public void SaveAppereance(){
        Texture2D tex = MassageBackroundImage.sprite.texture;
        Appereance.backgound_bytecode = tex.GetRawTextureData();
        Appereance.image_height = tex.height;
        Appereance.image_width = tex.width;
        Debug.Log(tex.format);

        Appereance.pivot_x = MassageBackroundImage.sprite.pivot.x;
        Appereance.pivot_y = MassageBackroundImage.sprite.pivot.y;

        Appereance.massage_color = "#" + ColorUtility.ToHtmlStringRGBA(MassageColor.color);
                
        MassageSeettingsDBController.SaveAppereance();

        //MassageSeettingsDBController.ReadAppereance();
   }
}
