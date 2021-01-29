using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RestartableObject
{
    Vector2 initPos { get; set; }
    Quaternion initRot { get; set; }

    void InitRestart();

    void Restart();
}
