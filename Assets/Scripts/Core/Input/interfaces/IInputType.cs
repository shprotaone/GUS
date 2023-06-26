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
        bool BlockInput { get; }
        void Blocker(bool flag);
        EnumBind Movement();
    }
}

