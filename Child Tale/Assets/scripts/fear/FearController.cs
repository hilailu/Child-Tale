using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FearController : MonoBehaviour, ISaveable
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image image;
    public float plusFearCD = 1f;
    private int fear = 0;

    #region Singleton
    public static FearController instance;
    private void Awake()
    {
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
        if (instance != null)
            return;
        instance = this;
    }
    private FearController() { }
    #endregion

    void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }

    private void Start()
    {
        StartCoroutine(PlusFearRoutine());
        slider.value = fear;

        GameManager.instance.OnEndGame += HideBare;
    }

    private IEnumerator PlusFearRoutine()
    {
        yield return new WaitForSeconds(plusFearCD);
        fear++;
        UpdateValue();
        StartCoroutine(PlusFearRoutine());
    }

    public void addFear(int amount)
    {
        fear += amount;
        UpdateValue();
    }

    private void UpdateValue()
    {
        if (fear < 0) fear = 0;
        if (fear > 99)
        {
            fear = 100;
            EndGame();
        }
        slider.value = fear;
        image.color = gradient.Evaluate(slider.normalizedValue);
    }

    void EndGame() 
    {
        print("Lose Game");
    }

    private void HideBare()
        => slider.gameObject.SetActive(false);

    public void Save()
    {
        PlayerData.instance.fear = this.fear;
    }

    public void Load()
    {
        this.fear = PlayerData.instance.fear;
        UpdateValue();
    }
}
