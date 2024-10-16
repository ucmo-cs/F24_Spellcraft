using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{
    public Button levelSelect;
    public Button settings;
    public Button exit;
    public Button world1;
    public Button world2;
    public Button world3;
    public Button back;

    public GameObject mainCanvas;
    public GameObject worldCanvas;

    // Start is called before the first frame update
    void Start()
    {
        levelSelect.onClick.AddListener(OnButton1Click);
        settings.onClick.AddListener(OnButton2Click);
        exit.onClick.AddListener(OnButton3Click);
        world1.onClick.AddListener(OnButton4Click);
        world2.onClick.AddListener(OnButton5Click);
        world3.onClick.AddListener(OnButton6Click);
        back.onClick.AddListener(OnButton7Click);
    }

    // Function that will be triggered when button1 is clicked
    void OnButton1Click()
    {
        Debug.Log("Button 1 Clicked!");
        // Add any functionality here
        mainCanvas.SetActive(false);
        worldCanvas.SetActive(true);
    }

    // Function that will be triggered when button2 is clicked
    void OnButton2Click()
    {
        Debug.Log("Button 2 Clicked!");
        // Add any functionality here
    }

    // Function that will be triggered when button3 is clicked
    void OnButton3Click()
    {
        Debug.Log("Button 3 Clicked!");
        // Add any functionality here
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Function that will be triggered when button3 is clicked
    void OnButton4Click()
    {
        Debug.Log("Button 3 Clicked!");
        SceneManager.LoadScene("World 1");
    }

    // Function that will be triggered when button3 is clicked
    void OnButton5Click()
    {
        Debug.Log("Button 3 Clicked!");
        SceneManager.LoadScene("World 2");
    }

    // Function that will be triggered when button3 is clicked
    void OnButton6Click()
    {
        Debug.Log("Button 3 Clicked!");
        SceneManager.LoadScene("World 3");
    }

    // Function that will be triggered when button3 is clicked
    void OnButton7Click()
    {
        Debug.Log("Button 3 Clicked!");
        mainCanvas.SetActive(true);
        worldCanvas.SetActive(false);
    }
}
