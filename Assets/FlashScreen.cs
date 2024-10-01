using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashScreen : MonoBehaviour
{

    [SerializeField] private string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSelectedScene() => SceneManager.LoadScene(sceneName);
}
