using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [SerializeField] Camera _camera;

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            var active = hit.transform.gameObject.GetComponent<IInteractable>();

            if (Input.GetKeyDown(KeyCode.E) && active != null)
                active.Active();
        }
    }
}
