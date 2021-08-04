using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* To use:
 * 1) Navigate to the component menu (Press P to open menu)
 * 2) Press buttons to navigate to which component desired and how many needed
 * 3) Click take to spawn components or return to cancel 
 */

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

        public GameObject SpawningContainer;
        public GameObject Resistor;
        public Transform Parent;


        public void Init()
        {
            value = 1;
            index = 0;
            TextObject.text = value.ToString();
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
            // We want as many components as values, so loop and spawn that many
            for (int i = 0; i < value; i++)
            {
                // Randomly get the position within the boundaries of the container
                //  so that the objects do not spawn on top of each other
                var position = new Vector3(
                        Random.Range(SpawningContainer.GetComponent<Collider>().bounds.min.x, SpawningContainer.GetComponent<Collider>().bounds.max.x),
                        Random.Range(SpawningContainer.GetComponent<Collider>().bounds.min.y, SpawningContainer.GetComponent<Collider>().bounds.max.y),
                        Random.Range(SpawningContainer.GetComponent<Collider>().bounds.min.z, SpawningContainer.GetComponent<Collider>().bounds.max.z)
                        );

                GameObject ResistorClone;
                ResistorClone = Instantiate(Resistor, position, Random.rotation);

                // Set the "Parent" of the transform to be the "Circuit" Empty GameObject
                ResistorClone.transform.SetParent(Parent);

                // Need different names, set names to be component and number spawned
                // If the name is already taken, then we're gonna increment our bounds until
                //  we come up with unique names.  This is good because this method allows
                //  for unique names to be found for all components.
                while (Parent.transform.Find(Images[index].name + (i+1)) != null) {
                    i++;
                    value++;
                }
                ResistorClone.transform.name = Images[index].name + (i + 1);
            }
        }
    }
}
