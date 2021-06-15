using System;
using System.Collections;
using UnityEngine;

namespace Bee
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected RectTransform _mainPanel;
        protected float _delay = 0.001f;
        protected bool _lockState;

        protected virtual void ResetRectPosition()
        {            
            _mainPanel.offsetMin = new Vector2(0, 0);
            _mainPanel.offsetMax = new Vector2(0, 0);
            _mainPanel.offsetMax = new Vector2(0, 0);
            _mainPanel.offsetMin = new Vector2(0, 0);
        }

        protected void SetLeft(RectTransform rectTransform, float left)
        {
            rectTransform.offsetMin = new Vector2(left, rectTransform.offsetMin.y);            
        }

        protected void SetRight(RectTransform rectTransform, float right)
        {
            rectTransform.offsetMax = new Vector2(-right, rectTransform.offsetMax.y);
        }

        protected void SetTop(RectTransform rectTransform, float top)
        {
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -top);
        }

        protected void SetBottom(RectTransform rectTransform, float bottom)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottom);
        }        

        protected void SetAnchoredPosition(RectTransform rectTransform, float posX, float posY)
        {
            rectTransform.anchoredPosition = new Vector2(posX, posY);
        }

        protected virtual void Show(Action callBack = null)
        {
            StartCoroutine(ShowOrHide(callBack));
        }

        protected virtual void Hide(bool instaHide = false, Action callBack = null)
        {
            if (instaHide)
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;
                callBack?.Invoke();
                return;
            }

            StartCoroutine(ShowOrHide(callBack));
        }

        private IEnumerator ShowOrHide(Action callBack)
        {
            float counter = 1f;            
            float stepValue = 0.05f;
           
            if (_canvasGroup.alpha < 1)
            {
                stepValue = Mathf.Abs(stepValue);
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.interactable = true;
            }
            else
            {
                stepValue *= -1;
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;
            }

            while (counter > 0)
            {
                _canvasGroup.alpha += stepValue;
                counter -= Mathf.Abs(stepValue);
                yield return new WaitForSeconds(_delay);
            }
 
            callBack?.Invoke();
        }

        protected virtual IEnumerator Slide(RectTransform _panel, SlideDirectionName directionName, float step, float counter, Action callBack = null)
        {
            if (_lockState)
            {
                yield break;
            }

            while (counter > 0)
            {
                _lockState = true;
                counter -= Mathf.Abs(step);
                float nextStep = 0;
                switch (directionName)
                {
                    case SlideDirectionName.Vertical:
                        nextStep = _panel.anchoredPosition.y + step;
                        SetAnchoredPosition(_panel, _panel.anchoredPosition.x, nextStep);
                        break;
                    case SlideDirectionName.Horizontal:
                        nextStep = _panel.anchoredPosition.x + step;
                        SetAnchoredPosition(_panel, nextStep, _panel.anchoredPosition.y);
                        break;
                    default:
                        break;
                }
                
                yield return new WaitForSeconds(0.0001f);
            }

            _lockState = false;
            callBack?.Invoke();
        }
    }
}