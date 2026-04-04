using UnityEditor;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject Gameplay;
    public GameObject Store;

    private void Start()
    {
        MainMenu.SetActive(true);
        Options.SetActive(false);
        Store.SetActive(false);
        Gameplay.SetActive(false);
    }

    public void Play()
    {
        MainMenu.SetActive(false);
        Gameplay.SetActive(true);
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
