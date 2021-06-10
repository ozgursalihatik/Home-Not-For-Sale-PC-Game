using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueMessages : ScriptableObject
{
    [TextArea(1, 10)]
    [SerializeField] public List<string> Messages;
    [SerializeField] public List<Owners> owners;
    [SerializeField] public int Index;
}
public enum Owners
{
    Ozgur = 0,
    Grandpa = 1,
    Grandma = 2
}
