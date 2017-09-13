using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject loadingScreen;

    public Image fill;

    public Text text;

    public Button loadLevelButton;

    private float _progress = 0f;

    private bool _isClicked = false;

    public void OnLoadLevelClick(int sceneIndex)
    {
        loadingScreen.SetActive(true);

        loadLevelButton.interactable = false;

        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;

        while (_progress < 1f)
        {
            _progress = Mathf.Clamp01(operation.progress / 0.9f);

            fill.fillAmount = _progress;

            text.text = "Loading... " + (int)(_progress * 100f) + "%"; 

            yield return null;
        }

        text.text = "Click anywhere to start.";

        while (!_isClicked)
        {
            yield return null;
        }

        operation.allowSceneActivation = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _progress == 1f)
        {
            _isClicked = true;
        }
    }
}