using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RESTClient : MonoBehaviour
{
    public string Uri;
    // Start is called before the first frame update
    void Start()
    {

        // Player testes = JsonUtility.FromJson<Player>("{"+ "Id"+":"+"1"+","+"Name"+":Hivara,"+"Score"+":0.8}");

        StartCoroutine(GET(Uri,Imprimedatos));

        //StartCoroutine(GetRequest(Uri));

        //StartCoroutine(POST(uri,"new data send"));
    }

    void Imprimedatos(RootObject codes)
    {

        //foreach (RestResponse a in codes.RestResponse)
        //{
        //    Debug.Log("----------");
        //    Debug.Log(a.name);
        //}

    }

    //IEnumerator GetRequest(string uri)
    //{
    //    using (WWW webRequest = new WWW(uri))
    //    {
    //        // Request and wait for the desired page.
    //        yield return webRequest;

    //        if (webRequest.isDone)
    //        {
    //            string resultado = webRequest.text;

    //            //Debug.Log(webRequest.text);   
    //        }
    //        else
    //        {
    //            Debug.Log(": Error: ");
    //        }
    //    }
    //}

    IEnumerator GET(string uri,Action<RootObject> Callback) {

        RootObject roote = new RootObject() { capital = "tepeyisco"};

        string infoString = roote.capital.ToString();

        byte[] infoSend = System.Text.Encoding.ASCII.GetBytes(infoString);
        Debug.Log(infoSend.Length);

        string infoDesirialized = System.Text.Encoding.ASCII.GetString(infoSend);

        Debug.Log(infoDesirialized);

        WWWForm form = new WWWForm();
        form.AddField("name", infoString);

        Debug.Log(form.data.Length);

        using (UnityWebRequest content = UnityWebRequest.Get(uri))
        {

            yield return content.SendWebRequest();

            if (content.isNetworkError)
            {

                Debug.Log("error al hacer peticion   " + content.error);
            }

            else
            {
                if (content.isDone)
                {

                    string resultado = System.Text.Encoding.UTF8.GetString(content.downloadHandler.data);

                    //Debug.Log(resultado);

                    var result = JsonHelper.getJsonArray<RootObject>(resultado);


                    foreach (RootObject a in result) {
                        //Debug.Log(a.name);
                    }

                    //Callback(codes);
                }
            }
        }
    }


    IEnumerator POST(string url,string data) {

        UnityWebRequest PREQ = UnityWebRequest.Post(url,JsonUtility.ToJson(data));

        WWWForm form = new WWWForm();
        form.AddField("name","robertito",System.Text.Encoding.UTF8);

        yield return PREQ.SendWebRequest();

        if (PREQ.isNetworkError)
        {
            Debug.Log("and error sending data");
        }

        else {

            if (PREQ.isDone) {

                Debug.Log("data send  "  + PREQ.uploadHandler.data);
            }
        }
    }
}
