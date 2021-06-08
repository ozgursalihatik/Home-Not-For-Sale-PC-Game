using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueMessages : ScriptableObject
{
    [TextArea(1, 5)]
    [SerializeField] public List<string> Messages;
    [SerializeField] public Owners Owner;
}
public enum Owners
{
    Grandpa = 0,
    Grandma = 1,
    test = 2,
    test1 = 3,
    test2 = 4
}
