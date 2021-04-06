using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _interact;

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            var active = hit.transform.gameObject.GetComponent<IInteractable>();

            if (active != null)
            {
                _interact.SetBool("InteractOpen", true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interact.SetBool("InteractOpen", false);
                    active.Active();
                }
            }
        }
        else _interact.SetBool("InteractOpen", false);
    }
}
