using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadContactList : MonoBehaviour
{

    [SerializeField]
    GameObject ContactTmpPrefub;

    [SerializeField]
    GameObject ChatScreenObject;

    [SerializeField]

    GameObject ContactContainer, Footer;
    // Start is called before the first frame update
    void Start()
    {
        CreateContactList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateContactList(){
        List<Contact> contact_list = MassageDBControoler.GetContactList();
        foreach(Contact contact in contact_list){
            CreateListElem(contact);
        }

    }

    public void CreateListElem(Contact c){
        GameObject contact = Instantiate (ContactTmpPrefub, ContactContainer.transform);
        contact.transform.SetParent(ContactContainer.transform);
        contact.GetComponent<ContactController>().SetContact(c);
        contact.GetComponent<ContactController>().SetChatObjects(ChatScreenObject);
        contact.GetComponent<ContactController>().SetContactScreenObject(gameObject);
        contact.GetComponent<ContactController>().SetFooter(Footer);
        foreach (Transform  child in contact.transform){
            if(child.name == "ContactNameBox"){
                child.GetComponent<TextMeshProUGUI>().text = c.contact_name;
            }else if(child.name == "OnlineState"){
                child.GetComponent<TextMeshProUGUI>().text = "online";
            }if(child.name == "ContactAvatar"){

                //Texture2D texCopy = new (c.contactImage.image_width, c.contactImage.image_height, TextureFormat.DXT5, false);
                //texCopy.LoadRawTextureData(c.contactImage.avatarImageData);
                //texCopy.Apply();
                //Sprite mySprite = Sprite.Create(texCopy, new Rect(0.0f, 0.0f, c.contactImage.image_width, c.contactImage.image_height), new Vector3(0.5f, 0.5f, 100f));
                //child.GetComponent<Image>().sprite = mySprite;
            }
        }
    }
}
