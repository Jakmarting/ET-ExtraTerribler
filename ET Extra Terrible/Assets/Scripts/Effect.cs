using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // An effect defines something that will be inflicted e.g.
    // Acid (stacks - true), (maxStacks - 9), (dps - 1), (changeInSpeed - (-0.3f)), (changeInAttack (0.0f)) ,(ignoreArmour - false), (ignoreShields - false), (lossInArmour - 2.0f), (shieldDamage - 0.1f)

    public string effectName;
    public bool stacks;
    public int maxStack;

    public float dps;
    public float changeInSpeed;
    public float changeInAttack;

    public bool ignoreArmour;
    public bool ignoreShields;

    public float lossInArmour;
    public float shieldDamage;
}
