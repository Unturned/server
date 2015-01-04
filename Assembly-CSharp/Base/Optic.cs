using System;

public class Optic : Useable
{
	public Optic()
	{
	}

	public override void dequip()
	{
		//HUDGame.binoculars = false;
		Look.fov = 0f;
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startSecondary()
	{
		Viewmodel.play("startLook");
		//HUDGame.binoculars = true;
		Look.fov = GameSettings.fov - 90f + 80f;
	}

	public override void stopSecondary()
	{
		Viewmodel.play("stopLook");
        //HUDGame.binoculars = false;
		Look.fov = 0f;
	}
}