
using UnityEngine;

namespace GUS.Core.InputSys
{
    public class Keyboard : IInputType
    {
        private bool _isHold;

        public bool BlockInput { get; private set; }

        public void SetHold(bool isHold)
        {
            _isHold = isHold;
        }

        public void Blocker(bool flag) => BlockInput = flag;

        public EnumBind Movement()
        {
            if (BlockInput) return EnumBind.None;

            if (!_isHold)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    return EnumBind.Left;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    return EnumBind.Right;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    return EnumBind.Up;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    return EnumBind.Down;
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    return EnumBind.Forward;
                }

                
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    return EnumBind.Left;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    return EnumBind.Right;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    return EnumBind.Up;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    return EnumBind.Down;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    return EnumBind.Forward;
                }
            }


            if (Input.GetMouseButton(0))
            {
                return EnumBind.Fire;
            }


            return EnumBind.None;
        }
    }
}
