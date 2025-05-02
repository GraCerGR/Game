using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatAmplitude = 0.05f;
    public float floatFrequency = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        // --------- Качание --------- 
        float newY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}