using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject GridGroup;

    [SerializeField]
    private GameObject Screen;
    // Start is called before the first frame update

    void Start(){
        AppStorage.loadAppList();
        CreateAppGrid();
    }
    private void CreateApp(GameObject appObject, GameObject appIconPrefub){
        //Debug.Log("Hola");
        GameObject appic = Instantiate (appIconPrefub, GridGroup.transform);
        appic.transform.SetParent(GridGroup.transform);
        appic.GetComponent<App>().SetAppIcon(appic);

        GameObject app_window = Instantiate (appObject, Screen.transform);
        appic.GetComponent<App>().SetApp(app_window);

        app_window.SetActive(false);
        app_window.transform.SetParent(Screen.transform);
    }
    private void CreateAppGrid(){
        foreach (GameObject app_obj in AppStorage.appList){ 
            foreach (GameObject app_ico in AppStorage.appIconList){
                if(app_obj.name == app_ico.name + "Window"){
                    CreateApp(app_obj, app_ico);
                }else{
                    //Debug.Log("Proebalsya prefub");
                }
            }  
        }
    }
}   
