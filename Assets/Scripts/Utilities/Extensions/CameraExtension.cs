using UnityEngine;

namespace Utilities.Extensions
{
    public static class CameraExtension
    {
        public static Vector3 GetWorldPositionIn3D(this Camera camera, Vector2 vector2, float cameraZPosition = 1f)
        {
            return camera.ScreenToWorldPoint(new Vector3(vector2.x, vector2.y, cameraZPosition));
        }
    }
}