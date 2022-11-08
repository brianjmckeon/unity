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
        // The start button will only become clickable once the user enters a name
        startButton.interactable = false;
        nameField.ActivateInputField();
    }

    public void OnStartButton()
    {
        GameData.Instance.Name = nameField.text;
        SceneManager.LoadScene(1);
    }

    // Called whenever the name text field's value changes
    public void OnNameField()
    {
        // If the name field isn't empty, allow the start button to be clicked
        startButton.interactable = (nameField.text.Length == 0) ? false : true;
    }
}
