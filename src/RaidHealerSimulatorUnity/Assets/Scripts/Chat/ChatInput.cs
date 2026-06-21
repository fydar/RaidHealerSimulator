using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChatInput : MonoBehaviour
{
    public static ChatInput Instance;

    public InputField Field;
    public Button Submit;
    private bool allowEnter;
    private int lookback = -1;
    public bool IsDirty = false;

    private readonly List<string> lastInputs = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        bool send = keyboard.enterKey.isPressed || keyboard.numpadEnterKey.isPressed;
        if (allowEnter && Field.text.Length > 0 && send)
        {
            Send();
            lookback = -1;
            allowEnter = false;
        }
        else if (send || (!Field.isFocused && keyboard.tKey.isPressed))
        {
            Field.Select();
            if (keyboard.leftShiftKey.isPressed || keyboard.rightShiftKey.isPressed)
            {
                Field.text = "/";
                Field.selectionAnchorPosition = 1;
                Field.selectionFocusPosition = 1;
            }
        }
        else
        {
            allowEnter = Field.isFocused || Field.isFocused;
        }

        if (Field.isFocused)
        {
            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                if (lookback + 1 < lastInputs.Count)
                {
                    lookback++;
                    Field.text = lastInputs[lastInputs.Count - 1 - lookback];
                    Field.selectionAnchorPosition = Field.text.Length;
                    Field.selectionFocusPosition = Field.text.Length;
                }
            }
            else if (keyboard.downArrowKey.wasPressedThisFrame)
            {
                if (lookback - 1 >= 0)
                {
                    lookback--;
                    Field.text = lastInputs[lastInputs.Count - 1 - lookback];
                    Field.selectionAnchorPosition = Field.text.Length;
                    Field.selectionFocusPosition = Field.text.Length;
                }
                else
                {
                    lookback = -1;
                    Field.text = "";
                }
            }
            else if (keyboard.anyKey.wasPressedThisFrame)
            {
                lookback = -1;
            }
        }
    }

    public void Send()
    {
        string text = Field.text;
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        Chat.Instance.Log($"<color=#777>{Game.Instance.Username}:</color> {text}");

        IsDirty = true;

        lastInputs.Add(text);
        if (lastInputs.Count > 30)
        {
            lastInputs.RemoveAt(lastInputs.Count - 1);
        }

        Field.text = "";
    }
}
