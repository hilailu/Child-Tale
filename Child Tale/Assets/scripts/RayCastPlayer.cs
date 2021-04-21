using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [HideInInspector] public Camera _camera;
    private PlayerController player;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        player = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f) && !GameManager.isPaused)
        {
            var active = hit.transform.gameObject.GetComponent<IInteractable>();

            if (active != null)
            {
                GameManager.instance.SetInteractableAnim(true);

                if (Input.GetKeyDown(KeyCode.E))
                    active.Active();

                return;
            }


            var activeForSafe = hit.transform.gameObject.GetComponent<IPlayerInteractive>();

            if (activeForSafe != null)
            {
                GameManager.instance.SetInteractableAnim(true);

                if (Input.GetKeyDown(KeyCode.E))
                    activeForSafe.Active(player);
            }
        }
        else
        {
            GameManager.instance.SetInteractableAnim(false);
        }
    }
}
