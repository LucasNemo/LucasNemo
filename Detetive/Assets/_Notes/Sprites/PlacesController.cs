using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacesController : MonoBehaviour {

    public List<NoteBehaviour> places;


    private void Start()
    {
        var playerPlaces = Manager.Instance.MyGameInformation.Rs;

        foreach (var item in playerPlaces)
        {
            var correct = places.First(x => x.ID == item.P.MP);
            correct.SetName(Manager.Instance.PlacesNames[(Enums.Places)item.P.MP]);
        }
    }

}
