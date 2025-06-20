using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishCheckpoint : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        if (levelCompletedPanel != null)
            levelCompletedPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelCompletedPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
