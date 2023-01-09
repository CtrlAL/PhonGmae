using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBackGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadBG();
    }

    private void LoadBG(){
        MassageSeettingsDBController.ReadAppereance();
        Texture2D tex = new Texture2D(128,128);
        tex.LoadRawTextureData(Appereance.backgroundImage.imageData);
        Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        gameObject.GetComponent<Image>().sprite = mySprite;
    }
}
