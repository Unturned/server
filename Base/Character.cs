using System;
using UnityEngine;

public class Character : MonoBehaviour
{
	public int face = 6001;

	public int shirt = -1;

	public int pants = -1;

	public int hat = -1;

	public int hair = -1;

	public int backpack = -1;

	public int vest = -1;

	public int item = -1;

	public string state = string.Empty;

	public int skinColor;

	public int hairColor;

	public bool arm;

	private GameObject modelHat;

	private GameObject modelHair;

	private GameObject modelBackpack;

	private GameObject modelVest;

	private GameObject modelItem;

	public Character()
	{
	}

	public void wear()
	{
		base.transform.localScale = new Vector3(1f, 1f, (float)((!this.arm ? 1 : -1)));
		Texture2D texture2D = new Texture2D(64, 64, TextureFormat.RGBA32, false);
		Texture2D texture2D1 = null;
		Texture2D texture2D2 = null;
		Texture2D texture2D3 = null;
		if (this.face != -1)
		{
			texture2D1 = (Texture2D)Resources.Load(string.Concat("Textures/Faces/", this.face));
		}
		if (this.shirt != -1)
		{
			texture2D2 = (Texture2D)Resources.Load(string.Concat("Textures/Shirts/", this.shirt));
		}
		if (this.pants != -1)
		{
			texture2D3 = (Texture2D)Resources.Load(string.Concat("Textures/Pants/", this.pants));
		}
		for (int i = 0; i < 64; i++)
		{
			for (int j = 0; j < 64; j++)
			{
				if (texture2D1 != null && i >= 32 && j >= 16 && i < 48 && j < 32 && texture2D1.GetPixel(i - 32, j - 16) != Color.white)
				{
					texture2D.SetPixel(i, j, texture2D1.GetPixel(i - 32, j - 16));
				}
				else if (texture2D2 != null && texture2D2.GetPixel(i, j) != Color.white)
				{
					texture2D.SetPixel(i, j, texture2D2.GetPixel(i, j));
				}
				else if (!(texture2D3 != null) || !(texture2D3.GetPixel(i, j) != Color.white))
				{
					texture2D.SetPixel(i, j, SkinColor.getColor(this.skinColor));
				}
				else
				{
					texture2D.SetPixel(i, j, texture2D3.GetPixel(i, j));
				}
			}
		}
		texture2D.filterMode = FilterMode.Point;
		texture2D.name = "texture";
		texture2D.Apply();
		base.transform.FindChild("model").renderer.material.SetTexture("_MainTex", texture2D);
		if (this.modelHat != null)
		{
			UnityEngine.Object.Destroy(this.modelHat);
		}
		if (this.modelHair != null)
		{
			UnityEngine.Object.Destroy(this.modelHair);
		}
		if (this.modelBackpack != null)
		{
			UnityEngine.Object.Destroy(this.modelBackpack);
		}
		if (this.modelVest != null)
		{
			UnityEngine.Object.Destroy(this.modelVest);
		}
		if (this.modelItem != null)
		{
			UnityEngine.Object.Destroy(this.modelItem);
		}
		if (this.hat != -1)
		{
			this.modelHat = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Charmodels/", this.hat)));
			this.modelHat.name = "modelHat";
			this.modelHat.transform.parent = base.transform.FindChild("skeleton").FindChild("spine").FindChild("neck");
			this.modelHat.transform.localPosition = Vector3.zero;
			this.modelHat.transform.localRotation = Quaternion.identity;
			this.modelHat.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (this.hair != -1 && (this.hat == -1 || !ArmorStats.getCover(this.hat) || HairStyles.getBeard(this.hair) && !ArmorStats.getMask(this.hat)))
		{
			this.modelHair = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Charmodels/", this.hair)));
			this.modelHair.name = "modelHair";
			this.modelHair.transform.parent = base.transform.FindChild("skeleton").FindChild("spine").FindChild("neck");
			this.modelHair.transform.localPosition = Vector3.zero;
			this.modelHair.transform.localRotation = Quaternion.identity;
			texture2D = new Texture2D(4, 4, TextureFormat.RGBA32, false);
			for (int k = 0; k < 4; k++)
			{
				for (int l = 0; l < 4; l++)
				{
					texture2D.SetPixel(k, l, HairColor.getColor(this.hairColor));
				}
			}
			texture2D.filterMode = FilterMode.Point;
			texture2D.name = "texture";
			texture2D.Apply();
			this.modelHair.transform.FindChild("model").renderer.material.SetTexture("_MainTex", texture2D);
			this.modelHair.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (this.backpack != -1)
		{
			this.modelBackpack = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Charmodels/", this.backpack)));
			this.modelBackpack.name = "modelBackpack";
			this.modelBackpack.transform.parent = base.transform.FindChild("skeleton").FindChild("spine");
			this.modelBackpack.transform.localPosition = Vector3.zero;
			this.modelBackpack.transform.localRotation = Quaternion.identity;
			this.modelBackpack.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (this.vest != -1)
		{
			this.modelVest = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Charmodels/", this.vest)));
			this.modelVest.name = "modelVest";
			this.modelVest.transform.parent = base.transform.FindChild("skeleton").FindChild("spine");
			this.modelVest.transform.localPosition = Vector3.zero;
			this.modelVest.transform.localRotation = Quaternion.identity;
			this.modelVest.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (this.item != -1)
		{
			this.modelItem = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", this.item)));
			this.modelItem.name = "modelItem";
			if (this.item == 7004 || this.item == 7014)
			{
				this.modelItem.transform.parent = base.transform.FindChild("skeleton").FindChild("spine").FindChild("leftShoulder").FindChild("leftArmUpper").FindChild("leftArmLower").FindChild("leftHand");
				this.modelItem.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
			}
			else
			{
				this.modelItem.transform.parent = base.transform.FindChild("skeleton").FindChild("spine").FindChild("rightShoulder").FindChild("rightArmUpper").FindChild("rightArmLower").FindChild("rightHand");
				this.modelItem.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
			}
			this.modelItem.transform.localPosition = Vector3.zero;
			UnityEngine.Object.Destroy(this.modelItem.GetComponent<Useable>());
			this.modelItem.tag = "Enemy";
			this.modelItem.layer = 9;
			this.modelItem.transform.FindChild("model").gameObject.tag = "Enemy";
			this.modelItem.transform.FindChild("model").gameObject.layer = 9;
			this.modelItem.transform.FindChild("model").renderer.castShadows = true;
			this.modelItem.transform.FindChild("model").renderer.receiveShadows = true;
			this.modelItem.transform.localScale = new Vector3(1f, 1f, 1f);
			if (ItemType.getType(this.item) == 7 && this.state != string.Empty)
			{
				string[] strArrays = Packer.unpack(this.state, '\u005F');
				int num = int.Parse(strArrays[1]);
				int num1 = int.Parse(strArrays[2]);
				int num2 = int.Parse(strArrays[3]);
				int num3 = int.Parse(strArrays[4]);
				bool flag = strArrays[6] == "y";
				Transform transforms = this.modelItem.transform.FindChild("model").transform.FindChild("magazine");
				if (num != -1)
				{
					GameObject vector3 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", num)));
					vector3.name = "model";
					vector3.transform.parent = transforms;
					vector3.transform.localPosition = Vector3.zero;
					vector3.transform.localRotation = Quaternion.identity;
					vector3.tag = "Enemy";
					vector3.layer = 9;
					vector3.transform.FindChild("model").tag = "Enemy";
					vector3.transform.FindChild("model").gameObject.layer = 9;
					vector3.transform.FindChild("model").renderer.castShadows = true;
					vector3.transform.FindChild("model").renderer.receiveShadows = true;
					vector3.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				Transform transforms1 = this.modelItem.transform.FindChild("model").transform.FindChild("tactical");
				if (num1 != -1)
				{
					GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", num1)));
					gameObject.name = "model";
					gameObject.transform.parent = transforms1;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.identity;
					gameObject.tag = "Enemy";
					gameObject.layer = 9;
					gameObject.transform.FindChild("model").tag = "Enemy";
					gameObject.transform.FindChild("model").gameObject.layer = 9;
					gameObject.transform.FindChild("model").renderer.castShadows = true;
					gameObject.transform.FindChild("model").renderer.receiveShadows = true;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					if (num1 == 11002)
					{
						gameObject.transform.FindChild("model").FindChild("light").light.enabled = flag;
					}
					else if (num1 == 11003)
					{
						gameObject.transform.FindChild("model").FindChild("light_0").light.enabled = flag;
						gameObject.transform.FindChild("model").FindChild("light_1").light.enabled = flag;
					}
				}
				Transform transforms2 = this.modelItem.transform.FindChild("model").transform.FindChild("barrel");
				if (num2 != -1)
				{
					GameObject vector31 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", num2)));
					vector31.name = "model";
					vector31.transform.parent = transforms2;
					vector31.transform.localPosition = Vector3.zero;
					vector31.transform.localRotation = Quaternion.identity;
					vector31.tag = "Enemy";
					vector31.layer = 9;
					vector31.transform.FindChild("model").tag = "Enemy";
					vector31.transform.FindChild("model").gameObject.layer = 9;
					vector31.transform.FindChild("model").renderer.castShadows = true;
					vector31.transform.FindChild("model").renderer.receiveShadows = true;
					vector31.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				Transform transforms3 = this.modelItem.transform.FindChild("model").transform.FindChild("sight");
				if (num3 != -1)
				{
					GameObject gameObject1 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", num3)));
					gameObject1.name = "model";
					gameObject1.transform.parent = transforms3;
					gameObject1.transform.localPosition = Vector3.zero;
					gameObject1.transform.localRotation = Quaternion.identity;
					gameObject1.tag = "Enemy";
					gameObject1.layer = 9;
					gameObject1.transform.FindChild("model").tag = "Enemy";
					gameObject1.transform.FindChild("model").gameObject.layer = 9;
					gameObject1.transform.FindChild("model").renderer.castShadows = true;
					gameObject1.transform.FindChild("model").renderer.receiveShadows = true;
					UnityEngine.Object.Destroy(gameObject1.transform.FindChild("model").FindChild("aim").gameObject);
					gameObject1.transform.localScale = new Vector3(1f, 1f, 1f);
				}
			}
			else if ((this.item == 8001 || this.item == 8008) && this.state == "b")
			{
				this.modelItem.transform.FindChild("model").FindChild("light_0").light.enabled = true;
				this.modelItem.transform.FindChild("model").FindChild("light_1").light.enabled = true;
			}
		}
	}
}