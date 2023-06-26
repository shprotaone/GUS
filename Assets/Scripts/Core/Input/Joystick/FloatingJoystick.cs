using GUS.Core.InputSys;
using System;
using UnityEngine.EventSystems;

namespace GUS.Core.InputSys.Joiystick
{
    public class FloatingJoystick : Joystick, IInputType
    {
        public event Action OnActive;
        public bool BlockInput { get; private set; }

        protected override void Start()
        {
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            OnActive?.Invoke();
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            base.OnPointerUp(eventData);
        }

        public EnumBind Movement()
        {
            return EnumBind.None;
        }

        public void Blocker(bool flag)
        {
            BlockInput = flag;
        }
    }
}

