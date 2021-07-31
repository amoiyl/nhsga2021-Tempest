using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager
{
    public struct Shape
    {
        public List<float> angles;
        public bool isLoop;
        public int center;

        public Shape(List<float> angles, int center, bool isLoop)
        {
            this.angles = angles;
            this.center = center;
            this.isLoop = isLoop;
        }
    }

    public static Shape Circle = new Shape(new List<float>() { 0, 36, 36, 36, 36, 36, 36, 36, 36, 36 }, 0, true);
    public static Shape Flat = new Shape(new List<float>() { 0, 0, 0, 0, 0, 0, 0 }, 3, false);
    public static Shape SlopeLine = new Shape(new List<float>() { 0, 0, 0, 45, 0, 0 }, 3, false);
    public static Shape Cshape = new Shape(new List<float>() { 0, 0, 0, 45, 45, 0, 45, 45, 0, 0, 0 }, 0, false);
    public static Shape ZigZagshape = new Shape(new List<float>() { -30, 40, -60, 50, 45, 0, -45, 0, 105, 0, -30, 15 }, 4, false);
    public static Shape Box = new Shape(new List<float>() { 0, 0, 0, 90, 0, 0, 90, 0, 0, 90, 0, 0 }, 0, true);
    public static Shape Triangle = new Shape(new List<float>() { 0, 0, 0, 0, 0, 120, 0, 0, 0, 0, 120, 0, 0, 0, 0 }, 2, false);
    public static Shape FlatBox = new Shape(new List<float>() { 0, 0, 0, 0, 0, 0, 90, 0, 90, 0, 0, 0, 0, 90, 0 }, 0, true);
}