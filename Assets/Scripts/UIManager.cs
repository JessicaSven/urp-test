using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;

    [SerializeField] private Button _menuButton;

    // Start is called before the first frame update
    void Start()
    {
        _menuButton.onClick.AddListener(LoadMenuScene);
    }

    private void LoadMenuScene()
    {
        _sceneLoader.LoadScene("Menu");
    }

}
