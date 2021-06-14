using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueMessages : ScriptableObject
{
    [TextArea(1, 10)]
    [SerializeField] public List<string> Messages;
    [SerializeField] public List<Members> owners;
    [SerializeField] public int Index;
}
public enum Members
{
    Ozgur = 0,
    Dede = 1,
    Babaanne = 2
}
