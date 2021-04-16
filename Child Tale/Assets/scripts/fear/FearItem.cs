using UnityEngine;


public class FearItem : MonoBehaviour, IPlayerInteractive
{
    [SerializeField] int fearAmount;

    public void Active(PlayerController player)
    {
        FearController.instance.addFear(fearAmount);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 1.5f, 0);
    }
}
