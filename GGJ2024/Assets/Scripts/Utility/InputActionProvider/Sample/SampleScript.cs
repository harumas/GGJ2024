using Core.Input;
using PropertyGenerator.Generated;
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleScript : MonoBehaviour
{
    private InputEvent jumpEvent;
    private InputEvent moveEvent;

    void Start()
    {
        //入力イベントを取得
        jumpEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.Jump);
        moveEvent = InputActionProvider.Instance.CreateEvent(ActionGuid.Player.Move);

        jumpEvent.Started += OnJump;
    }

    private void Update()
    {
        Vector2 move = moveEvent.ReadValue<Vector2>();

        if (move != Vector2.zero)
        {
            Debug.Log(move);
        }
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        Debug.Log("OnJump");
    }
}