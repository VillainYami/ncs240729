using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Vector2 MoveInput;

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
}
