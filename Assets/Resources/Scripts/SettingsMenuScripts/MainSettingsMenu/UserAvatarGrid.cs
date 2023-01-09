using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UserAvatarGrid : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject userAvatarPrefub;
    [SerializeField] Image userAvatar;
    [SerializeField] GameObject ChooseUserAvatarObject;
    void Start()
    {
        CreateUserAvatarGrid();
    }

    // Update is called once per frame
    void CreateUserAvatarGrid(){
        Sprite[] avatarGridList = Resources.LoadAll<Sprite>("Sprites/MassageApp/ContactAvatar/");
        foreach(Sprite s in avatarGridList){
            GameObject avatarGridElement = Instantiate(userAvatarPrefub, gameObject.transform);
            avatarGridElement.transform.SetParent(gameObject.transform);
            avatarGridElement.GetComponent<Image>().sprite = s;

            UnityAction setActiveFalse = () =>
            {
                ChooseUserAvatarObject.SetActive(false);
                userAvatar.sprite = s;
                //contactAvatarImage.sprite = s;
            };
            avatarGridElement.GetComponent<Button>().onClick.AddListener(setActiveFalse);
        }
    }
}
