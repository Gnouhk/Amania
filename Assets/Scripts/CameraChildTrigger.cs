using UnityEngine;

public class CameraChildTrigger : MonoBehaviour
{
    private CamerePivot parentScript;

    private void Start()
    {
        parentScript = GetComponentInParent<CamerePivot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parentScript.OnChildTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentScript.OnChildTriggerExit(other);
    }
}