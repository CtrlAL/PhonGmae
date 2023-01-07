using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveApereanceOnExit : MonoBehaviour
{

    [SerializeField] Image MassageColor, MassageBackroundImage;
    // Start is called before the first frame update
   public void SaveAppereance(){
     if(MassageBackroundImage.sprite == null){
          return;
     }
     Texture2D tex = MassageBackroundImage.sprite.texture;
     Appereance.backgroundImage = new ImageData(tex.GetRawTextureData(), tex.height, tex.width);
     Debug.Log(tex.format);


     Appereance.message_color = "#" + ColorUtility.ToHtmlStringRGBA(MassageColor.color);
               
     //MassageSeettingsDBController.SaveAppereance();
     //MassageSeettingsDBController.UpdateAvatar();
     MassageSeettingsDBController.CreateAppereancePresetRow();
     MassageSeettingsDBController.UpdateBackground();
     MassageSeettingsDBController.UpdateMessageColor();

        //MassageSeettingsDBController.ReadAppereance();
   }
}
