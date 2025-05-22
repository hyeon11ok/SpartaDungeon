using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractUI : BaseUI
{
    protected override UIState UIState => UIState.Interact;

    [Header("RayCast")]
    private float checkRate = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxCheckDistance;
    private Camera cam;
    private GameObject curInteractGameObject;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI interactText;

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

            if(Physics.Raycast(ray, out hit, maxCheckDistance))
            {
                if(hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    SetInteractText();
                }
            }
            else
            {
                curInteractGameObject = null;
                interactText.gameObject.SetActive(false);
            }

        }
    }

    private void SetInteractText()
    {
        interactText.gameObject.SetActive(true);
        interactText.text = curInteractGameObject.transform.name;
    }
}
