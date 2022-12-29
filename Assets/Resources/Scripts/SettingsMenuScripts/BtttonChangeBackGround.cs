using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class BtttonChangeBackGround : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject BackGroundChatObject;

    public void ChangeBackGround(){
        Image new_image = gameObject.GetComponent<Image>();
        BackGroundChatObject.GetComponent<Image>().sprite = new_image.sprite;
        Debug.Log(new_image.sprite.name);
        BackGroundChatObject.GetComponent<Image>().type = new_image.type;
        BackGroundChatObject.GetComponent<Image>().pixelsPerUnitMultiplier = new_image.pixelsPerUnitMultiplier;

        //byte[] data  = BackGroundChatObject.GetComponent<Image>().sprite.texture.GetRawTextureData();
        //BackGroundChatObject.GetComponent<Image>().LoadImageIntoTexture
        //string filepath = AssetDatabase.GetAssetPath(BackGroundChatObject.GetComponent<Image>().sprite);
        //MassageSeettingsDBController.SaveBackGroundImage(data);
    }

    public void SetBackGoundObject(GameObject bco){
        BackGroundChatObject = bco;
    }

    public void SetImage(Sprite chat_background){
            gameObject.GetComponent<Image>().sprite = chat_background;
            gameObject.GetComponent<Image>().type = Image.Type.Tiled;
            BackGroundChatObject.GetComponent<Image>().pixelsPerUnitMultiplier = 2;
    }
}
