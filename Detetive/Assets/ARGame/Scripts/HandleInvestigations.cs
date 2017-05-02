using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class HandleInvestigations : MonoBehaviour
{
    public ARElement[] arElements;

    private void Start()
    {
         SetCurrentRoom( Manager.Instance.ActiveRoom );
    }

    private void SetCurrentRoom(object activeRoom)
    {
        throw new NotImplementedException();
    }

    public void SetCurrentRoom(Room roomInfo)
    {
        if (roomInfo.W != null)
        {
            EnableWeapons(roomInfo);
        }

        if (roomInfo.T != null && roomInfo.T.Count > 0)
        {
            EnableTips();
        }

    }

    private void EnableTips()
    {
        throw new NotImplementedException();
    }

    private void EnableWeapons(Room roomInfo)
    {
        var weapon = (Enums.Weapons)roomInfo.W.MW;

        var arWeapon = arElements.Where(x => x.arType == ARElement.ARType.Weapon).FirstOrDefault(x => x.weaponType == weapon);

        if (arWeapon)
        {
            arWeapon.gameObject.SetActive(true);

            UnityEngine.Random.InitState( (int) Time.realtimeSinceStartup);

            var rX = UnityEngine.Random.Range(-10, 10);
            var rY = UnityEngine.Random.Range(-10, 10);
            var rZ = UnityEngine.Random.Range(-10, 10);

            arWeapon.transform.position = new Vector3(rX, rY, rZ);

            arWeapon.onElementClicked = () =>
            {
                SceneManager.LoadScene("DetectiveScene");
            };
        }
    }
}
