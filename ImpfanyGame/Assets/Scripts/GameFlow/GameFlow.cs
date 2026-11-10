using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    public void StartGame()
    {
        HidePanel(startPanel);
    }

    private void HidePanel (GameObject panel)
    {
        panel.SetActive(false);
    }

    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
}
