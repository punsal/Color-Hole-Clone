using UnityEngine;

namespace Utilities.Extensions {
    public static class ComponentExtension
    {
        public static bool GetComponent<T>(this Component component, out T type)
        {
            type = component.GetComponent<T>();
            return type != null;
        }
    }
}