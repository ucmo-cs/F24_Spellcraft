using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Public fields to reference the buttons
    public Button worldSelect;
    public Button settingsButton;
    public Button exitButton;
    public Button world1Button;
    public Button world2Button;
    public Button world3Button;
    public Button backButton;

    public GameObject MainMenu;
    public GameObject worldSelection;

    void Start()
    {
        // Assign the button click events
        worldSelect.onClick.AddListener(OnButton1Click);
        settingsButton.onClick.AddListener(OnButton2Click);
        exitButton.onClick.AddListener(OnButton3Click);
        world1Button.onClick.AddListener(OnButton4Click);
        world2Button.onClick.AddListener(OnButton5Click);
        world3Button.onClick.AddListener(OnButton6Click);
        backButton.onClick.AddListener(OnButton7Click);

        MainMenu.SetActive(true);
        worldSelection.SetActive(false);
    }

    // Function that will be triggered when button1 is clicked
    void OnButton1Click()
    {
        Debug.Log("Button 1 Clicked!");
        // Add any functionality here

        MainMenu.SetActive(false);
        worldSelection.SetActive(true);
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

        //Application.Quit();
    }

    // Function that will be triggered when button3 is clicked
    void OnButton4Click()
    {
        Debug.Log("Button 3 Clicked!");
        // Add any functionality here

        SceneManager.LoadScene("World 1");
    }

    // Function that will be triggered when button3 is clicked
    void OnButton5Click()
    {
        Debug.Log("Button 3 Clicked!");
        // Add any functionality here

        SceneManager.LoadScene("World 2");
    }

    // Function that will be triggered when button3 is clicked
    void OnButton6Click()
    {
        Debug.Log("Button 3 Clicked!");
        // Add any functionality here

        SceneManager.LoadScene("World 3");
    }

    // Function that will be triggered when button3 is clicked
    void OnButton7Click()
    {
        Debug.Log("Button 3 Clicked!");
        // Add any functionality here

        MainMenu.SetActive(true);
        worldSelection.SetActive(false);
    }

}
