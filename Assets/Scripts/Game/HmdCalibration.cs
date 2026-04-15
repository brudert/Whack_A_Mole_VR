using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

public class HmdCalibration : MonoBehaviour
{

    [System.Serializable]
    public class CalibrationEvent : UnityEvent { }
    [SerializeField]
    public CalibrationEvent calibrationUpdate;

    [SerializeField]
    private ActionBasedController controllerGameObject;

    [SerializeField]
    private float ratioSpeed = 3f;

    bool calibrated = false;

    [SerializeField]
    private CanvasGroup canvasGroupToFade; // Used to make disappear all elements linked to a canvas}

    private bool calibrationActionPressed()
    {
        return controllerGameObject.activateAction.action.ReadValue<float>() >= 0.5;
    }

    public void Update()
    {
        if (!calibrated)
        {
            if (Keyboard.current.vKey.wasPressedThisFrame || calibrationActionPressed())
            {
                CloseInstructionPanel();
            }
        }
    }

    //generic function if we need to change something when the instruction panel disappeared
    private void CloseInstructionPanel()
    {
        StartCoroutine(FadeOutCanvasGroup());
        calibrationUpdate.Invoke();
        calibrated = true;
    }

    public IEnumerator FadeOutCanvasGroup()
    {
        while (canvasGroupToFade.alpha > 0)
        {
            canvasGroupToFade.alpha -= Time.deltaTime * ratioSpeed;

            yield return null;
        }
        canvasGroupToFade.transform.gameObject.SetActive(false);
    }
}
