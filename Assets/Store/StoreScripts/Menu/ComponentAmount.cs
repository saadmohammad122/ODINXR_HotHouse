using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Circuits
{
    public class ComponentAmount : MonoBehaviour
    {
        public int value = 1;
        public Text TextObject;
        public Text Description;

        public Image ComponentImage;
        public Sprite[] Images;
        public Text[] Descriptions;
        private int index;
        private int arraySize = 2;


        public void Init()
        {
            value = 1;
            index = 0;
            DisplayImage();
        }

        // Changes the image and description displayed based on
        //  current list index
        public void DisplayImage()
        {
            ComponentImage.sprite = Images[index];
            Description.text = Images[index].name;
        }

        // Increment and Decrement affect the number of items to be taken
        // Capped at 10
        public void Increment()
        {
            if (value < 10)
            {
                value++;
                TextObject.text = value.ToString();
            }
        }

        public void Decrement()
        {
            if (value > 1)
            {
                value--;
                TextObject.text = value.ToString();
            }
        }

        // Next and Prev take the index, update appropriately, and
        //  update the image
        public void Next()
        {
            if (index < arraySize - 1)
            {
                index++;
                DisplayImage();
            }
        }

        public void Prev()
        {
            if (index > 0)
            {
                index--;
                DisplayImage();
            }
        }

        public void Take()
        {
            Debug.Log("Take " + value + " " + Images[index].name + "s and place them into inventory");
        }
    }
}
