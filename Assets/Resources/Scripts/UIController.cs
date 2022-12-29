using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


public class UIController : EditorWindow 
{ 
    VisualElement container;
    [MenuItem("test/test_widndow")]

    public static void Show_Window(){
        UIController window = GetWindow<UIController>(); 
        window.titleContent = new GUIContent("test_window");
    }

    public void CreateGUI(){
        container = rootVisualElement; 
        VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UIDoc/MassageTestFile.uxml");
        container.Add(visualTreeAsset.Instantiate());

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UIDoc/UIStyles/massageStyles.uss");
        container.styleSheets.Add(styleSheet);
    }

}
