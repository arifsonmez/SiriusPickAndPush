using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push.Adj
{
    public class Adjustment : MonoBehaviour
    {
        public static Adjustment adjEvent;
        public Material[] mats;
        public int size;
        public float pushforce;

        void Awake()
        {
            adjEvent = this;
        }


        public event System.Action ScoreTexter;
        public void scoretext()
        {
            ScoreTexter?.Invoke();
        }



        public void ChAtStart(Renderer ChRenderer, Transform ChTransform)
        {
            int randomColor = Random.Range(0, mats.Length);
            Material mat = mats[randomColor];
            ChRenderer.material = mat;
            ChTransform.localScale = new Vector3(size, size, size) / size;
            AddForce adfc = ChTransform.GetComponent<AddForce>();
            adfc.sinif = randomColor;
            adfc.Size_Score = size;
        }
    }
}