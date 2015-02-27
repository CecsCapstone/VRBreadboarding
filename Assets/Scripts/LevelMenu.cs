using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelMenu : MonoBehaviour {
    public Button backButton;

    public void OnBackButtonGUI()
    {
        Application.LoadLevel(Application.loadedLevel - 1);
    }
}
