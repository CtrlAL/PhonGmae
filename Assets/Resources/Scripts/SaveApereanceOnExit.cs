using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveApereanceOnExit : MonoBehaviour
{

    [SerializeField] Image MassageColor, MassageBackroundImage, UserAvatarImage;
    // Start is called before the first frame update
   public void SaveAppereance(){
     if(MassageBackroundImage.sprite == null){
          return;
     }
     Appereance.backgroundImage = new ImageData(MassageBackroundImage);

     Appereance.userAvatarImage = new ImageData(UserAvatarImage);
     Debug.Log(MassageBackroundImage.sprite.texture.format);

     Appereance.message_color = "#" + ColorUtility.ToHtmlStringRGBA(MassageColor.color);
               
     MassageSeettingsDBController.SaveAppereance();
     //MassageSeettingsDBController.ReadAppereance();
   }
}
