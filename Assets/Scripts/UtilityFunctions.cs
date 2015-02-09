using System.Collections;
using UnityEngine;

public class UtilityFunctions {	
	public static Color32 ToColor(string HexVal)
	{		
		//Remove # if present
		if (HexVal.IndexOf('#') != -1)
			HexVal = HexVal.Replace("#", "");
	
		byte red = byte.Parse(HexVal.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
		byte green = byte.Parse(HexVal.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
		byte blue = byte.Parse(HexVal.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
		return new Color32 (red, green, blue, 255);
	}

	public static Color32 NextRandomBeautifulColor()
	{
		// to create lighter colours:
		// take a random integer between 0 & 128 (rather than between 0 and 255)
		// and then add 127 to make the colour lighter
		byte[] colorBytes = new byte[3];
		colorBytes[0] = (byte)(Random.Range(0, 128) + 127);
		colorBytes[1] = (byte)(Random.Range(0, 128) + 127);
		colorBytes[2] = (byte)(Random.Range(0, 128) + 127);
		
		var color = new Color32();
		
		// make the color fully opaque
		color.a = 255;
		color.r = colorBytes[0];
		color.b = colorBytes[1];
		color.g = colorBytes[2];
		
		return color;
	}
}
