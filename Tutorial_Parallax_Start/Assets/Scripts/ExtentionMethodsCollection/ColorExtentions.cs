using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtentions {

    public static void SetColorAlpha(this UnityEngine.UI.Image image, float alpha) {
        image.color = image.color.With(a: alpha);
    }

    public static void SetColorAlpha(this SpriteRenderer sr, float alpha) {
        sr.color = sr.color.With(a: alpha);
    }

    public static Color With(this Color color, float? r = null, float? g = null, float? b = null, float? a = null) {
        return new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
    }

}
