using System;
using Microsoft.Xna.Framework;

namespace TheBackrooms.Core.Utilities
{
    public static class Easings
    {
        public static double EaseInOutCirc(double value)
        {
            if ((value *= 2) < 1)
            {
                double sqrt = Math.Sqrt(1 - value * value);
                double halfSqrt = 0.5D * sqrt;
                return 0.5D - halfSqrt;
            }

            double inSqrt = 1 - (value -= 2D) * value;
            double outSqrt = Math.Sqrt(inSqrt);
            double halfOutSqrt = 0.5D * outSqrt;
            return halfOutSqrt + 0.5D;
        }

        public static double EaseInQuint(double value) => value * value * value * value * value;

        public static float ClampLerp(float value, float clamp, float amount)
        {
            if (
                amount < 0
                    ? value + amount >= clamp
                    : value + amount <= clamp
            ) return clamp;

            return value + (clamp - value) * amount;
        }
    }
}