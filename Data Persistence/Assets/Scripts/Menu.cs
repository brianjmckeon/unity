using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField nameField;

    // Start is called before the first frame update
    void Start()
    {
        startButton.interactable = false;
        nameField.ActivateInputField();
    }

    public void OnStartButton()
    {
        GameData.Instance.Name = nameField.text;
        SceneManager.LoadScene(1);
    }

    public void OnNameField()
    {
        startButton.interactable = (nameField.text.Length == 0) ? false : true;
    }
}
