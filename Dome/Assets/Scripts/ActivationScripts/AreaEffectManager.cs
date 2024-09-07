using UnityEngine;

public class AreaEffectManager : MonoBehaviour
{
    private void Start()
    {
        // Disable all child objects initially
        SetChildrenActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetChildrenActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetChildrenActive(false);
        }
    }

    private void SetChildrenActive(bool isActive)
    {
        // Enable or disable all child objects of this effect area
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
