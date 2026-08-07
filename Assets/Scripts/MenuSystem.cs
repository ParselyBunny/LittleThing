using UnityEditor;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject GameplayUI;
    public GameObject GameplayGeometry;
    public GameObject HUD;
    public GameObject Pet;

    private void Start()
    {
        MainMenu.SetActive(true);
        Options.SetActive(false);
        GameplayUI.SetActive(false);
        GameplayGeometry.SetActive(false);
        HUD.SetActive(false);
        Pet.SetActive(false);
    }

    public void Play()
    {
        MainMenu.SetActive(false);
        Options.SetActive(false);
        GameplayUI.SetActive(true);
        GameplayGeometry.SetActive(true);
        HUD.SetActive(true);
        Pet.SetActive(true);
    }

    public void ToggleOptions()
    {
        Options.SetActive(!Options.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();

        if (Application.isEditor)
        {
            EditorApplication.ExitPlaymode();
        }
    }
}
