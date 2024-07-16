using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeapon", menuName = "Scriptable Object/Weapon")]
public class PlayerWeapon : ScriptableObject
{
    public int[] Damage = new int[5];
    public int[] AttackSpeed = new int[5];

}
