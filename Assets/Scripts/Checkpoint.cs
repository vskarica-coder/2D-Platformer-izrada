using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    private SpriteRenderer indicator;
    private void Awake()
    {
        indicator = GetComponent<SpriteRenderer>();
        indicator.color = inactiveColor;
    }
    public void SetCheckpointActive(bool isActive)
    {
        if (isActive)
        {
            indicator.color = activeColor;
        }
        else
        {
            indicator.color = inactiveColor;
        }
    }
}