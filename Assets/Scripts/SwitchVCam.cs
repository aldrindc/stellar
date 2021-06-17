using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

/// <summary>
/// Switch between a 3rd person vcam and a 3rd person aiming vcam once the player has started or stopped aiming.
/// </summary>
public class SwitchVCam : MonoBehaviour
{
    [SerializeField, Tooltip("Player controls, used to get the Aim control.")]
    private PlayerInput playerInput;
    [SerializeField, Tooltip("How much should the priority of each vcam change once aiming has started or stopped. Priority determines which vcam is shown.")]
    private int priorityBoostAmount = 10;
    [SerializeField, Tooltip("Canvas containing aim reticle for the third person mode.")]
    private Canvas thirdPersonCanvas;
    [SerializeField, Tooltip("Canvas containing aim reticle for the third person aim mode.")]
    private Canvas aimCanvas;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;

    private void Awake() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable() {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable() {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    /// <summary>
    /// Called when the player has started aiming. This enables the aim vcam and canvas with the aim reticle.
    /// </summary>
    private void StartAim() {
        virtualCamera.Priority += priorityBoostAmount;
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;
    }

    /// <summary>
    /// Called when the player has stopped aiming. This enables the normal third person vcam and canvas with the hip fire reticle.
    /// </summary>
    private void CancelAim() {
        virtualCamera.Priority -= priorityBoostAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
    }
}
