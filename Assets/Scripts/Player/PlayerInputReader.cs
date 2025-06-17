using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMover))]
public class PlayerInputReader : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _inputActions = new PlayerInputActions(); // Otomatik oluþturulmuþ C# sýnýfý
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Update()
    {
        Vector2 moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        _playerMover.Move(moveInput);
    }
}
