using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public Button startButton;

    public void OnStartButtonGUI()
    {
        Application.LoadLevel(1);
    }
}
