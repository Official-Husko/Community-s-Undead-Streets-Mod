using GTA.Math;
using System;
using System.Collections.Generic;

namespace CWDM.Extensions
{
    public static class MathExtensions
    {
        public static Random CachedRandom
        {
            get
            {
                return random ?? (random = new Random());
            }
        }

        private static Random random;

        public static Vector3 RandomDirection(this bool zeroZ)
        {
            Vector3 direction;
            if (zeroZ)
            {
                direction = Vector3.RandomXY();
            }
            else
            {
                direction = Vector3.RandomXYZ();
            }
            direction.Normalize();
            return direction;
        }

        public static float RandomHeading()
        {
            return ((float)CachedRandom.NextDouble()) * 360.0f;
        }

        public static bool RandomBool()
        {
            return CachedRandom.Next(0, 2) == 0;
        }

        public static T GetRandomElementFromList<T>(this List<T> list)
        {
            if (list == null)
            {
                return default;
            }

            return list[CachedRandom.Next(list.Count)];
        }

        public static T GetRandomElementFromArray<T>(this T[] array)
        {
            if (array == null)
            {
                return default;
            }

            return array[CachedRandom.Next(array.Length)];
        }

        public static int Abs(this int value)
        {
            if (value >= 0)
            {
                return value;
            }
            else
            {
                return value * -1;
            }
        }

        public static float Min(this float x, float y)
        {
            if (x <= y)
            {
                return x;
            }
            else
            {
                return y;
            }
        }

        public static int Min(this int x, int y)
        {
            if (x <= y)
            {
                return x;
            }
            else
            {
                return y;
            }
        }

        public static int Max(this int x, int y)
        {
            if (x >= y)
            {
                return x;
            }
            else
            {
                return y;
            }
        }

        public static float Max(this float x, float y)
        {
            if (x >= y)
            {
                return x;
            }
            else
            {
                return y;
            }
        }

        public static int TrimValue(this int value, int min, int max)
        {
            value = Max(min, value);
            value = Min(max, value);
            return value;
        }

        public static float TrimValue(this float value, float min, float max)
        {
            value = Max(min, value);
            value = Min(max, value);
            return value;
        }
    }
}