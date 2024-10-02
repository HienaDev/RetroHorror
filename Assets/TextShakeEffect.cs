using TMPro;
using UnityEngine;

public class TextShakeEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public float shakeMagnitude = 1.0f; // Magnitude of the shake effect
    public float shakeSpeed = 25.0f;    // Speed of the shake movement

    void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMeshPro.ForceMeshUpdate();  // Ensure mesh updates if text changes (e.g. color)
        ShakeText();
    }

    void ShakeText()
    {
        var textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            int vertexIndex = charInfo.vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            Vector3 offset = new Vector3(
                Mathf.Sin(Time.time * shakeSpeed + i) * shakeMagnitude,
                Mathf.Cos(Time.time * shakeSpeed + i) * shakeMagnitude,
                0
            );

            vertices[vertexIndex + 0] += offset;
            vertices[vertexIndex + 1] += offset;
            vertices[vertexIndex + 2] += offset;
            vertices[vertexIndex + 3] += offset;
        }

        // Apply modified vertex positions back to the mesh
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
