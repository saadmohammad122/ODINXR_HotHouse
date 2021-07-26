using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Circuits
{
    public class Questions_Container : MonoBehaviour
    {
        public enum Answers
        {
            A, B, C, D
        }

        public Answers answer;

        public string Question_A;
        public string Question_B;
        public string Question_C;
        public string Question_D;
    }
}
