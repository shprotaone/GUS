using GUS.Core.InputSys;
using UnityEngine.EventSystems;

namespace GUS.Core.InputSys.Joiystick
{
    public class FloatingJoystick : Joystick, IInputType
    {
        protected override void Start()
        {
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
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
    }
}

