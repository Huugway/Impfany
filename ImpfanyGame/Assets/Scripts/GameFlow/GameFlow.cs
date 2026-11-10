using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject hudPanel;
    public void StartGame()
    {
        HidePanel(startPanel);
        ShowPanel(hudPanel);
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
