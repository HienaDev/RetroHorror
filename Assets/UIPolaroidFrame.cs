using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIPolaroidFrame : MonoBehaviour
{
    private GameObject bigFrameGameObject;
    [SerializeField] private GameObject tex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBigFrame(GameObject renderer) => bigFrameGameObject = renderer;

    public void OpenBigFrame()
    {
        bigFrameGameObject.SetActive(true);
        //bigFrameGameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", tex.GetComponent<Renderer>().material.GetTexture("_BaseMap"));
        bigFrameGameObject.GetComponentInChildren<PhotoPolaroid>().GetComponent<RawImage>().texture = tex.GetComponent<RawImage>().texture;
    }
}
