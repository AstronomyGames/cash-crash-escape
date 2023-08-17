using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M16 : Weapon
{
    public override void Setup(CharacterType source)
    {
        base.Setup(source);
    }

    public override void Shot(Vector3 target)
    {
        Bullets[currentBullet].Launch(target);
        currentBullet++;
        if (currentBullet >= Bullets.Length)
            currentBullet = 0;
    }
}
