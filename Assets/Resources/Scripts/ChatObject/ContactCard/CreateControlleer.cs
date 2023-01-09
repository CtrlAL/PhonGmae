using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateControlleer : MonoBehaviour
{
    // Start is called before the first frame updat
    [SerializeField]
    TMP_InputField NameInput;
    [SerializeField]
    LoadContactList ContactLisstLoader;
    [SerializeField] NotListControllr notListController;
    [SerializeField] Image Avatar;

    // Update is called once per frame

    void Start(){
        Avatar.sprite = Resources.LoadAll<Sprite>("Sprites/MassageApp/ContactAvatar/")[0];
    }
    public void CreateNewContact(){
        if(NameInput.text==""){
            return;
        }
        Contact c = new Contact(NameInput.text);

        Texture2D tex = Avatar.sprite.texture;
        Debug.Log(tex.format);
        ImageData contactAvatarImage = new ImageData(tex.GetRawTextureData(), tex.width, tex.height);
        c.contactImage = contactAvatarImage;


        MassageDBControoler.CreateContact(c);
        ContactLisstLoader.CreateListElem(c);
        notListController.AddToNotList(c);
    }
}
