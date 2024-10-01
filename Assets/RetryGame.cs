using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    [SerializeField] private KeyCode retryKey = KeyCode.R;
    [SerializeField] private string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(retryKey)) { LoadSelectedScene(); }
    }

    public void LoadSelectedScene() => SceneManager.LoadScene(sceneName);
}
