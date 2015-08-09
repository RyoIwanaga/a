using UnityEngine;
using System.Collections;
using System.Reflection;

public class Reu
{
	public static string MakeFnName (MethodBase mBase)
	{
		return string.Format ("{0}:{1} ", 
		                      mBase.DeclaringType.FullName,
		                      mBase.Name);
	}


	// return 0f ~ 1f
	public static float Position1ToFraction(float p, float front, float back)
	{
		var range = Mathf.Abs(back - front);

		// * f b
		if (front < back) {
			if (p < front) {
				return 0f;
			}
			else {
				var fixed_pos = Mathf.Min (range, Mathf.Abs (p - front));

				return fixed_pos / range;
			}
		}
		// b f *
		else {
			if (p > front) {
				return 0f;
			}
			else {
				var fixed_pos = Mathf.Min (range, Mathf.Abs (front - p));

				return fixed_pos / range;
			}
		}
	}

	// return -1f ~ 1f
	public static float Position1ToFraction2(float l, float highMin, float highMax, float lowMin, float lowMax)
	{
		var fraction_high = Position1ToFraction(l, highMin, highMax);
		var fraction_low = Position1ToFraction(l, lowMin, lowMax);

		if (fraction_high != 0f)
			return fraction_high;

		if (fraction_low != 0f)
			return - fraction_low;

		return 0f;
	}

	// 30, 360  -> 30
	// 390, 360 -> 30
	// -30, 360 -> 330
	public static float Normalize(float value, float max)
	{
		var remainder = value % max;

		if (remainder >= 0) {
			return remainder;
		}
		else {
			return remainder + max;
		}
	}

	public static float NormalizeDegree(float value)
	{
		return Normalize(value, 360f);
	}

	public static float NormalizeRadian(float value)
	{
		return Normalize(value, Mathf.PI * 2f);
	}

	public static Vector2 Vec3XZToVec2(Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}

	public static float Vec2ToRadian(Vector2 v)
	{
		return Mathf.Atan2(v.y, v.x);
	}

	public static float Vec2ToDegree(Vector2 v)
	{
		return Vec2ToRadian(v) * Mathf.Rad2Deg;
	}
}
