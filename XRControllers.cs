/*

	// XRControllers.cs - For getting the right and left controllers //
	Add "XRControllers.Instance.InputDevicesChanged += [method name];" in
	another script to register when devices have changed in some way. Add
	this script to the XR Rig.

*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class XRControllers : MonoBehaviour
{
	public static XRControllers Instance { get; private set; }

	public InputDevice XRLeftHand;
	public InputDevice XRRightHand;
	public UnityAction InputDevicesChanged;

	private void Awake()
	{
		InputDevices.deviceConnected += InputDevices_devicesChanged;
		InputDevices.deviceDisconnected += InputDevices_devicesChanged;
		InputDevices.deviceConfigChanged += InputDevices_devicesChanged;
	}

	private void OnDestroy()
	{
		InputDevices.deviceConnected -= InputDevices_devicesChanged;
		InputDevices.deviceDisconnected -= InputDevices_devicesChanged;
		InputDevices.deviceConfigChanged -= InputDevices_devicesChanged;
	}

	private void InputDevices_devicesChanged(InputDevice device)
	{
		CheckForControllers();
	}

	public bool CheckForControllers()
	{
		bool controllerLeftFound = false;
		bool controllerRightFound = false;

		List<InputDevice> leftHandDevices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);

		if (leftHandDevices.Count > 0)
		{
			controllerLeftFound = true;
			XRLeftHand = leftHandDevices[0];
		}

		List<InputDevice> rightHandDevices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);
		if (rightHandDevices.Count > 0)
		{
			controllerRightFound = true;
			XRRightHand = rightHandDevices[0];
		}
		
		InputDevicesChanged();

		if (!controllerLeftFound || !controllerRightFound)
		{
			// One of the controllers is missing
			return false;
		} else
		{
			return true;
		}
	}
}