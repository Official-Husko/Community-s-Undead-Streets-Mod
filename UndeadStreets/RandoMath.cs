using System;
using System.Collections.Generic;
using GTA.Math;

namespace CWDM
{
    internal static class RandoMath
    {
        public static Random CachedRandom
        {
            get
            {
                return random ?? (random = new Random());
            }
        }

        private static Random random;

        public static Vector3 RandomDirection(bool zeroZ)
        {
            _ = Vector3.Zero;
            Vector3 theDirection;
            if (zeroZ)
            {
                theDirection = Vector3.RandomXY();
            }
            else
            {
                theDirection = Vector3.RandomXYZ();
            }

            theDirection.Normalize();
            return theDirection;
        }

        public static float RandomHeading()
        {
            return ((float)CachedRandom.NextDouble()) * 360.0f;
        }

        public static bool RandomBool()
        {
            return CachedRandom.Next(0, 2) == 0;
        }

        public static T GetRandomElementFromList<T>(List<T> theList)
        {
            if (theList == null) return default;
            return theList[CachedRandom.Next(theList.Count)];
        }

        public static T GetRandomElementFromArray<T>(T[] theArray)
        {
            if (theArray == null) return default;
            return theArray[CachedRandom.Next(theArray.Length)];
        }

        public static int Abs(int value)
        {
            return value >= 0 ? value : value * -1;
        }

        public static float Min(float x, float y)
        {
            if (x <= y) return x;
            else return y;
        }

        public static int Min(int x, int y)
        {
            if (x <= y) return x;
            else return y;
        }

        public static int Max(int x, int y)
        {
            if (x >= y) return x;
            else return y;
        }

        public static float Max(float x, float y)
        {
            if (x >= y) return x;
            else return y;
        }

        public static int TrimValue(int value, int min, int max)
        {
            value = Max(min, value);
            value = Min(max, value);

            return value;
        }

        public static float TrimValue(float value, float min, float max)
        {
            value = Max(min, value);
            value = Min(max, value);

            return value;
        }
    }
}