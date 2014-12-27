using System;
using UnityEngine;

public class HairColor
{
	public readonly static Color[] COLORS;

	static HairColor()
	{
		HairColor.COLORS = new Color[] { new Color(0.09803922f, 0.09803922f, 0.09803922f), new Color(0.196078435f, 0.196078435f, 0.196078435f), new Color(0.254901975f, 0.223529413f, 0.203921571f), new Color(0.3529412f, 0.3019608f, 0.266666681f), new Color(0.454901963f, 0.384313732f, 0.3372549f), new Color(0.654902f, 0.654902f, 0.654902f), new Color(0.75686276f, 0.7019608f, 0.627451f), new Color(0.8509804f, 0.8f, 0.7254902f), new Color(0.8117647f, 0.8117647f, 0.8117647f), new Color(0.6039216f, 0.02745098f, 0.02745098f), new Color(0.6627451f, 0.396078438f, 0.101960786f), new Color(0.419607848f, 0.168627456f, 0.443137258f), new Color(0.203921571f, 0.3137255f, 0.5176471f), new Color(0.05882353f, 0.478431374f, 0.4627451f), new Color(0.0235294122f, 0.5372549f, 0.007843138f), new Color(0.309803933f, 0.407843143f, 0.309803933f), new Color(0.65882355f, 0.596078455f, 0.0509803928f), new Color(0.6901961f, 0.427450985f, 0.0509803928f) };
	}

	public HairColor()
	{
	}

	public static Color getColor(int id)
	{
		return HairColor.COLORS[id];
	}
}