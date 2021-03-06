using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LoadImageOnUI : MonoBehaviour
{
    [SerializeField] private DownloadImage[] _cards;
    [SerializeField] private Transform[] _cardsTransform;

    public void loadImageOnUI()
    {
        switch (TypeDownload.typeDownload)

        {
            case 0:
                AllAtOnce();
                break;
            case 1:
                OneByOne();
                break;
            case 2:

                WhenImageReady();
                break;



        }
    }

    private void AllAtOnce()
    {
        var seq = DOTween.Sequence();
        seq.AppendInterval(1f);

        foreach (var cardTR in _cardsTransform)
            seq.Join(cardTR.DORotate(new Vector2(0, 180), 0.3f, RotateMode.Fast));
        seq.InsertCallback(0f, Complete);
        seq.AppendInterval(1f);

        foreach (var cardTR in _cardsTransform)
        {

            seq.Join(cardTR.DORotate(new Vector2(0, 0), 0.3f, RotateMode.Fast));

        }

    }
    private void Complete()
    {
        foreach (var card in _cards)
            card.DownloadPhoto();
    }

    private void OneByOne()
    {

        var seq = DOTween.Sequence();
        foreach (var cardTR in _cardsTransform)
            cardTR.DORotate(new Vector2(0, 180), 0.3f, RotateMode.Fast);
        foreach (var card in _cards)
            card.DownloadPhoto();
        seq.AppendInterval(1f);
        foreach (var cardTR in _cardsTransform)
        {

            seq.Append(cardTR.DORotate(new Vector2(0, 0), 0.3f, RotateMode.Fast));

        }

    }

    private void WhenImageReady()
    {
        foreach (var card in _cards)
            card.DownloadPhotoOnReady();

    }

}
