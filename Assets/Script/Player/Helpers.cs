using UnityEngine;

public static class Helpers
{
    private static readonly Matrix4x4 IsoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => IsoMatrix.MultiplyPoint3x4(input);
}
