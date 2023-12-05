using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _crossfadeImage;
    [SerializeField] private float _transitionTime = 1f;

    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        _crossfadeImage.gameObject.SetActive(true);
        StartCoroutine(Crossfade(1, 0));
    }

    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(Crossfade(0, 1, levelIndex));
    }

    private IEnumerator Crossfade(float from, float to, int levelIndex = -1)
    {
        float elapsedTime = 0;

        while (elapsedTime < _transitionTime)
        {
            elapsedTime += Time.deltaTime;
            _crossfadeImage.alpha = Mathf.Lerp(from, to, elapsedTime / _transitionTime);

            yield return null;
        }

        _crossfadeImage.alpha = to;

        if (levelIndex < 0)
            yield break;

        SceneManager.LoadScene(levelIndex);
    }
}
