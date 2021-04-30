using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Animator endGameAnimator;
    [SerializeField] private Text resultsText;
    [SerializeField] private PlayerController player;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private Canvas endGameCanvas;
    public static bool isGameEnd = false;
    private string hungry, clothes;

    private LocalizedString kindOfEnd = new LocalizedString { TableReference = "End Game" };


    private void Start()
    {
        endGameCanvas.enabled = false;
        resultsText.text = hungry = clothes = string.Empty;

        GameManager.instance.OnEndGame += EndGameAnim;
        GameManager.instance.OnLoseGame += LoseGame;
        kindOfEnd.StringChanged += UpdateString;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnEndGame -= EndGameAnim;
        GameManager.instance.OnLoseGame -= LoseGame;
        kindOfEnd.StringChanged -= UpdateString;
    }

    public void EndGameAnim()
    {
        endGameCanvas.enabled = true;
        endGameAnimator.SetBool("End Game", true);
        GameManager.isPaused = isGameEnd = true;

        if (Photon.Pun.PhotonNetwork.OfflineMode)
            ResultsLanguage();
    }

    private void ResultsLanguage()
    {
        bool clothesCollected = inventory.CheckClothes();

        if (!player.isHungry && clothesCollected) return;
        else resultsText.text = string.Empty;

        if (player.isHungry)
        {
            kindOfEnd.TableEntryReference = "hungry";
            resultsText.text += "\n";
        }

        if (!clothesCollected)
        {
            kindOfEnd.TableEntryReference = "clothes";
            resultsText.text += "\n";
        }

        if (player.isHungry || !clothesCollected)
            kindOfEnd.TableEntryReference = "match";
    }

    void UpdateString(string translatedValue)
        => resultsText.text += translatedValue;

    public void LoseGame()
    {
        endGameCanvas.enabled = true;
        GameManager.isPaused = isGameEnd = true;
        endGameAnimator.SetTrigger("Lose");
    }

    public void LoadToMenu()
        => GameManager.instance.ToMenu();
}
