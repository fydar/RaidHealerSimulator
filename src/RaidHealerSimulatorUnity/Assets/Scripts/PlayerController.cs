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

        var members = character.Party.Members;
        if (scrollRight && members.Count > 0)
        {
            int currentIndex = members.IndexOf(character.SelectedTarget);
            character.SelectedTarget = currentIndex == -1
                ? members[0]
                : members[(currentIndex + 1) % members.Count];
        }

        if (scrollLeft && members.Count > 0)
        {
            int currentIndex = members.IndexOf(character.SelectedTarget);
            character.SelectedTarget = currentIndex == -1
                ? members[members.Count - 1]
                : members[(currentIndex - 1 + members.Count) % members.Count];
        }
        Character.PlayerSelected = character.SelectedTarget;
    }
}
