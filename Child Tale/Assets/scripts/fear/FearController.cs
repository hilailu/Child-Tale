using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FearController : MonoBehaviour
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
        if (instance != null)
            return;
        instance = this;
    }
    private FearController() { }
    #endregion

    private void Start()
    {
        StartCoroutine(PlusFearRoutine());
        slider.value = fear;
    }

    private void Update()
    {
        if (CustomTime.hours == 17)
            LoseGame();
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
            LoseGame();
        }
        slider.value = fear;
        image.color = gradient.Evaluate(slider.normalizedValue);
    }

    void LoseGame() 
    {
        print("Lose Game");
    }
}
