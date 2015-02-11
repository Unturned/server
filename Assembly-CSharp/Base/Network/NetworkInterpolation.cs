using System;
using UnityEngine;
using Unturned.Log;

public class NetworkInterpolation : MonoBehaviour
{
	public readonly static int INTERPOLATION_RATE;

	private Vector3 position = Vector3.zero;

	private Quaternion rotation = Quaternion.identity;

	private Vector3 lastPosition = Vector3.zero;

	private Quaternion lastRotation = Quaternion.identity;

	private Vector3 lastTele = Vector3.zero;

	private float timeTele;

	private float passedDistance;

	private float passedHeight;

	private float timeBuffer;

	static NetworkInterpolation()
	{
		NetworkInterpolation.INTERPOLATION_RATE = 4;
	}

	public NetworkInterpolation()
	{
	}

	[RPC]
	public void askStatePosition(NetworkPlayer player)
	{
		if (player != Network.player)
		{
			base.networkView.RPC("tellStatePosition", player, new object[] { this.position, this.rotation });
		}
		else
		{
			this.tellStatePosition_Pizza(this.position, this.rotation);
		}
	}

	public void Awake()
	{
		this.passedDistance = 0f;
		this.passedHeight = 0f;
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
		if (!base.networkView.isMine && !Network.isServer)
		{
			base.networkView.RPC("askStatePosition", RPCMode.Server, new object[] { Network.player });
		}
	}

	[RPC]
	public void tellPosition(Vector3 setPosition, NetworkMessageInfo info)
	{
		if (info.sender == base.networkView.owner)
		{
			if (!Network.isServer || !(base.tag == "Player") && !(base.tag == "Enemy") || !(this.lastTele != Vector3.zero) || this.timeTele == 0f || !(base.transform.parent != null) || !(base.transform.parent.name == "models"))
			{
				this.passedDistance = 0f;
				this.passedHeight = 0f;
				this.timeBuffer = 0f;
			}
			else
			{
				NetworkInterpolation networkInterpolation = this;
				networkInterpolation.passedDistance = networkInterpolation.passedDistance + Mathf.Sqrt(Mathf.Pow(setPosition.x - this.lastTele.x, 2f) + Mathf.Pow(setPosition.z - this.lastTele.z, 2f));
				NetworkInterpolation networkInterpolation1 = this;
				networkInterpolation1.passedHeight = networkInterpolation1.passedHeight + (setPosition.y - this.lastTele.y);
				NetworkInterpolation networkInterpolation2 = this;
				networkInterpolation2.timeBuffer = networkInterpolation2.timeBuffer + (Time.realtimeSinceStartup - this.timeTele);
				if (this.timeBuffer > 3f)
				{
					if (this.passedDistance / this.timeBuffer > 13f || this.passedHeight / this.timeBuffer < -12f || this.passedHeight / this.timeBuffer > 5f)
					{
						NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for suspected teleport."));
					}
					else
					{
						this.passedDistance = 0f;
						this.passedHeight = 0f;
						this.timeBuffer = 0f;
					}
				}
			}
			this.lastTele = setPosition;
			this.timeTele = Time.realtimeSinceStartup;
			this.position = setPosition;
		}
	}

	[RPC]
	public void tellRotation(Quaternion setRotation)
	{
		this.rotation = setRotation;
	}

	[RPC]
	public void tellStatePosition(Vector3 setPosition, Quaternion setRotation, NetworkMessageInfo info)
	{
		if (info.sender != Network.player) 
		{
			NetworkUser user = NetworkUserList.getUserFromPlayer(info.sender);
			Logger.LogSecurity(user.id, user.name, "Tried to use teleport hack");
		}


		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.tellStatePosition_Pizza(setPosition, setRotation);
		}
	}

	public void tellStatePosition_Pizza(Vector3 setPosition, Quaternion setRotation)
	{
		this.lastTele = setPosition;
		this.timeTele = Time.realtimeSinceStartup;
		this.passedDistance = 0f;
		this.timeBuffer = 0f;
		this.position = setPosition;
		this.rotation = setRotation;

		if (base.transform.parent != null && base.transform.parent.name == "models")
		{
			base.transform.position = this.position;
			base.transform.rotation = this.rotation;
			if (base.tag == "Player" && base.networkView.isMine)
			{
				Look.yaw = base.transform.rotation.eulerAngles.y;
				Look.yawReal = base.transform.rotation.eulerAngles.y;
			}
		}
	}

	public void Update()
	{
        if (base.networkView.isMine)
		{
            DedicatedServer.CheckPlayer( base.networkView.owner, "NetworkInterpolation @Update" );

			this.position = base.transform.position;
			this.rotation = base.transform.rotation;

			if ((double)Mathf.Abs(this.position.x - this.lastPosition.x) > 0.2 || (double)Mathf.Abs(this.position.y - this.lastPosition.y) > 0.2 || (double)Mathf.Abs(this.position.z - this.lastPosition.z) > 0.2) {
				this.lastPosition = this.position;
				base.networkView.RPC("tellPosition", RPCMode.Others, new object[] { this.position });
			}
			
			if (Mathf.Abs(this.rotation.eulerAngles.x - this.lastRotation.eulerAngles.x) > 5f || Mathf.Abs(this.rotation.eulerAngles.y - this.lastRotation.eulerAngles.y) > 5f || Mathf.Abs(this.rotation.eulerAngles.z - this.lastRotation.eulerAngles.z) > 5f) {
				this.lastRotation = this.rotation;
				base.networkView.RPC("tellRotation", RPCMode.Others, new object[] { this.rotation });
			}
		}
		else if (base.transform.parent != null && base.transform.parent.name == "models")
		{
			this.lastPosition = base.transform.position;
			this.lastRotation = base.transform.rotation;
			if ((double)Mathf.Abs(this.position.x - this.lastPosition.x) > 0.2 || (double)Mathf.Abs(this.position.y - this.lastPosition.y) > 0.2 || (double)Mathf.Abs(this.position.z - this.lastPosition.z) > 0.2 || Mathf.Abs(this.rotation.eulerAngles.x - this.lastRotation.eulerAngles.x) > 5f || Mathf.Abs(this.rotation.eulerAngles.y - this.lastRotation.eulerAngles.y) > 5f || Mathf.Abs(this.rotation.eulerAngles.z - this.lastRotation.eulerAngles.z) > 5f) {
				base.transform.position = Vector3.Lerp(this.lastPosition, this.position, (float)NetworkInterpolation.INTERPOLATION_RATE * Time.deltaTime);
				base.transform.rotation = Quaternion.Lerp(this.lastRotation, this.rotation, (float)NetworkInterpolation.INTERPOLATION_RATE * Time.deltaTime);
			}
		}
	}
}