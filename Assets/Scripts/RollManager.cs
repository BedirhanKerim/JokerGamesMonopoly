using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RollManager : Singleton<RollManager>
{
    [SerializeField] private GameObject dicePrefab;
    private int predeterminedNumber1 = 5;
    private int predeterminedNumber2 = 6;
    private int turnTimeDice1 = 9;
    private int turnTimeDice2 = 12;
    [SerializeField] private Transform diceSpawnPoint;
    private GameObject currentDice1;
    private GameObject currentDice2;

    private float rotationDuration1 = 1f;
    private float rotationDuration2 = 1f;

    private void Start()
    {
        UIManager.Instance.RollEvent += SpawnDice;
    }

    public void SpawnDice(int number1, int number2)
    {
        predeterminedNumber1 = number1;
        predeterminedNumber2 = number2;
        turnTimeDice1 = Random.Range(6, 14);
        turnTimeDice2 = Random.Range(6, 14);

        currentDice1 = Instantiate(dicePrefab, diceSpawnPoint.position, Quaternion.identity);
        currentDice2 = Instantiate(dicePrefab, diceSpawnPoint.position + Vector3.right * 0.75f, Quaternion.identity);

        SetDiceRotation(currentDice1, predeterminedNumber1, turnTimeDice1);
        SetDiceRotation(currentDice2, predeterminedNumber2, turnTimeDice2);

        Rotate90DegreesX();
    }

    private void SetDiceRotation(GameObject dice, int number, int turnCount)
    {
        Vector3 rotation = ShowResult(number, turnCount);
        dice.transform.rotation = Quaternion.Euler(rotation);
    }
    public void Rotate90DegreesX()
    {
        StartCoroutine(RotateOverTimeDice1());
        StartCoroutine(RotateOverTimeDice2());
    }

    private IEnumerator RotateOverTimeDice1()
    {
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration1)
        {
            float rotationAmount = turnTimeDice1 * 90 * (Time.deltaTime / rotationDuration1);
            currentDice1.transform.Rotate(rotationAmount, 0f, 0f, Space.World);
            elapsedTime += Time.deltaTime;
            Vector3 moveDirection = Vector3.forward;
            currentDice1.transform.position += moveDirection * turnTimeDice1 / 2 * Time.deltaTime;
            yield return null;
        }
        Destroy(currentDice1, 2f);
    }

    private IEnumerator RotateOverTimeDice2()
    {
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration2)
        {
            float rotationAmount = turnTimeDice2 * 90 * (Time.deltaTime / rotationDuration2);
            currentDice2.transform.Rotate(rotationAmount, 0f, 0f, Space.World);
            elapsedTime += Time.deltaTime;
            Vector3 moveDirection = Vector3.forward;
            currentDice2.transform.position += moveDirection * turnTimeDice2 / 2 * Time.deltaTime;
            yield return null;
        }
        Destroy(currentDice2, 2f);
    }


    private Vector3 ShowResult(int selectedNumber, int turnCount)
    {
        float baseRotation = selectedNumber switch
        {
            1 => 0f,
            2 => 180f,
            3 => 0f,
            4 => 270f,
            5 => 0f,
            6 => 90f,
            _ => 0f
        };
        float rotationZ = (selectedNumber == 3) ? 270f : (selectedNumber == 5) ? 90f : 0f;
        return new Vector3(baseRotation + turnCount * -90f, 0f, rotationZ);
    }
}