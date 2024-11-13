using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    private Vector3 targetPosition;
    public int currentTileNumber, maxTileNumber;
    public ParticleSystem lootParticle;
    void Start()
    {
    }

    private IEnumerator MoveObjectInSteps(int steps, float duration)
    {
        for (int i = 0; i < steps; i++)
        {
            if (currentTileNumber == maxTileNumber)
            {
                transform.position = new Vector3(0, 1, 0);
                currentTileNumber = 1;
            }
            else
            {
                targetPosition = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);

                yield return StartCoroutine(MoveOverTime(duration, targetPosition));
            }
        }

        MapManager.Instance.DetectReward(currentTileNumber);
    }

    private IEnumerator MoveOverTime(float duration, Vector3 target)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        float peakHeight = 1f;
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            float yOffset = peakHeight * (1 - Mathf.Pow(2 * progress - 1, 2));
            transform.position = Vector3.Lerp(startPosition, target, progress) + new Vector3(0, yOffset, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target + new Vector3(0, 0, 0);
        currentTileNumber++;
    }

    public void MoveOnTiles(int stepCount)
    {
        StartCoroutine(MoveObjectInSteps(stepCount, .5f));
    }
}