using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
   private GameObject _app;

   private GameObject _appIcon;

   private string appName;

   public App(GameObject app, GameObject appIcon){
        _appIcon = appIcon;
        _app = app;
   }

   public void SetApp(GameObject app){
      _app = app;
   }

   public void SetAppIcon(GameObject appIcon){
      _appIcon = appIcon;
   }
   public void openApp(){
      Debug.Log(_app.activeSelf);
      _app.SetActive(true);
   }
}
