using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TPFinal - Tadeo Elli.
public struct EnemyFov
{
    public float speed, attackDistance, timeToPatrol, _viewRadius, _viewAngle;
    public EnemyFov(float speed, float attackDistance, float timeToPatrol, float _viewRadius, float _viewAngle)
    {
        this.speed = speed;
        this.attackDistance = attackDistance;
        this.timeToPatrol = timeToPatrol;
        this._viewRadius = _viewRadius;
        this._viewAngle = _viewAngle;
    }
} 
