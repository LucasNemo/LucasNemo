using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class HandleInvestigations : MonoBehaviour
{
    public ARElement[] arElements;

    public Transform[] arPos;

    public TipBehaviour tipModel; 

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
            EnableTips(roomInfo);
        }
    }

    private void EnableTips(Room roomInfo)
    {
        var tips = roomInfo.T;

        for (int i = 0; i < tips.Count; i++)
        {
            var pos = arPos.Length > i ? arPos[i] : arPos[0]; 
            SpawnTip(tips[i], pos.position);
        }
    }

    private void SpawnTip(TipItem tip, Vector3 pos)
    {
        var go = Instantiate(tipModel, pos, Quaternion.identity);
        go.SetupTip(tip.TP);
    }

    private void EnableWeapons(Room roomInfo)
    {
        var weapon = (Enums.Weapons)roomInfo.W.MW;

        var arWeapon = arElements.Where(x => x.arType == ARElement.ARType.Weapon).FirstOrDefault(x => x.weaponType == weapon);

        if (arWeapon)   
        {
            arWeapon.gameObject.SetActive(true);

            UnityEngine.Random.InitState( (int) Time.realtimeSinceStartup);
            
            //arWeapon.transform.position = new Vector3(rX, rY, rZ);

            arWeapon.onElementClicked = () =>
            {
                SceneManager.LoadScene("DetectiveScene");
            };
        }
       

    }
}
