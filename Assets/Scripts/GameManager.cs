using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (EnemyController.Instance == null)
        {
            Instantiate(Utilities.Instance.EnemyPrefab);
        }
    }
}
