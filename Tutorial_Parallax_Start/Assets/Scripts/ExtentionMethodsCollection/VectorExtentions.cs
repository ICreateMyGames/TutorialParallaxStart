using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtentions {
    //Vector2
    public static Vector2 With(this Vector2 v, float? x = null, float? y = null) {
        return new Vector2(x ?? v.x, y ?? v.y);
    }

    public static Vector2 AddX(this Vector2 vector, float addX) {
        return new Vector2(vector.x + addX, vector.y);
    }

    public static Vector2 AddY(this Vector2 vector, float addY) {
        return new Vector2(vector.x, vector.y + addY);
    }

    public static Vector2 TimesX(this Vector2 vector, float timesX) {
        return new Vector2(vector.x * timesX, vector.y);
    }

    public static Vector2 TimesY(this Vector2 vector, float timesY) {
        return new Vector2(vector.x, vector.y * timesY);
    }

    public static Vector2 RotateBy(this Vector2 vector, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = vector.x;
        float ty = vector.y;
        vector.x = (cos * tx) - (sin * ty);
        vector.y = (sin * tx) + (cos * ty);
        return vector;
    }

    public static Vector3 WithZ(this Vector2 v, float z) {
        return new Vector3(v.x, v.y, z);
    }


    //Vector3

    public static Vector3 With(this Vector3 v, float? x =null, float? y = null, float? z = null) {
        return new Vector3(x ?? v.x, y ?? v.y, z ?? v.z);
    }
    public static Vector3 AddX(this Vector3 vector, float addX) {
        return new Vector3(vector.x + addX, vector.y, vector.z);
    }

    public static Vector3 AddY(this Vector3 vector, float addY) {
        return new Vector3(vector.x, vector.y + addY, vector.z);
    }

    public static Vector3 AddZ(this Vector3 vector, float addZ) {
        return new Vector3(vector.x, vector.y, vector.z + addZ);
    }

    public static Vector3 TimesX(this Vector3 vector, float timesX) {
        return new Vector3(vector.x * timesX, vector.y, vector.z);
    }

    public static Vector3 TimesY(this Vector3 vector, float timesY) {
        return new Vector3(vector.x, vector.y * timesY, vector.z);
    }

    public static Vector3 TimesZ(this Vector3 vector, float timesZ) {
        return new Vector3(vector.x, vector.y, vector.z * timesZ);
    }


}
