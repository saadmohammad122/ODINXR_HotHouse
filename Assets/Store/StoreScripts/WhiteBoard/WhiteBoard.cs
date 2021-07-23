using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Circuits
{
    public class WhiteBoard : MonoBehaviour
    {
        public List<GameObject> Slides;

        private GameObject copy;

        private int size;
        private int slides_index;
        private int image_index;

        // Variable used to distunguish between what type of slide we are on
        // 1 - Image Slide; 2 - Question Slide;
        // Open to more slide types
        private int current_type;

        private TextMeshProUGUI selection;
        public TextMeshProUGUI answer;

        private bool questionSlide;
        public bool QuestionSlide
        {
            get
            {
                return questionSlide;
            }
        }

        private void Awake()
        {
            slides_index = 0;
            selection = null;

            copy = CheckNextSlideType();
        }

        private GameObject CheckNextSlideType() {
            GameObject currentSlide = Slides[slides_index];
            GameObject slides_copy = Instantiate(currentSlide, transform);
            // Should check for error

            image_index = 0;
            if (currentSlide.GetComponent<Slides_Container>() != null)
            {
                questionSlide = false;
                current_type = 1;
                slides_copy.GetComponent<Image>().sprite = slides_copy.GetComponent<Slides_Container>().PowerPoint[image_index];
                size = CountSlides(slides_copy);
            }
            else if (Slides[slides_index].GetComponent<Questions_Container>() != null)
            {
                questionSlide = true;
                current_type = 2;
                size = 0;
                EnterQuestionInformation(slides_copy);
            }

            return slides_copy;
        }

        // Count the actual number of slides inserted in the inspector
        // If there are null spaces between slides, move the slides together
        private int CountSlides(GameObject slides_copy)
        {
            int count = 0;
            List<Sprite> Slides = slides_copy.GetComponent<Slides_Container>().PowerPoint;

            for (int i = 0; i < Slides.Count; i++)
            {
                if (Slides[i] != null)
                {
                    // Copy the slide to the correct position
                    //  and clear it out of the incorrect position
                    if (Slides[count] == null){
                        Slides[count] = Slides[i];
                        Slides[i] = null;
                    }
                    count++;
                }
            }

            return count;
        }

        // Get the Next/Prev prefab from the slides list
        public void GetNextSlide()
        {
            Destroy(copy);
            slides_index++;
            copy = CheckNextSlideType();
        }

        private void GetPrevSlide()
        {
            Destroy(copy);
            slides_index--;
            copy = CheckNextSlideType();
            image_index = size - 1;
        }

        // Properly set up the questions and the listeners for the question slides
        public void EnterQuestionInformation(GameObject slides_copy)
        {
            TextMeshProUGUI text;
            // Set the anwswer for the question
            answer = slides_copy.transform
                .Find(slides_copy.GetComponent<Questions_Container>().answer.ToString())
                .Find("Letter").GetComponent<TextMeshProUGUI>();

            // Write out each question based on what the author put in the inspector
            text = slides_copy.transform.Find("A").Find("Question").GetComponent<TextMeshProUGUI>();
            text.text = slides_copy.GetComponent<Questions_Container>().Question_A;

            text = slides_copy.transform.Find("B").Find("Question").GetComponent<TextMeshProUGUI>();
            text.text = slides_copy.GetComponent<Questions_Container>().Question_B;

            text = slides_copy.transform.Find("C").Find("Question").GetComponent<TextMeshProUGUI>();
            text.text = slides_copy.GetComponent<Questions_Container>().Question_C;

            text = slides_copy.transform.Find("D").Find("Question").GetComponent<TextMeshProUGUI>();
            text.text = slides_copy.GetComponent<Questions_Container>().Question_D;

            // Create listener event for each button
            CreateListeners("A", slides_copy);
            CreateListeners("B", slides_copy);
            CreateListeners("C", slides_copy);
            CreateListeners("D", slides_copy);
        }

        // onClick events cannot be instantiated, so create these by grabbing the
        //  button in the scene and directly adding a listener to it of the desired
        //  function.
        private void CreateListeners(string character, GameObject slides_copy)
        {
            Button button = slides_copy.transform.Find(character).GetComponent<Button>();
            button.onClick.AddListener(() => {
                TextMeshProUGUI letter = slides_copy.transform.Find(character).Find("Letter").GetComponent<TextMeshProUGUI>();
                Select(letter, answer);
            });
        }

        // For Images in the "Slides" prefab
        public void NextSlide()
        {
            if (current_type == 1 && image_index < size - 1)
            {
                image_index++;
                copy.GetComponent<Image>().sprite = copy.GetComponent<Slides_Container>().PowerPoint[image_index];
            }
            else if (slides_index < Slides.Count - 1) {
                GetNextSlide();
            }
        }

        public void PrevSlide()
        {
            if (current_type == 1 && image_index > 0)
            {
                image_index--;
                copy.GetComponent<Image>().sprite = copy.GetComponent<Slides_Container>().PowerPoint[image_index];
            }
            else if (slides_index > 0)
            {
                GetPrevSlide();
                if (current_type == 1)
                    copy.GetComponent<Image>().sprite = copy.GetComponent<Slides_Container>().PowerPoint[image_index];
            }
        }

        // Sets the color for the question slide when user clicks on an answer
        public void Select(TextMeshProUGUI letter, TextMeshProUGUI answer)
        {
            if (selection != null)
                selection.color = Color.black;

            if (answer.color != Color.black)
                answer.color = Color.black;

            letter.color = Color.blue;
            selection = letter;
        }

        // Covers the large white submit button at the bottom center of the
        //  whiteboard.  Shows the solution and your answer.
        public void Submit()
        {
            if (selection == null)
            {
                return;
            }

            if (selection != answer)
            {
                selection.color = Color.red;
            }

            answer.color = Color.green;
        }
    }
}
