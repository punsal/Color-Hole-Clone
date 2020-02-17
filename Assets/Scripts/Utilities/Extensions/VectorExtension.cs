﻿using UnityEngine;

namespace Utilities.Extensions
{
    public static class VectorExtension
    {
        public static Vector2 ToVector2(this Vector3 vector3, bool isFlatten = false)
        {
            switch (isFlatten)
            {
                case true:
                    return new Vector2(vector3.x, vector3.y);
                default:
                    return new Vector2(vector3.x, vector3.z);
            }
        }

        public static Vector3 ToVector3(this Vector2 vector2, bool isFlatten = false)
        {
            switch (isFlatten)
            {
                case true:
                    return new Vector3(vector2.x, 0f, vector2.y);
                default:
                    return new Vector3(vector2.x, vector2.y);
            }
        }

        public static Vector3 SetX(this Vector3 vector3, float? x)
        {
            return new Vector3(x ?? vector3.x, vector3.y, vector3.z);
        }

        public static Vector3 SetY(this Vector3 vector3, float? y)
        {
            return new Vector3(vector3.x, y ?? vector3.y, vector3.z);
        }
    }
}