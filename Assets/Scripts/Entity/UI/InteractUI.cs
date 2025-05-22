using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractUI : BaseUI
{
    protected override UIState UIState => UIState.Interact;

    [Header("RayCast")]
    private float checkRate = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxCheckDistance;
    private Camera cam;
    [SerializeField] private LayerMask interactLayerMask;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI interactText;

    private GameObject curInteractGameObject;
    private IInteractable curInteractable;

    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
    }

    private void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, interactLayerMask))
            {
                if(hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetInteractText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                interactText.gameObject.SetActive(false);
            }

        }
    }

    private void SetInteractText()
    {
        interactText.gameObject.SetActive(true);
        interactText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractable = null;
            interactText.gameObject.SetActive(false);
        }
    }
}
