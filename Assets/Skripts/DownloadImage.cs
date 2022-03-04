using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DownloadImage : MonoBehaviour
{
    [SerializeField] private Transform card;

    [SerializeField] private string urlLeft, urlRight;

    public void DownloadPhoto()
    {

        int rnd = Random.Range(0, 45);
        GetTexture(urlLeft + rnd + urlRight, (string error) =>
         {
             Debug.Log("error");
         }, (Texture2D texture2d) =>
         {
             Sprite sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(.5f, .6f), 135);
             gameObject.GetComponent<SpriteRenderer>().sprite = sprite;


         });

    }
    public void DownloadPhotoOnReady()
    {
        card.DORotate(new Vector2(0, 180), 0.3f, RotateMode.Fast);
        int rnd = Random.Range(0, 15);
        GetTexture(urlLeft + rnd + urlRight, (string error) =>
        {
            Debug.Log("error");
        }, (Texture2D texture2d) =>
        {
            Sprite sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(.5f, .6f), 135);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

            card.DORotate(new Vector2(0, 0), 0.3f, RotateMode.Fast);
        });

    }

    private void GetTexture(string url, Action<string> OnError, Action<Texture2D> OnSuccess)
    {
        StartCoroutine(GetTextureCoroutine(url, OnError, OnSuccess));
    }

    IEnumerator GetTextureCoroutine(string url, Action<string> OnError, Action<Texture2D> OnSuccess)
    {
        using (UnityWebRequest unitywebRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return unitywebRequest.SendWebRequest();
            if (unitywebRequest.isNetworkError || unitywebRequest.isHttpError) { OnError(unitywebRequest.error); }
            else
            {
                DownloadHandlerTexture downloadHandlerTexture = unitywebRequest.downloadHandler as DownloadHandlerTexture;
                OnSuccess(downloadHandlerTexture.texture);
            }
        }
    }
}
