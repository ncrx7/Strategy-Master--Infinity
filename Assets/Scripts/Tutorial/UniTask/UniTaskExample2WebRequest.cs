using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.Networking;
using System;

public class UniTaskExample2WebRequest : MonoBehaviour
{
    [SerializeField] private Image[] _images;

    async void Start()
    {
        string apiKey = "https://reqres.in/api/users/2";
        string[] urls = new string[]
        {
            "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Yone_0.jpg",
            "https://cmsassets.rgpub.io/sanity/images/dsfx7636/game_data/28a559dfaa320f5b9641c3dc116fe3f6f9cea155-1280x720.jpg",
            "https://liquipedia.net/commons/images/thumb/f/f6/WR_Infobox_Yone.jpg/600px-WR_Infobox_Yone.jpg"
        };

        List<UniTask<Sprite>> getSpriteTask = new List<UniTask<Sprite>>();
        foreach (string url in urls)
        {
            getSpriteTask.Add(GetImageFromWebRequest(url, this.GetCancellationTokenOnDestroy()));
        }

        Sprite[] sprites = await UniTask.WhenAll(getSpriteTask);

        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].sprite = sprites[i];
        }

        string apiText = await GetStringWebRequestTimeOut(apiKey, this.GetCancellationTokenOnDestroy());
        Debug.Log("apitext: " + apiText);
    }

    private async UniTask<Sprite> GetImageFromWebRequest(string url, CancellationToken token)
    {
        var unityWebRequestTexture = await UnityWebRequestTexture.GetTexture(url).SendWebRequest().WithCancellation(token);

        Texture2D texture = ((DownloadHandlerTexture)unityWebRequestTexture.downloadHandler).texture;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        return sprite;
    }

    private async UniTask<string> GetStringWebRequest(string url, CancellationToken token)
    {
        var op = await UnityWebRequest.Get(url).SendWebRequest().WithCancellation(token);
        return op.downloadHandler.text;
    }

    private async UniTask<string> GetStringWebRequestTimeOut(string url, CancellationToken token)
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfterSlim(3000);

        try
        {
            var op = await UnityWebRequest.Get(url).SendWebRequest().WithCancellation(token);
            return op.downloadHandler.text;
        }
        catch (OperationCanceledException ex)
        {
            if(ex.CancellationToken == cts.Token)
            {
                Debug.Log("TIME OUT!!");
                return "time out !!";
            }
            return "null";
        }
    }
}
