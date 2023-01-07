using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ContactAvatarGrid : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image contactAvatarImage;
    [SerializeField] GameObject avatarPrefub;

    [SerializeField] GameObject content;

    [SerializeField] GameObject contactGridContent;
    // Update is called once per frame
    void Start(){
        CreateGrid();
    }
    public void CreateGrid(){
        Sprite[] avatarGridList = Resources.LoadAll<Sprite>("Sprites/MassageApp/ContactAvatar/");
        foreach(Sprite s in avatarGridList){
            GameObject avatar = Instantiate(avatarPrefub, content.transform);
            avatar.transform.SetParent(content.transform);    
            avatar.GetComponent<Image>().sprite = s;

            UnityAction setActiveFalse = () =>
            {
                contactGridContent.SetActive(false);
                contactAvatarImage.sprite = s;
                //contactAvatarImage.sprite = s;
            };
            avatar.GetComponent<Button>().onClick.AddListener(setActiveFalse);
        }
    }
}
