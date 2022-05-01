using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float jumpForce, speed;
    public GameObject bulletPrefab, cubePrefab, cubePreview, moveJoystick, lookJoystick, cursor, gun, placeButton, shootButton;
    public bool building;
    public int selectedWeapon;
    public GameObject[] guns;

    public void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveJoystick.transform.localPosition.x/128 * speed, GetComponent<Rigidbody2D>().velocity.y);

        if (lookJoystick.transform.localPosition.x != 0 && lookJoystick.transform.localPosition.y != 0) cursor.transform.localPosition = new Vector2(lookJoystick.transform.localPosition.x / 128 * 2, lookJoystick.transform.localPosition.y / 128 * 2);
        Vector3 dir = cursor.transform.position - gun.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion lastRot = gun.transform.rotation;
        gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion newRot = gun.transform.rotation;
        gun.transform.rotation = Quaternion.Slerp(lastRot, newRot, 0.2f);
        Vector2 pos = cursor.transform.position;
        cubePreview.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), cubePrefab.transform.rotation.z);
        cubePreview.transform.rotation = cubePrefab.transform.rotation;
        if (cursor.transform.localPosition.x < 0)
        {
            gun.transform.localScale = new Vector3(1, -1, 1);
            cubePreview.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gun.transform.localScale = new Vector3(1, 1, 1);
            cubePreview.transform.localScale = new Vector3(1, 1, 1);
        }

        Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, transform.position - transform.forward * 10, 0.2f);
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Place()
    {
        if (cubePreview.GetComponent<buildingPreview>().touching == false)
        {
            Vector2 pos = cursor.transform.position;
            GameObject g = Instantiate(cubePrefab, new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), transform.position.z), Quaternion.identity);
            g.transform.localScale = cubePreview.transform.localScale;
        }
    }

    public void BuildingChange()
    {
        if (building == false)
        {
            building = true;
            placeButton.SetActive(true);
            cubePreview.SetActive(true);
            cursor.SetActive(true);
            gun.SetActive(false);
            shootButton.SetActive(false);
        }
        else
        {
            building = false;
            placeButton.SetActive(false);
            cubePreview.SetActive(false);
            cursor.SetActive(false);
            gun.SetActive(true);
            shootButton.SetActive(true);
        }
    }

    public void Shoot()
    {
        GameObject g = Instantiate(bulletPrefab, gun.transform.position + gun.transform.right, gun.transform.rotation);
        g.GetComponent<Rigidbody2D>().AddForce(gun.transform.right * 7, ForceMode2D.Impulse);
    }

    public void ChangeBuilding(GameObject building)
    {
        cubePrefab = building;
        cubePreview.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
    }
}
