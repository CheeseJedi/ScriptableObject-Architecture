using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public static class UnityColorExtensions
    {
        public static float[] ToFloatArray(this Color color)
        {
            float[] arrayRepresentation = new float[4];
            arrayRepresentation[0] = color.r;
            arrayRepresentation[1] = color.g;
            arrayRepresentation[2] = color.b;
            arrayRepresentation[3] = color.a;
            return arrayRepresentation;
        }

        public static void FromFloatArray(this ref Color color, float[] arrayRepresentation)
        {
            color.r = arrayRepresentation[0];
            color.g = arrayRepresentation[1];
            color.b = arrayRepresentation[2];
            color.a = arrayRepresentation[3];
        }
    }
}