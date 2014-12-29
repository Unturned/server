using System;
using UnityEngine;

public class SkinColor
{
	public readonly static Color[] COLORS;

	static SkinColor()
	{
		SkinColor.COLORS = new Color[] { new Color(0.870588243f, 0.8352941f, 0.78039217f), new Color(0.7019608f, 0.5921569f, 0.4392157f), new Color(0.7490196f, 0.694117665f, 0.619607866f), new Color(0.905882359f, 0.709803939f, 0.5176471f), new Color(0.623529434f, 0.5686275f, 0.490196079f), new Color(0.5019608f, 0.4509804f, 0.3764706f), new Color(0.3529412f, 0.3137255f, 0.254901975f), new Color(0.254901975f, 0.223529413f, 0.180392161f), new Color(0.180392161f, 0.160784319f, 0.129411772f), new Color(0.68235296f, 0.105882354f, 0.105882354f), new Color(0.7411765f, 0.4745098f, 0.180392161f), new Color(0.498039216f, 0.247058824f, 0.521568656f), new Color(0.282352954f, 0.392156869f, 0.596078455f), new Color(0.137254909f, 0.5568628f, 0.5411765f), new Color(0.0627451f, 0.6156863f, 0.0470588244f), new Color(0.3882353f, 0.4862745f, 0.3882353f), new Color(0.7372549f, 0.6745098f, 0.129411772f), new Color(0.768627465f, 0.5058824f, 0.129411772f) };
	}

	public SkinColor()
	{
	}

	public static Color getColor(int id)
	{
		return SkinColor.COLORS[id];
	}
}