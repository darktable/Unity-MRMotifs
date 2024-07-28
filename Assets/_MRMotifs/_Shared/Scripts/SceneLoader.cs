using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Tooltip("List of toggles that contain the covers of the scenes.")]
    [SerializeField] private List<Toggle> sceneToggles;

    [Tooltip("List of scene names corresponding to the toggles.")]
    [SerializeField] private List<string> sceneNames;

    [Tooltip("List of control bars for individual scenes.")]
    [SerializeField] private List<GameObject> sceneControlBars;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        RegisterToggleListeners();
        DeactivateAllControlBars();
    }

    private void RegisterToggleListeners()
    {
        foreach (var toggle in sceneToggles)
        {
            toggle.onValueChanged.AddListener(delegate { ToggleSceneLoad(toggle); });
        }
    }

    private void ToggleSceneLoad(Toggle toggle)
    {
        var index = sceneToggles.IndexOf(toggle);
        if (toggle.isOn && index != -1)
        {
            StartCoroutine(LoadSceneAsync(sceneNames[index], index));
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName, int sceneIndex)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (asyncLoad is { isDone: false })
        {
            yield return null;
        }
        EnableControlBar(sceneIndex);
    }

    private void EnableControlBar(int sceneIndex)
    {
        DeactivateAllControlBars();
        if (sceneIndex >= 0 && sceneIndex < sceneControlBars.Count)
        {
            sceneControlBars[sceneIndex].SetActive(true);
        }
    }

    private void DeactivateAllControlBars()
    {
        foreach (var controlBar in sceneControlBars)
        {
            controlBar.SetActive(false);
        }
    }
}
