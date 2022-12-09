using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTThrowBomb : BTBaseNode
{
    private GameObject bomb;
    private GameObject player;
    private float bombTimer;

    public BTThrowBomb(GameObject _bomb, GameObject _player, float _bombTimer)
    {
        bomb = _bomb;
        player = _player;
        bombTimer = _bombTimer;
    }

    public override TaskStatus Run()
    {
        GameObject spawnedBomb = GameObject.Instantiate(bomb, player.transform);
        GameObject.Destroy(spawnedBomb, bombTimer);

        return TaskStatus.Success;
    }
}
