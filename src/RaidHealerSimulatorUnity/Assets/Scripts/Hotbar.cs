using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hotbar : MonoBehaviour
{
    private static readonly Dictionary<int, Key> mappings = new()
    {
        [0] = Key.Digit1,
        [1] = Key.Digit2,
        [2] = Key.Digit3,
        [3] = Key.Digit4,
        [4] = Key.Digit5,
        [5] = Key.Digit6,
        [6] = Key.Digit7,
        [7] = Key.Digit8,
        [8] = Key.Digit9,
        [9] = Key.Minus,
        [10] = Key.Equals,
    };

    [SerializeField] private Character character;

    [Space]
    [SerializeField] private RectTransform holder;
    [SerializeField] private UIPool<HotbarSlot> slotPool;

    private void Start()
    {
        slotPool.Flush();
        int index = 0;
        foreach (var ability in character.Abilities)
        {
            var slotRenderer = slotPool.Grab(holder);

            if (!mappings.TryGetValue(index, out var mapping))
            {
                mapping = Key.None;
            }

            slotRenderer.Setup((Ability)ability, mapping);
            index++;
        }
    }
}
