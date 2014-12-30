using System;
using UnityEngine;

public class Look : MonoBehaviour
{
	public readonly static int STAND_EXTENT = 85;
	public readonly static int CROUCH_EXTENT = 70;
	public readonly static int PRONE_EXTENT = 70;
	public readonly static int CLIMB_EXTENT = 10;
	public readonly static int SWIM_EXTENT = 60;
	public static float lean;
	public static float rear;
	public static float yaw;
	public static float pitch;
	public static float yawReal;
	public static float pitchReal;
	private static float look_x;
	private static float look_y;
	public static float swayPitch;
	public static float swayRoll;
	public static float recoil_x;
	public static float recoil_y;
	public static float lastRecoil;
	public static Camera view;
	public static Camera zoom;
	public static float fov;
	private bool lastLock;
	private static RaycastHit hit;

	public static void recoil (float x, float y)
	{
		Look.recoil_x = Look.recoil_x + x * 0.95f;
		Look.recoil_y = Look.recoil_y + y * 0.95f;
		Look.yaw = Look.yaw + x;
		Look.pitch = Look.pitch + y;
		if ((double)(Time.realtimeSinceStartup - Look.lastRecoil) < 0.2) {
			Look.recoil_x = Look.recoil_x * 0.5f;
			Look.recoil_y = Look.recoil_y * 0.5f;
		}
		Look.lastRecoil = Time.realtimeSinceStartup;
	}

	public static void resetCameraPosition ()
	{
		Look.pitch = 0f;
		Look.pitchReal = 0f;
		if (Movement.vehicle == null) {
			Camera.main.transform.localPosition = new Vector3 (0f, 1.75f, 0f);
		} else {
			Look.yaw = 0f;
			Look.yawReal = 0f;
			Camera.main.transform.localPosition = new Vector3 (0f, 1.1f, 0f);
		}
		Look.swayPitch = 0f;
		Look.swayRoll = 0f;
		Look.lean = 0f;
	}

	private SunShafts s;

	public void Start ()
	{
		Look.fov = 0f;
		Look.view = base.transform.FindChild ("viewcamera").camera;
		Look.zoom = base.transform.FindChild ("scopecamera").camera;
		base.camera.layerCullSpherical = true;
		Look.zoom.layerCullSpherical = true;
		float[] singleArray = new float[] { Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, Single.MaxValue, 75f + GraphicsSettings.distance * 150f, 20f + GraphicsSettings.distance * 40f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 75f + GraphicsSettings.distance * 150f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 75f + GraphicsSettings.distance * 150f, 300f + GraphicsSettings.distance * 600f, 20f + GraphicsSettings.distance * 40f, Single.MaxValue, 75f + GraphicsSettings.distance * 150f, Single.MaxValue, 75f + GraphicsSettings.distance * 150f, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue };
		base.camera.layerCullDistances = singleArray;
		Look.zoom.layerCullDistances = singleArray;
		base.GetComponent<SSAOEffect> ().enabled = GraphicsSettings.ssao;
		base.GetComponent<BloomAndLensFlares> ().enabled = GraphicsSettings.bloom;
		base.GetComponent<AntialiasingAsPostEffect> ().enabled = GraphicsSettings.blur;
		base.GetComponent<DepthOfField34> ().enabled = GraphicsSettings.dof;
		base.GetComponent<CameraMotionBlur> ().enabled = GraphicsSettings.dof;
		Look.view.GetComponent<AntialiasingAsPostEffect> ().enabled = GraphicsSettings.blur;
		base.GetComponent<SunShafts> ().enabled = GraphicsSettings.streaks;
		//base.GetComponent<SunShafts>().sunTransform = GameObject.Find("day").transform;
		//Look.view.GetComponent<AntialiasingAsPostEffect>().enabled = GraphicsSettings.blur;
		//Look.zoom.GetComponent<AntialiasingAsPostEffect>().enabled = GraphicsSettings.blur;
		//Look.zoom.GetComponent<CameraMotionBlur>().enabled = GraphicsSettings.dof;
		Look.lean = 0f;
		Look.rear = 0f;
		Quaternion quaternion = base.transform.parent.parent.rotation;
		Look.yaw = quaternion.eulerAngles.y;
		Quaternion quaternion1 = base.transform.parent.parent.rotation;
		Look.yawReal = quaternion1.eulerAngles.y;
		Look.pitch = 0f;
		Look.pitchReal = 0f;
		Look.swayPitch = 0f;
		Look.swayRoll = 0f;
		if (GameObject.Find ("ground") != null) {
			Terrain component = GameObject.Find ("ground").GetComponent<Terrain> ();
			if (GraphicsSettings.foliage == 0) {
				component.detailObjectDensity = 0.1f;
				component.terrainData.wavingGrassAmount = 0f;
				component.detailObjectDistance = 50f;
			} else if (GraphicsSettings.foliage != 1) {
				component.detailObjectDensity = 1f;
				component.terrainData.wavingGrassAmount = 0.3f;
				component.detailObjectDistance = 250f;
			} else {
				component.detailObjectDensity = 0.5f;
				component.terrainData.wavingGrassAmount = 0f;
				component.detailObjectDistance = 100f;
			}
		}
		this.lastLock = false;
		GraphicsSettings.apply ();
	}

	public void Update ()
	{
		Look.yaw = Look.yaw - Mathf.Lerp (0f, Look.recoil_x, 4f * Time.deltaTime);
		Look.pitch = Look.pitch - Mathf.Lerp (0f, Look.recoil_y, 4f * Time.deltaTime);
		Look.recoil_x = Mathf.Lerp (Look.recoil_x, 0f, 4f * Time.deltaTime);
		Look.recoil_y = Mathf.Lerp (Look.recoil_y, 0f, 4f * Time.deltaTime);
		if (!GraphicsSettings.hud) {
			Look.pitchReal = Look.pitch;
			Look.yawReal = Look.yaw;
		} else {
			Look.pitchReal = Look.pitch;
			Look.yawReal = Look.yaw;
		}

		Camera camera = base.camera;
		float single = base.camera.fieldOfView;
		float single1 = GameSettings.fov - Look.fov;

		float perspective = 0;
		if (Movement.isSprinting) {
			perspective = 10;
		}
		
		camera.fieldOfView = Mathf.Lerp (single, single1 + (float)perspective, 4f * Time.deltaTime);
		Look.view.fieldOfView = Mathf.Lerp (Look.view.fieldOfView, 90f - Look.fov, 4f * Time.deltaTime);
		if (Movement.vehicle == null) {
			Look.look_x = Input.GetAxis ("Mouse X");
			Look.look_y = Input.GetAxis ("Mouse Y");
			if (InputSettings.lookInvert) {
				Look.look_y = Look.look_y * -1f;
			}
			Look.look_x = Look.look_x * InputSettings.mouseSensitivity;
			Look.look_y = Look.look_y * InputSettings.mouseSensitivity;
			if (Gun.aiming && Gun.dualrender) {
				Look.look_x = Look.look_x * (Look.zoom.fieldOfView / 45f);
				Look.look_y = Look.look_y * (Look.zoom.fieldOfView / 45f);
			}
			Look.look_x = Look.look_x * (Camera.main.fieldOfView / 90f);
			Look.look_y = Look.look_y * (Camera.main.fieldOfView / 90f);
			if (Screen.lockCursor) {
				Look.yaw = Look.yaw + Look.look_x;
				Look.pitch = Look.pitch - Look.look_y;
			}
			if (Movement.isClimbing || Movement.isSwimming) {
				if (Look.pitch < (float)(-Look.CLIMB_EXTENT)) {
					Look.pitch = (float)(-Look.CLIMB_EXTENT);
					Look.pitchReal = (float)(-Look.CLIMB_EXTENT);
				} else if (Look.pitch > (float)Look.CLIMB_EXTENT) {
					Look.pitch = (float)Look.CLIMB_EXTENT;
					Look.pitchReal = (float)Look.CLIMB_EXTENT;
				}
			} else if (Stance.state == 0) {
				if (Look.pitch < (float)(-Look.STAND_EXTENT)) {
					Look.pitch = (float)(-Look.STAND_EXTENT);
					Look.pitchReal = (float)(-Look.STAND_EXTENT);
				} else if (Look.pitch > (float)Look.STAND_EXTENT) {
					Look.pitch = (float)Look.STAND_EXTENT;
					Look.pitchReal = (float)Look.STAND_EXTENT;
				}
			} else if (Stance.state == 1) {
				if (Look.pitch < (float)(-Look.CROUCH_EXTENT)) {
					Look.pitch = (float)(-Look.CROUCH_EXTENT);
					Look.pitchReal = (float)(-Look.CROUCH_EXTENT);
				} else if (Look.pitch > (float)Look.CROUCH_EXTENT) {
					Look.pitch = (float)Look.CROUCH_EXTENT;
					Look.pitchReal = (float)Look.CROUCH_EXTENT;
				}
			} else if (Look.pitch < (float)(-Look.PRONE_EXTENT)) {
				Look.pitch = (float)(-Look.PRONE_EXTENT);
				Look.pitchReal = (float)(-Look.PRONE_EXTENT);
			} else if (Look.pitch > (float)Look.PRONE_EXTENT) {
				Look.pitch = (float)Look.PRONE_EXTENT;
				Look.pitchReal = (float)Look.PRONE_EXTENT;
			}
			Look.swayPitch = Mathf.Lerp (Look.swayPitch, (!Screen.lockCursor ? 0f : Movement.input_y), 4f * Time.deltaTime);
			Look.swayRoll = Mathf.Lerp (Look.swayRoll, (!Screen.lockCursor ? 0f : -Movement.input_x), 4f * Time.deltaTime);
			if (Player.life.dead) {
				base.transform.localPosition = Vector3.Lerp (base.transform.localPosition, new Vector3 (0f, 0.25f, 0f), 2f * Time.deltaTime);
			} else if (Stance.state == 0) {
				base.transform.localPosition = Vector3.Lerp (base.transform.localPosition, new Vector3 (0f, 1.75f, 0f), 2f * Time.deltaTime);
			} else if (Stance.state != 1) {
				base.transform.localPosition = Vector3.Lerp (base.transform.localPosition, new Vector3 (0f, 0.3f, 0f), 2f * Time.deltaTime);
			} else {
				base.transform.localPosition = Vector3.Lerp (base.transform.localPosition, new Vector3 (0f, 1.1f, 0f), 2f * Time.deltaTime);
			}
			base.transform.parent.parent.rotation = Quaternion.Euler (0f, Look.yawReal, 0f);
			base.transform.localRotation = Quaternion.Euler (Look.pitchReal + Look.swayPitch, 0f, Look.swayRoll);
			if (!Screen.lockCursor || Movement.isSwimming || Movement.isClimbing || !Movement.isGrounded) {
				Look.lean = Mathf.Lerp (Look.lean, 0f, 4f * Time.deltaTime);
			} else if (Input.GetKey (InputSettings.leanLeftKey)) {
				Physics.Raycast (Player.model.transform.position + Vector3.up, -Player.model.transform.right, out Look.hit, 0.75f, 4057105);
				if (Look.hit.collider != null) {
					Look.lean = Mathf.Lerp (Look.lean, 0f, 4f * Time.deltaTime);
				} else {
					Look.lean = Mathf.Lerp (Look.lean, 15f, 4f * Time.deltaTime);
				}
			} else if (!Input.GetKey (InputSettings.leanRightKey)) {
				Look.lean = Mathf.Lerp (Look.lean, 0f, 4f * Time.deltaTime);
			} else {
				Physics.Raycast (Player.model.transform.position + Vector3.up, Player.model.transform.right, out Look.hit, 0.75f, 4057105);
				if (Look.hit.collider != null) {
					Look.lean = Mathf.Lerp (Look.lean, 0f, 4f * Time.deltaTime);
				} else {
					Look.lean = Mathf.Lerp (Look.lean, -15f, 4f * Time.deltaTime);
				}
			}
			base.transform.parent.localRotation = Quaternion.Euler (0f, 0f, Look.lean);
		} else {
			Look.look_x = Input.GetAxis ("Mouse X");
			Look.look_y = Input.GetAxis ("Mouse Y");
			if (InputSettings.lookInvert) {
				Look.look_y = Look.look_y * -1f;
			}
			Look.look_x = Look.look_x * InputSettings.mouseSensitivity;
			Look.look_y = Look.look_y * InputSettings.mouseSensitivity;
			if (Gun.aiming && Gun.dualrender) {
				Look.look_x = Look.look_x * (Look.zoom.fieldOfView / 45f);
				Look.look_y = Look.look_y * (Look.zoom.fieldOfView / 45f);
			}
			if (Screen.lockCursor) {
				Look.yaw = Look.yaw + Look.look_x;
				Look.pitch = Look.pitch - Look.look_y;
			}
			if (Movement.isDriving) {
				if (Look.yaw < -100f) {
					Look.yaw = -100f;
					Look.yawReal = -100f;
				} else if (Look.yaw > 180f) {
					Look.yaw = 180f;
					Look.yawReal = 180f;
				}
			} else if (Look.yaw < -90f) {
				Look.yaw = -90f;
				Look.yawReal = -90f;
			} else if (Look.yaw > 90f) {
				Look.yaw = 90f;
				Look.yawReal = 90f;
			}
			if (Look.pitch < -30f) {
				Look.pitch = -30f;
				Look.pitchReal = -30f;
			} else if (Look.pitch > 30f) {
				Look.pitch = 30f;
				Look.pitchReal = 30f;
			}
			if (!Movement.isDriving) {
				Look.rear = 0f;
			} else {
				Look.rear = Mathf.Lerp (Look.rear, Mathf.Sin (Look.yaw / 170f) * 0.5f, 4f * Time.deltaTime);
			}
			base.transform.localPosition = new Vector3 (Look.rear, 1.1f, 0f);
			base.transform.localRotation = Quaternion.Euler (Look.pitchReal, Look.yawReal, 0f);
		}
		if (Screen.lockCursor != this.lastLock) {
			this.lastLock = Screen.lockCursor;
			base.GetComponent<Blur> ().enabled = !Screen.lockCursor;
		}
		if (GraphicsSettings.dof) {
			// TODO: DISABLE DOF
			/* if ((!Gun.aiming || !Gun.dualrender) && (Interact.range == -1f || Interact.range >= 2f))
			{
				base.GetComponent<DepthOfField34>().focalPoint = Mathf.Lerp(base.GetComponent<DepthOfField34>().focalPoint, 4096f, 4f * Time.deltaTime);
				base.GetComponent<DepthOfField34>().smoothness = Mathf.Lerp(base.GetComponent<DepthOfField34>().smoothness, 4096f, 4f * Time.deltaTime);
				base.GetComponent<DepthOfField34>().focalSize = Mathf.Lerp(base.GetComponent<DepthOfField34>().focalSize, 8192f, 4f * Time.deltaTime);
			}
			else
			{
				base.GetComponent<DepthOfField34>().focalPoint = Mathf.Lerp(base.GetComponent<DepthOfField34>().focalPoint, 1f, 4f * Time.deltaTime);
				base.GetComponent<DepthOfField34>().smoothness = Mathf.Lerp(base.GetComponent<DepthOfField34>().smoothness, 1f, 4f * Time.deltaTime);
				base.GetComponent<DepthOfField34>().focalSize = Mathf.Lerp(base.GetComponent<DepthOfField34>().focalSize, 1f, 4f * Time.deltaTime);
			}*/
		}
	}
}