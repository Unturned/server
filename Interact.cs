using System;
using UnityEngine;

public class Interact : MonoBehaviour
{
	public static GameObject edit;

	public static GameObject focus;

	private static Material material;

	public static Interactable interactable;

	public static string hint;

	public static string icon;

	public static float range;

	private static RaycastHit hit;

	static Interact()
	{
		Interact.hint = string.Empty;
		Interact.icon = string.Empty;
	}

	public Interact()
	{
	}

	public static void interact(GameObject model)
	{
		Interact.edit = model;
		HUDGame.interacting = true;
		HUDInventory.openInteract();
	}

	public void Start()
	{
		Interact.focus = null;
		Interact.hint = string.Empty;
	}

	public void Update()
	{
		if (Interact.edit != null && (Input.GetKeyDown(InputSettings.interactKey) || (Interact.edit.transform.position - Player.model.transform.position).magnitude > 4f))
		{
			HUDGame.interacting = false;
			Interact.edit = null;
			HUDInventory.close();
		}
		if (GraphicsSettings.dof)
		{
			Physics.Raycast(base.transform.position, base.transform.forward, out Interact.hit, 512f, RayMasks.INTERACTABLE);
			if (Interact.hit.collider != null)
			{
				Interact.range = Interact.hit.distance;
			}
			else
			{
				Interact.range = -1f;
			}
		}
		if (Movement.vehicle == null)
		{
			if (!GraphicsSettings.dof || Interact.hit.collider != null && Interact.hit.distance < 8f)
			{
				if (!GraphicsSettings.dof)
				{
					Physics.Raycast(base.transform.position, base.transform.forward, out Interact.hit, 8f, RayMasks.INTERACTABLE);
				}
				if (Interact.hit.collider == null)
				{
					if (Interact.focus != null && Interact.focus.renderer != null && Interact.material.shader.name != "Transparent/Cutout/Diffuse")
					{
						Interact.focus.renderer.material = Interact.material;
					}
					Interact.focus = null;
					Interact.material = null;
					HUDGame.setHint(Interact.hint, null, Color.white, Color.white, Interact.icon);
				}
				else if (Interact.hit.collider.tag == "Enemy")
				{
					if (Interact.focus != null && Interact.focus.renderer != null && Interact.material.shader.name != "Transparent/Cutout/Diffuse")
					{
						Interact.focus.renderer.material = Interact.material;
					}
					Interact.focus = null;
					Interact.material = null;
					NetworkUser component = OwnerFinder.getOwner(Interact.hit.collider.gameObject).GetComponent<Player>().owner;
					if (!(PlayerSettings.friendHash != string.Empty) || !(component.friend == PlayerSettings.friendHash) || !(component.nickname != string.Empty))
					{
						HUDGame.setHint(component.name, null, (component.status != 21 ? Color.white : Colors.GOLD), (component.status != 21 ? Color.white : Colors.GOLD), Reputation.getIcon(component.reputation));
					}
					else
					{
						HUDGame.setHint(string.Concat(component.name, " [", component.nickname, "]"), null, (component.status != 21 ? Color.white : Colors.GOLD), (component.status != 21 ? Color.white : Colors.GOLD), Reputation.getIcon(component.reputation));
					}
				}
				else if (Interact.hit.distance >= 3f)
				{
					if (Interact.focus != null && Interact.focus.renderer != null && Interact.material.shader.name != "Transparent/Cutout/Diffuse")
					{
						Interact.focus.renderer.material = Interact.material;
					}
					Interact.focus = null;
					Interact.material = null;
					HUDGame.setHint(Interact.hint, null, Color.white, Color.white, Interact.icon);
				}
				else
				{
					Interact.interactable = Interact.hit.collider.GetComponent<Interactable>();
					if (Interact.interactable == null)
					{
						if (Interact.focus != null && Interact.focus.renderer != null && Interact.material.shader.name != "Transparent/Cutout/Diffuse")
						{
							Interact.focus.renderer.material = Interact.material;
						}
						Interact.focus = null;
						Interact.material = null;
						HUDGame.setHint(Interact.hint, null, Color.white, Color.white, Interact.icon);
					}
					else
					{
						if (Interact.focus == null)
						{
							Interact.focus = Interact.hit.collider.gameObject;
							if (Interact.focus.renderer != null)
							{
								Interact.material = Interact.hit.collider.renderer.material;
								if (GraphicsSettings.hud && Interact.hit.collider.renderer.material.shader.name != "Transparent/Cutout/Diffuse")
								{
									if (Interact.interactable.hint() == string.Empty)
									{
										Interact.hit.collider.renderer.material = (Material)Resources.Load("Materials/Help/badInteract");
									}
									else
									{
										Interact.hit.collider.renderer.material = (Material)Resources.Load("Materials/Help/goodInteract");
									}
								}
								Interact.hit.collider.renderer.material.mainTexture = Interact.material.mainTexture;
								Interact.hit.collider.renderer.material.color = Interact.material.color;
							}
						}
						else if (Interact.focus != Interact.hit.collider.gameObject)
						{
							if (Interact.focus != null && Interact.focus.renderer != null && Interact.material.shader.name != "Transparent/Cutout/Diffuse")
							{
								Interact.focus.renderer.material = Interact.material;
							}
							Interact.focus = Interact.hit.collider.gameObject;
							if (Interact.focus.renderer != null)
							{
								Interact.material = Interact.hit.collider.renderer.material;
								if (GraphicsSettings.hud && Interact.hit.collider.renderer.material.shader.name != "Transparent/Cutout/Diffuse")
								{
									if (Interact.interactable.hint() == string.Empty)
									{
										Interact.hit.collider.renderer.material = (Material)Resources.Load("Materials/Help/badInteract");
									}
									else
									{
										Interact.hit.collider.renderer.material = (Material)Resources.Load("Materials/Help/goodInteract");
									}
								}
								Interact.hit.collider.renderer.material.mainTexture = Interact.material.mainTexture;
								Interact.hit.collider.renderer.material.color = Interact.material.color;
							}
						}
						if (Interact.interactable.hint() != string.Empty)
						{
							HUDGame.setHint(string.Concat(new object[] { Interact.interactable.hint(), " [", InputSettings.interactKey, "]" }), (Interact.focus.transform.FindChild("focus") == null ? Interact.focus : Interact.focus.transform.FindChild("focus").gameObject), Color.white, Color.white, Interact.interactable.icon());
						}
					}
				}
			}
			else
			{
				if (Interact.focus != null && Interact.focus.renderer != null && Interact.material.shader.name != "Transparent/Cutout/Diffuse")
				{
					Interact.focus.renderer.material = Interact.material;
				}
				Interact.focus = null;
				Interact.material = null;
				HUDGame.setHint(Interact.hint, null, Color.white, Color.white, Interact.icon);
			}
			if (Input.GetKeyDown(InputSettings.interactKey) && !Player.life.dead && Interact.focus != null && Interact.interactable.hint() != string.Empty && Screen.lockCursor)
			{
				Interact.interactable.trigger();
			}
		}
		else
		{
			if (Input.GetKeyDown(InputSettings.interactKey) && !Player.life.dead && Screen.lockCursor)
			{
				Movement.vehicle.trigger();
			}
			if (Interact.focus != null && Interact.focus.renderer != null && Interact.material.shader.name != "Transparent/Cutout/Diffuse")
			{
				Interact.focus.renderer.material = Interact.material;
			}
			Interact.focus = null;
			Interact.material = null;
			Interact.interactable = null;
			HUDGame.setHint(string.Concat("Exit [", InputSettings.interactKey, "] - Seats [F1-6]"), null, Color.white, Color.white, string.Empty);
		}
	}
}