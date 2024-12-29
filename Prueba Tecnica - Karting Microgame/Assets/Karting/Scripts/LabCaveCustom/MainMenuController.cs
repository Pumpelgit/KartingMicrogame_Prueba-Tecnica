using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject controlsGo;
    [SerializeField]
    private GameObject kart;

    public void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ToggleControls()
    {
        bool isShowing = controlsGo.activeInHierarchy;
        controlsGo.SetActive(!isShowing);

        if (kart != null)
            kart.SetActive(isShowing);
    }
}
