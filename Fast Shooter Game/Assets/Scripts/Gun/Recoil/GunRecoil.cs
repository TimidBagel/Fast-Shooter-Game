using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    [Header("Reference Points")]
    public Transform recoilPosition;
    public Transform rotationPoint;

    [Header("Speed Settings")]
    public float positionalRecoilSpeed = 12f;
    public float rotationalRecoilSpeed = 12f;

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 30f;

    [Header("Amount Settings")]
    public Vector3 recoilRotation = new Vector3(10, 5, 7);
    public Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.1f);
    public Vector3 recoilRotationAim = new Vector3(10, 4, 6);
    public Vector3 recoilKickBackAim = new Vector3(0.015f, 0f, 0f);

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 rot;

	private void Start()
	{
        recoilPosition = GameObject.FindGameObjectWithTag("Weapon Position").transform;
        rotationPoint = GameObject.FindGameObjectWithTag("Rotation Point").transform;
	}

	private void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        rot = Vector3.Slerp(rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rot);
    }

    public void RecoilFire(bool aiming)
    {
        if (aiming)
        {
            rotationalRecoil += new Vector3(-recoilRotationAim.x, Random.Range(-recoilRotationAim.y, recoilRotationAim.y), Random.Range(-recoilRotationAim.z, recoilRotationAim.z));
            positionalRecoil += new Vector3(Random.Range(-recoilKickBackAim.x, recoilKickBackAim.x), Random.Range(-recoilKickBackAim.y, recoilKickBackAim.y), recoilKickBackAim.z);
        }
        else
        {
            rotationalRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
            positionalRecoil += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
        }
    }
}
