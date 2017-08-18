using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceGrading
{
    public class LetterContainer
    {
        //Constructors
        public LetterContainer(char startCharacter, char endCharacter, _SelectionMode selectionMode)
        {
            for (int i = startCharacter; i < endCharacter; i++)
                this.AddLetter(new Letter(character: (char)i, isAnswer: false, isOption: false));
        }

        public LetterContainer(List<Letter> letters, _SelectionMode selectionMode)
        {
            //If the mode is single and the paramater has more than one answer, 
            //   then only select the first answer (alphabetically) and ignore the rest
            if (selectionMode.Equals(_SelectionMode.Single))
            {
                bool alreadyHasAnswer = false;
                foreach (Letter letter in letters)
                {
                    if (alreadyHasAnswer && letter.IsAnswer)
                    {
                        this.AddLetter(new Letter(character: letter.Character,
                                                  isAnswer: false,
                                                  isOption: letter.IsOption));
                        alreadyHasAnswer = true;
                    } 
                    else
                    {
                        this.AddLetter(letter);
                    }
                }
            }
            else    
                this.Letters = letters;
        }

        //Public Attributes
        public List<Letter> Letters { get; set; }
        public _SelectionMode SelectionMode { get; set; }

        //Public Methods
        public void AddLetter(Letter newLetter)
        {
            this.Letters.Add(newLetter);
        }

        public void DeleteLetter(Letter deleteLetter)
        {
            this.Letters.Remove(deleteLetter);
        }

        public void DeleteLetter(int index)
        {
            if (index >= 0 && index < this.Letters.Count)
                this.Letters.RemoveAt(index);
        }

        public void SelectLetterAsAnswer(Letter selectedLetter)
        {
            selectedLetter.IsAnswer = true;
            if (this.SelectionMode.Equals(_SelectionMode.Single))
                DeselectLettersAsAnswer(selectedLetter);
        }


        //Private Methods
        /// <summary>
        /// Deselects all Letters as answers
        /// </summary>
        private void DeselectLettersAsAnswer()
        {
            foreach (Letter letter in this.Letters)
                letter.IsAnswer = false;
        }

        /// <summary>
        /// Deselects all letters as answers except one
        /// </summary>
        /// <param name="excludedLetter">Letter to keep as answer</param>
        private void DeselectLettersAsAnswer(Letter excludedLetter)
        {
            foreach (Letter letter in this.Letters)
            {
                if (letter != excludedLetter)
                    letter.IsAnswer = false;
            }
        }
    }
}
