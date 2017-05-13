using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleInvestigations : MonoBehaviour
{
    public ARElement[] arElements;  //List of elements on scene
    public List<Transform> arPos;   //The list of positions 
    public TipBehaviour tipModel;   //The base of any tip!
    public GameObject murdererPlace;
    public Text title;

    private void Start()
    {
        if (Manager.Instance.ActiveRoom == null) return;

        GenericModal.Instance.RemoveHandlers();

        //Randomize the array of positios!
        arPos = arPos.SortList();
        SetCurrentRoom( Manager.Instance.ActiveRoom );
    }

    private void SetCurrentRoom(Room roomInfo)
    {
        //Set the name choosed by player
        title.text = roomInfo.P.N;

        StartCoroutine(EnableModels(roomInfo));
    }

    public IEnumerator EnableModels(Room roomInfo)
    {
        yield return new WaitForSecondsRealtime(2f);

        FindObjectOfType<GyroCamera>().CalibrateYAngle();

        //Only enable tips and 3d models when the scene has it! - of course! 
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
            SpawnTip(tips[i], GetSpawnPosition());
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
            arWeapon.transform.position = GetSpawnPosition();
            arWeapon.onElementClicked = () =>
            {
             //    SceneManager.LoadScene("DetectiveScene");
            };
        }
    }

    /// <summary>
    /// Get a position from the array of positions then remove the indice from collection!
    /// </summary>
    /// <returns></returns>
    private Vector3 GetSpawnPosition()
    {
        var id = arPos.Count - 1;
        if (id < 0) return new Vector3(5, 5, 5);
        var p = arPos[id];
        Vector3 pos = new Vector3(p.position.x, p.position.y, p.position.z);
        arPos.RemoveAt(id);
        return pos;
    }
}
