using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Debris : MonoBehaviour
{
    private bool firstEnabled = false;
    public float delayBeforeChangeLayer;

    public int numberSavedPositions;
    public float delaySave;
    public float timerReconstruction;
    public float delayBeforeReconstruction;

    private Vector3[] savedPositions;
    private Vector3 initialRot;

    void Start()
    {
        savedPositions = new Vector3[numberSavedPositions];
    }

    void Update()
    {
        if(gameObject.activeSelf == true && !firstEnabled)
        {
            firstEnabled = true;
            StartCoroutine(ChangeLayer());
            StartCoroutine(SavePositions());
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(Reconstruction());
        }
    }

    private IEnumerator SavePositions()
    {
        initialRot = transform.eulerAngles;        
        for (int i = 0; i < numberSavedPositions; i++)
        {
            savedPositions[i] = transform.position;
            yield return new WaitForSeconds(delaySave);
        }

        yield return new WaitForSeconds(delayBeforeReconstruction);
        StartCoroutine(Reconstruction());
    }

    public IEnumerator Reconstruction()
    {
        float travelStep = timerReconstruction / numberSavedPositions;
        transform.DORotate(initialRot, timerReconstruction);
        for (int i = numberSavedPositions -1; i >= 0; i--)
        {
            transform.DOMove(savedPositions[i], travelStep).SetEase(Ease.Linear);
            yield return new WaitForSeconds(travelStep);
        }
        transform.parent.parent.GetComponent<Building>().Reconstruct();
    }

    private IEnumerator ChangeLayer()
    {
        yield return new WaitForSeconds(delayBeforeChangeLayer);        
        gameObject.layer = 9;
    }
}
