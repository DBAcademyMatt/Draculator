using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element Collection", menuName = "SandSim/Create Element Collection")]
public class ElementCollection : ScriptableObject
{
    public List<BaseElement> Elements = new List<BaseElement>();
}
