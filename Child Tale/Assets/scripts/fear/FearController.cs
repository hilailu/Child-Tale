using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearController : MonoBehaviour
{
    [SerializeField] Slider slider;
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
    #endregion

    private void Start()
    {
        StartCoroutine(PlusFearRoutine());
    }

    private void Update()
    {
        print(fear);
    }

    private IEnumerator PlusFearRoutine()
    {
        yield return new WaitForSeconds(plusFearCD);
        fear++;
        StartCoroutine(PlusFearRoutine());
    }

    public void addFear(int amount)
    {
        fear += amount;
        if (fear < 0) fear = 0;
        if (fear > 99)
        {
            fear = 100;
            LoseGame();
        }
    }

    void LoseGame() { }
}
