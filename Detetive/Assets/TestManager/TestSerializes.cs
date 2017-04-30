using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSerializes : MonoBehaviour {

    public string data;

    public void Start()
    {

        var deserialized =  JsonConvert.DeserializeObject<GameInformation>(data) ;

        print(deserialized);

    }

}
