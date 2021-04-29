using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Animator endGameAnimator;
    [SerializeField] private Text resultsText;
    [SerializeField] private PlayerController player;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private Canvas endGameCanvas;
    public static bool isGameEnd = false;
    private string hungry, clothes;

    private void Start()
    {
        endGameCanvas.enabled = false;
        GameManager.instance.OnEndGame += EndGameAnim;
        GameManager.instance.OnLoseGame += LoseGame;
        hungry = clothes = string.Empty;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnEndGame -= EndGameAnim;
        GameManager.instance.OnLoseGame -= LoseGame;
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
        bool english = LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0];
        bool clothesCollected = inventory.CheckClothes();

        if (!player.isHungry && clothesCollected) return;

        if (!english && player.isHungry)
            hungry = "Вы не поели и упали в обморок во время матча((\n";
        else if (english && player.isHungry)
            hungry = "You didn't eat and fainted during the game((\n";

        if (!english && !clothesCollected)
            clothes = "Вы не собрали всю одежду и не смогли выложиться по полной((\n";
        else if (english && !clothesCollected)
            clothes = "You didn't collect all clothes and could not do your best((\n";

        if (player.isHungry || !clothesCollected)
            if (english) resultsText.text = hungry + clothes + "You lost the game after all(((";
            else resultsText.text = hungry + clothes + "Матч вы все-таки проиграли(((";
    }

    public void LoseGame()
    {
        endGameCanvas.enabled = true;
        GameManager.isPaused = isGameEnd = true;
        endGameAnimator.SetTrigger("Lose");
    }

    public void LoadToMenu()
        => GameManager.instance.ToMenu();
}
