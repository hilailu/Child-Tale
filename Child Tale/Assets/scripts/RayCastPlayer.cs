using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    private Camera _camera;
    public GameManager gameManager;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();         ////
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            var active = hit.transform.gameObject.GetComponent<IInteractable>();

            if (active != null)
            {
                gameManager.SetInteractableAnim(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //gameManager.SetInteractableAnim(false);

                    active.Active();
                }
            }
        }
        else
        {
            gameManager.SetInteractableAnim(false);
        }

    }
}
