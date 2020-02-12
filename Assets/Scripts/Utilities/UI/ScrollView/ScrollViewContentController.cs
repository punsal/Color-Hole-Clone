using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI.ScrollView
{
    [RequireComponent(typeof(RectTransform)), RequireComponent(typeof(LayoutGroup)), ExecuteAlways]
    public class ScrollViewContentController : MonoBehaviour
    {
        private RectTransform rectTransform;
        private LayoutGroup layoutGroup;
        [SerializeField] private LayoutGroupType layoutGroupType;
        [SerializeField] private int spacing;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            layoutGroup = GetComponent<LayoutGroup>();
            layoutGroupType = layoutGroup.GetType() == typeof(VerticalLayoutGroup)
                ? LayoutGroupType.Vertical
                : LayoutGroupType.Horizontal;
        }

        private void Update()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            if (layoutGroup == null)
            {
                layoutGroup = GetComponent<LayoutGroup>();
                layoutGroupType = layoutGroup.GetType() == typeof(VerticalLayoutGroup)
                    ? LayoutGroupType.Vertical
                    : LayoutGroupType.Horizontal;
            }

            Transform containerTransform;
            var childCount = (containerTransform = transform).childCount;
            if (childCount == 0) return;

            childCount = 0;
            for (var i = 0; i < containerTransform.childCount; i++)
            {
                if (containerTransform.GetChild(i).gameObject.activeInHierarchy)
                {
                    childCount++;
                }
            }

            var firstChildRectTransform = containerTransform.GetChild(0).GetComponent<RectTransform>();
            var childSizeDelta = firstChildRectTransform.sizeDelta;

            var padding = layoutGroup.padding;
            var sizeDelta = rectTransform.sizeDelta;
            switch (layoutGroupType)
            {
                case LayoutGroupType.Vertical:
                    sizeDelta.y = childCount * (childSizeDelta.y + padding.top + padding.bottom + spacing);
                    sizeDelta.y *= 0.5f;
                    break;
                case LayoutGroupType.Horizontal:
                    sizeDelta.x = childCount * (childSizeDelta.x + padding.left + padding.right);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            rectTransform.sizeDelta = sizeDelta;
        }
    }
}