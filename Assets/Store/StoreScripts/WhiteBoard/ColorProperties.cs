using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Circuits
{
    public class ColorProperties : MonoBehaviour
    {
        private Button submission;
        public Color startColor;
        public Color endColor;

        public RaycastHit hit;

        public Camera FPCam;
        private readonly int maxDistance = 100;

        private bool highlighting;

        // Start is called before the first frame update
        void Awake()
        {
            submission = transform.Find("Footer").Find("Submission").GetComponent<Button>();
            highlighting = true;
        }


        // Update is called once per frame
        void Update()
        {
            HighlightableCheck();

            if (highlighting)
                Highlight();
            else
                submission.image.color = Color.white;
        }

        void Highlight()
        {
            submission.image.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
        }

        // This method is used to see if the raycast is looking at something
        //  that is highlightable.
        //  7/19/21 -Christian Bloemhof
        private void HighlightableCheck()
        {
            Ray mousePointer = FPCam.ScreenPointToRay(Input.mousePosition);

            int layerMask = 1 << 6;
            layerMask = ~layerMask;

            // We only want the submission button to highlight on a question slide
            bool questionSlide = transform.GetComponent<WhiteBoard>().QuestionSlide;

            if (Physics.Raycast(mousePointer, out hit, maxDistance, layerMask))
            {
                if (hit.collider.gameObject.tag == "Highlightable" && questionSlide)
                {
                    highlighting = true;
                }
                else
                {
                    highlighting = false;
                }
            }
            else
            {
                highlighting = false;
            }
        }
    }
}

