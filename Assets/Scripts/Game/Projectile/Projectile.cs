using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Projectile
{
    void Init(GameObject _owner, Vector3 _dir, float _damage, float _speed = 10);
}
