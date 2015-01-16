using System;
using UnityEngine;

public class MenuRegister : MonoBehaviour
{
	public readonly static int ERROR_TIMEOUT;

	public static Transform lerpTo;

	public static SleekWindow window;

	public static SleekContainer container;

	public static SleekBox boxConnection;

	public static SleekImage iconConnection;

	public static SleekButton buttonCancel;

	public static SleekImage iconCancel;

	public static SleekButton buttonQuit;

	public static SleekImage iconQuit;

	private static float startedError;

	static MenuRegister()
	{
		MenuRegister.ERROR_TIMEOUT = 2;
		MenuRegister.startedError = Single.MaxValue;
	}

	public MenuRegister()
	{
	}

	public void Awake() {
		
	}

	public static void close()
	{
		MenuRegister.container.position = Coord2.ZERO;
		MenuRegister.container.lerp(new Coord2(0, 0, -1f, 0f), MenuRegister.container.size, 4f);
		MenuTitle menuTitle = new MenuTitle();
		MenuTitle.open();
	}

	public static void closeError()
	{
		MenuRegister.boxConnection.position = new Coord2(-155, -20, 0.5f, 0.5f);
		MenuRegister.boxConnection.lerp(new Coord2(-155, -20, -0.5f, 0.5f), MenuRegister.boxConnection.size, 4f);
		MenuRegister.container.visible = true;
	}

	public static void closeInfo()
	{
		MenuRegister.boxConnection.position = new Coord2(-155, -20, 0.5f, 0.5f);
		MenuRegister.boxConnection.lerp(new Coord2(-155, -20, -0.5f, 0.5f), MenuRegister.boxConnection.size, 4f);
	}

	public void OnGUI()
	{
		
	}

	public static void open() {
		MenuRegister.container.position = new Coord2(0, 0, 1f, 0f);
		MenuRegister.container.lerp(Coord2.ZERO, MenuRegister.container.size, 4f);
	}

	public static void openError(string text, string icon) {
		MenuRegister.startedError = Time.realtimeSinceStartup;
		MenuRegister.boxConnection.text = text;
		MenuRegister.iconConnection.setImage(icon);
		MenuRegister.boxConnection.position = new Coord2(-155, -20, -0.5f, 0.5f);
		MenuRegister.boxConnection.lerp(new Coord2(-155, -20, 0.5f, 0.5f), MenuRegister.boxConnection.size, 4f);
		MenuRegister.container.visible = false;
	}

	public static void openInfo(string text, string icon) {
		MenuRegister.boxConnection.text = text;
		MenuRegister.iconConnection.setImage(icon);
		MenuRegister.boxConnection.position = new Coord2(-155, -20, -0.5f, 0.5f);
		MenuRegister.boxConnection.lerp(new Coord2(-155, -20, 0.5f, 0.5f), MenuRegister.boxConnection.size, 4f);
	}

	public void Update() {
		if (PlayerSettings.id != string.Empty && MenuRegister.lerpTo != null) {
			base.transform.position = Vector3.Lerp(base.transform.position, MenuRegister.lerpTo.position, Time.deltaTime * 0.66f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, MenuRegister.lerpTo.rotation, Time.deltaTime * 0.66f);
		}
		
		if (Time.realtimeSinceStartup - MenuRegister.startedError > (float)MenuRegister.ERROR_TIMEOUT) {
			MenuRegister.startedError = Single.MaxValue;
			MenuRegister.closeError();
		}
		
		if (Time.realtimeSinceStartup - MenuGraphics.lastRes > 1f && !Input.GetMouseButtonDown(0)) {
			Debug.Log(":o");
			MenuGraphics.lastRes = Single.MaxValue;
			GraphicsSettings.apply();
		}
		
		if (MenuTitle.labelNews != null) {
			MenuTitle.labelNews.text = string.Concat(new string[] { "Version: ", Texts.VERSION_ID, "    Time: ", Sun.getTime(), "\n", Texts.NEWS });
		}
		
		Screen.lockCursor = false;
	}

	public static void usedCancel(SleekFrame frame) {
		NetworkTools.disconnect();
		MenuPlay.open();
		MenuRegister.closeError();
	}

	public static void usedQuit(SleekFrame frame) {
		Application.Quit();
	}
}