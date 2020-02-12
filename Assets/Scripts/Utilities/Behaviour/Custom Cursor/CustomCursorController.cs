using UnityEngine;

namespace Utilities.Behaviour.Custom_Cursor
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomCursorController : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private Texture2D customCursor;
        #pragma warning restore 649
        
        private RectTransform rectTransform;
        private Rect cursorRect;
        private Vector2 cursorPosition;
        private Vector2 cursorRectSizeDelta;
        private float width;
        private float height;

        private Vector2 mousePosition;

        private bool isActive;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            isActive = true;
        }

        private void Start()
        {
            //Cursor.visible = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                isActive = !isActive;
            }
            mousePosition = new Vector2(
                Input.mousePosition.x,
                Screen.height - Input.mousePosition.y);
            var sizeDelta = rectTransform.sizeDelta;
            width = sizeDelta.x;
            height = sizeDelta.y;
            cursorPosition = new Vector2(mousePosition.x - width * 0.5f, mousePosition.y - height * 0.5f);
            cursorRectSizeDelta = new Vector2(width, height);
            cursorRect = new Rect(cursorPosition, cursorRectSizeDelta);
        }

        private void OnGUI()
        {
            if (isActive)
            {
                GUI.DrawTexture(cursorRect, customCursor);
            }
        }
    }
}
