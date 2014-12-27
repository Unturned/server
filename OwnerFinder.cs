using System;
using UnityEngine;

public class OwnerFinder
{
	public OwnerFinder()
	{
	}

	public static int getLimb(GameObject child)
	{
		if (child.name == "leftLegLower" || child.name == "rightLegLower")
		{
			return 0;
		}
		if (child.name == "leftLegUpper" || child.name == "rightLegUpper" || child.name == "backLeft" || child.name == "backRight")
		{
			return 1;
		}
		if (child.name == "leftArmLower" || child.name == "rightArmLower")
		{
			return 2;
		}
		if (child.name == "leftArmUpper" || child.name == "rightArmUpper" || child.name == "frontLeft" || child.name == "frontRight")
		{
			return 3;
		}
		if (child.name == "neck" || child.name == "skull")
		{
			return 4;
		}
		if (!(child.name == "spine") && !(child.name == "back"))
		{
			return -1;
		}
		return 5;
	}

	public static Vector3 getOrigin(GameObject owner, int limb)
	{
		if (limb == 0)
		{
			return owner.transform.position + (owner.transform.up * 0.25f);
		}
		if (limb == 1)
		{
			return owner.transform.position + (owner.transform.up * 0.55f);
		}
		if (limb == 2)
		{
			return owner.transform.position + (owner.transform.up * 0.8f);
		}
		if (limb == 3)
		{
			return owner.transform.position + (owner.transform.up * 1.25f);
		}
		if (limb == 4)
		{
			return owner.transform.position + (owner.transform.up * 1.7f);
		}
		if (limb != 5)
		{
			return Vector3.zero;
		}
		return owner.transform.position + (owner.transform.up * 1.1f);
	}

	public static GameObject getOwner(GameObject child)
	{
		if (child.name == "leftLegLower" || child.name == "rightLegLower")
		{
			return child.transform.parent.parent.parent.parent.parent.parent.gameObject;
		}
		if (child.name == "leftLegUpper" || child.name == "rightLegUpper")
		{
			return child.transform.parent.parent.parent.parent.parent.gameObject;
		}
		if (child.name == "leftArmLower" || child.name == "rightArmLower")
		{
			return child.transform.parent.parent.parent.parent.parent.parent.parent.gameObject;
		}
		if (child.name == "leftArmUpper" || child.name == "rightArmUpper")
		{
			return child.transform.parent.parent.parent.parent.parent.parent.gameObject;
		}
		if (child.name == "neck")
		{
			return child.transform.parent.parent.parent.parent.parent.gameObject;
		}
		if (child.name == "spine")
		{
			return child.transform.parent.parent.parent.parent.gameObject;
		}
		if (child.name == "back" || child.name == "backLeft" || child.name == "backRight")
		{
			return child.transform.parent.parent.parent.parent.gameObject;
		}
		if (!(child.name == "skull") && !(child.name == "frontLeft") && !(child.name == "frontRight"))
		{
			return null;
		}
		return child.transform.parent.parent.parent.parent.parent.gameObject;
	}
}