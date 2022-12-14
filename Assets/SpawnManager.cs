using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ghostPrefab;
    public GameObject RockPrefab;
    public GameObject vergil;
    public SpikeController spikeController;
    public GameObject[]  Ground;

    int ghostAmount = 0;
    int initalGhost = 5;
    int ghostKilled = 0;
    int waveNo = 1;

    public UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        SpawnGhosts(initalGhost);
        Ground = GameObject.FindGameObjectsWithTag("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostAmount <= 0 && spikeController.InputsActive())
        {
            SpawnGhosts(initalGhost++);
            waveNo++;
            uIManager.UpdateWaveText(waveNo);

            if (waveNo % 3 == 0)
            {
                RockPrefab.transform.position = Ground[Random.Range(0, Ground.Length - 1)].transform.position;
                Instantiate(RockPrefab, RockPrefab.transform.position, RockPrefab.transform.rotation);
            }
        }

		if (waveNo > 10 && !GetComponent<AudioSource>().isPlaying)
		{
            vergil.SetActive(true);
            GetComponent<AudioSource>().Play();
        }

    }

    void SpawnGhosts(int amnt)
    {
		for (int i = 0; i < amnt; i++)
		{
            ghostAmount++;
            Instantiate(ghostPrefab, ghostPrefab.transform.position, ghostPrefab.transform.rotation);
		}
    }

    public void GhostDestroyed()
    {
        ghostAmount--;
        ghostKilled++;
        uIManager.UpdateScoreText(ghostKilled);

    }
}
