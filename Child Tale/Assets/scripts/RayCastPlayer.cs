using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [HideInInspector] public Camera _camera;
    public GameManager gameManager;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();         ////
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            var active = hit.transform.gameObject.GetComponent<IInteractable>();

            if (active != null)
            {
                gameManager.SetInteractableAnim(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    active.Active();
                }
                return;
            }


            var activeForSafe = hit.transform.gameObject.GetComponent<ISafeInteractive>();

            if (activeForSafe != null)
            {
                gameManager.SetInteractableAnim(true);

                if (Input.GetKeyDown(KeyCode.E))
                    activeForSafe.Active(_camera);
            }
        }
        else
        {
            gameManager.SetInteractableAnim(false);
        }
    }
}
