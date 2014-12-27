using System;

public class SleekContainer : SleekFrame
{
	public SleekContainer()
	{
	}

	public override void drawFrame()
	{
		base.update();
		base.drawChildren();
	}
}