/*

	// XRShooting.cs - For getting input from the controllers //
	Attach this to both the LeftHand Controller and the RightHand
	Controller GameObjects and select which hand from inside the inspector
	
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRShooting : MonoBehaviour
{
	public enum WhichHand { left, right };
	public WhichHand whichHand;

	void Start()
	{
		XRControllers.Instance.InputDevicesChanged += ConnectControllers;
		
		ConnectControllers();
	}

	private void ConnectControllers ()
	{
		if (whichHand == WhichHand.left)
		{
			device = XRControllers.Instance.XRLeftHand;
		}
		else if (whichHand == WhichHand.right)
		{
			device = XRControllers.Instance.XRRightHand;
		}
	}

	void Update()
	{
		bool triggerValue;
		if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue && Time.time > lastFire)
		{
			lastFire = Time.time + fireDelta;
			ShootGun(); 
		}
	}
	
	public void ShootGun()
	{
		GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);

		device.SendHapticImpulse(0, 0.7f, 0.2f);
	}
}
