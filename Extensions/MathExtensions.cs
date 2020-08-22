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
                if (random == null)
                {
                    random = new Random();
                }
                return random;
            }
        }

        private static Random random;

        public static Vector3 RandomDirection(bool zeroZ)
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

        public static T GetRandomElementFromList<T>(List<T> list)
        {
            if (list == null)
            {
                return default;
            }

            return list[CachedRandom.Next(list.Count)];
        }

        public static T GetRandomElementFromArray<T>(T[] array)
        {
            if (array == null)
            {
                return default;
            }

            return array[CachedRandom.Next(array.Length)];
        }

        public static int Abs(int value)
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

        public static float Min(float x, float y)
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

        public static int Min(int x, int y)
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

        public static int Max(int x, int y)
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

        public static float Max(float x, float y)
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
