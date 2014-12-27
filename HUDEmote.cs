using System;

public class HUDEmote
{
	public static SleekContainer container;

	public static bool state;

	public static SleekButton waveButton;

	public static SleekButton pointButton;

	public static SleekButton surrenderButton;

	public HUDEmote()
	{
		HUDEmote.container = new SleekContainer()
		{
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDGame.container.addFrame(HUDEmote.container);
		HUDEmote.container.visible = false;
		HUDEmote.waveButton = new SleekButton()
		{
			position = new Coord2(50, -20, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_WAVE
		};
		HUDEmote.waveButton.onUsed += new SleekDelegate(HUDEmote.usedWave);
		HUDEmote.container.addFrame(HUDEmote.waveButton);
		HUDEmote.pointButton = new SleekButton()
		{
			position = new Coord2(-250, -20, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_POINT
		};
		HUDEmote.pointButton.onUsed += new SleekDelegate(HUDEmote.usedPoint);
		HUDEmote.container.addFrame(HUDEmote.pointButton);
		HUDEmote.surrenderButton = new SleekButton()
		{
			position = new Coord2(-100, -90, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_SURRENDER
		};
		HUDEmote.surrenderButton.onUsed += new SleekDelegate(HUDEmote.usedSurrender);
		HUDEmote.container.addFrame(HUDEmote.surrenderButton);
		HUDEmote.state = false;
	}

	public static void close()
	{
		HUDEmote.state = false;
		HUDEmote.container.visible = false;
	}

	public static void open()
	{
		HUDEmote.state = true;
		HUDEmote.container.visible = true;
	}

	public static void usedPoint(SleekFrame frame)
	{
		Player.play("point");
		Viewmodel.play("point");
		HUDEmote.close();
	}

	public static void usedSurrender(SleekFrame frame)
	{
		Player.play("surrender");
		Viewmodel.play("surrender");
		HUDEmote.close();
	}

	public static void usedWave(SleekFrame frame)
	{
		Player.play("wave");
		Viewmodel.play("wave");
		HUDEmote.close();
	}
}