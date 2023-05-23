using GUS.Core.InputSys;
using UnityEngine;
using System;

namespace GUS.Core.InputSys
{
    public class SmartphoneInput : IInputType
    {
        private Vector2 _swipeDirection;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private Vector2 _swipeDelta;

        private float _deadZone = 100;
        private float _magnitudeTrashold = 40;

        private bool _isTap;
        private bool _isSwiping;

        public Vector2 StartPosition => _startPosition;
        public float Direction { get; private set; }
        public float Delta { get; private set; }

        public SmartphoneInput()
        {
            ResetSwipe();
        }

        public EnumBind Firing()
        {
            if(Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    _startPosition= Input.GetTouch(0).position;
                    return EnumBind.Fire;
                }
            }          

            return EnumBind.None;
        }

        public EnumBind Movement()
        {
            TouchInput();
            CheckDirection();

            if (_swipeDirection.y > 0)
            {
                ResetSwipe();
                return EnumBind.Up;
            }
            else if (_swipeDirection.y < 0)
            {
                ResetSwipe();
                return EnumBind.Down;
            }
            else if (_swipeDirection.x == 1)
            {
                ResetSwipe();
                return EnumBind.Left;
            }
            else if (_swipeDirection.x == -1)
            {
                ResetSwipe();
                return EnumBind.Right;
            }

             
            return EnumBind.None;
        }

        private void TouchInput()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _startPosition = Input.GetTouch(0).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved && _startPosition != Vector2.zero)
                {
                    _endPosition = Input.GetTouch(0).position;
                    
                    CheckTouchOrSwipe();
                }
            }
        }

        private void CheckDirection()
        {
            if (_isSwiping)
            {
                _swipeDelta = Vector2.zero;

                if (Input.touchCount > 0)
                {
                    _swipeDelta = Input.GetTouch(0).position - _startPosition;
                }
                else
                {
                    _swipeDelta = _endPosition - _startPosition;
                }

                if (_swipeDelta.magnitude > _deadZone)
                {
                    if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    {
                        if (_swipeDelta.x > 0) _swipeDirection = Vector2.left;
                        else _swipeDirection = Vector2.right;
                    }
                    else
                    {
                        if(_swipeDelta.y > 0) _swipeDirection = Vector2.up;
                        else _swipeDirection = Vector2.down;
                    }
                }
              
                Delta = _swipeDelta.y;
                Direction = _swipeDirection.x;
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;
            _isTap = false;
            _swipeDirection = Vector2.zero;
            _startPosition = Vector3.zero;
            _endPosition = Vector3.zero;
            _swipeDelta = Vector3.zero;
        }

        private void CheckTouchOrSwipe()
        {
            float magnitude = (_startPosition - _endPosition).magnitude;

            if (magnitude < _magnitudeTrashold)
            {
                _isTap = true;
            }
            else
            {
                _isSwiping = true;
            }
        }

    }
}