using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Push.Adj;


public class AddForce : MonoBehaviour
{
    float pushspeed;
    Rigidbody rigi;
    Transform tr;
    Renderer ChRenderer;

    public int Size_Score;

    bool collide;
    bool poison;
    public bool texter;

    public int sinif;

    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
        tr = transform;
        texter = false;
        ChRenderer = GetComponent<Renderer>();
        Adjustment.adjEvent.ChAtStart(ChRenderer, this.transform);
        pushspeed = Adjustment.adjEvent.pushforce;
        Adjustment.adjEvent.ScoreTexter += scoreTexter;
        Adjustment.adjEvent.ScoreTexter += size;
        Adjustment.adjEvent.scoretext();



    }

    private void OnDestroy()
    {
        Adjustment.adjEvent.ScoreTexter -= scoreTexter;
        Adjustment.adjEvent.ScoreTexter -= size;

    }
    void size()
    {
        float temp = Size_Score / Adjustment.adjEvent.size;

        if (temp < 0.5f)
            temp = 0.5f;
        if (temp > 2f)
            temp = 2f;
        transform.localScale = new Vector3(temp, temp, temp);
    }


    void scoreTexter()
    {
        text.text = Size_Score.ToString();
    }


    bool ustunlukboolean(int sinif, int enemysinif)
    {
        bool dondur = false;

        if (sinif == 0 && enemysinif == 1)
            dondur = true;

        if (sinif == 1 && enemysinif == 2)
            dondur = true;

        if (sinif == 2 && enemysinif == 0)
            dondur = true;

        return dondur;

    }


    void onCollide(Collider voidcol)
    {
        AddForce enemyscore = voidcol.transform.GetComponent<AddForce>();



        if (enemyscore.Size_Score <= 25f)
        {
            Destroy(enemyscore.gameObject);
        }
        if (Size_Score > enemyscore.Size_Score || ustunlukboolean(sinif, enemyscore.sinif))
        {
            float incDec = (enemyscore.Size_Score * 50) / 100;
            Size_Score += (int)incDec;
            enemyscore.Size_Score -= (int)incDec;
        }




        Adjustment.adjEvent.scoretext();


        Vector3 pos = transform.position;
        Vector3 playerpos = voidcol.transform.position;
        Vector3 dir = pos - playerpos;
        Vector3 ittir = new Vector3(dir.x * pushspeed, -1, dir.z * pushspeed);
        Rigidbody rb = voidcol.transform.GetComponent<Rigidbody>();
        rb.AddForce(-ittir, ForceMode.Impulse);

    }


    void pickup(string tag,GameObject delete)
    {
        int buyutucu = Random.Range(80, 160);
        Size_Score += buyutucu;
        Destroy(delete);

        switch (tag)
        {
            case "0":
                sinif = 0;
                GetComponent<Renderer>().material = Adjustment.adjEvent.mats[sinif];
                break;
            case "1":

                sinif = 1;
                GetComponent<Renderer>().material = Adjustment.adjEvent.mats[sinif];
                break;
            case "2":

                sinif = 2;
                GetComponent<Renderer>().material = Adjustment.adjEvent.mats[sinif];
                break;

            default:
                break;

        }
        Adjustment.adjEvent.scoretext();


    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            onCollide(collision.collider);
            collide = true;
            texter = true;

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            poison = other.CompareTag("PoisonCloud") ? true : poison;
            texter = true;
        }
        pickup(other.gameObject.tag,other.gameObject);

    }
    private void OnTriggerStay(Collider other)
    {
        Adjustment.adjEvent.scoretext();
    }
    private void OnCollisionExit(Collision collision)
    {
        collide = false;
    }
    private void OnTriggerExit(Collider other)
    {
        poison = false;
    }


}

/* if (collision.gameObject.name != "player") return;
*/

