using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Core.InputSys
{
    /// <summary>
    /// �������� �� ��� �����
    /// </summary>
    public interface IInputType
    {
        event Action<EnumBind> OnMove;
        bool BlockInput { get; }
        void Blocker(bool flag);
        EnumBind Movement();
    }
}

