using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    private Character character;

    private void Start()
    {
        character = GetComponent<Character>();


    }

    private void OnTakeDamage(Character dealer, int damage)
    {
    }

    private void Update()
    {
        character.SelectedTarget = Character.PlayerSelected;

        bool scrollRight = false;
        bool scrollLeft = false;
        var keyboard = Keyboard.current;
        if (keyboard.tabKey.wasPressedThisFrame)
        {
            if (keyboard.leftShiftKey.isPressed)
            {
                scrollLeft = true;
            }
            else
            {
                scrollRight = true;
            }
        }

        if (scrollRight)
        {
            int currentIndex = character.Party.Members.IndexOf(character.SelectedTarget);
            if (currentIndex != -1)
            {
                currentIndex++;

                if (currentIndex >= character.Party.Members.Count)
                {
                    currentIndex = 0;
                }
                character.SelectedTarget = character.Party.Members[currentIndex];
            }
        }

        if (scrollLeft)
        {
            int currentIndex = character.Party.Members.IndexOf(character.SelectedTarget);
            if (currentIndex != -1)
            {
                currentIndex--;

                if (currentIndex < 0)
                {
                    currentIndex = character.Party.Members.Count - 1;
                }
                character.SelectedTarget = character.Party.Members[currentIndex];
            }
        }
        Character.PlayerSelected = character.SelectedTarget;
    }
}
