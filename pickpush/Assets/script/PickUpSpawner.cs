using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnobject;
    [SerializeField] float value;
    [SerializeField] Material[] matlar;
    

    void Start()
    {

        StartCoroutine("spawner");
    }
    IEnumerator spawner()
    {
        while (true)
        {
            Vector3 rand = Random.insideUnitSphere * value;
            int rando = Random.Range(0,matlar.Length);
            rand.y = 0;
            GameObject food = Instantiate(spawnobject, rand, Quaternion.identity);
            food.GetComponent<Renderer>().material = matlar[rando];
            food.tag = rando.ToString();
            yield return new WaitForSeconds(0.5f);
            Debug.Log("spawed");
        }
    }
}
