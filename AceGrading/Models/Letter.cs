using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceGrading
{
    public class Letter 
    {
        /// <summary>
        /// Instantiates the Letter class without a character value
        /// </summary>
        public Letter()
        {
            this.IsAnswer = false;
            this.IsOption = false;
        }

        /// <summary>
        /// Instantiates the Letter class with a character value
        /// </summary>
        /// <param name="character">The Letter of the Alphabet</param>
        public Letter(char character)
        {
            this.Character = character;
            this.IsAnswer = false;
            this.IsOption = false;
        }

        /// <summary>
        /// Fully instantiates the Letter class
        /// </summary>
        /// <param name="character">The Letter of the Alphabet</param>
        /// <param name="isAnswer">Is the/an answer for the question</param>
        /// <param name="isOption">Is an option for the question</param>
        public Letter(char character, bool isAnswer, bool isOption)
        {
            this.Character = character;
            this.IsAnswer = isAnswer;
            this.IsOption = isOption;
        }

        /// <summary>
        /// The letter of the alphabet
        /// </summary>
        public char Character { get; set; }

        /// <summary>
        /// Is the/an answer for the question
        /// </summary>
        public bool IsAnswer { get; set; }

        /// <summary>
        /// Is an option for the question
        /// </summary>
        public bool IsOption { get; set; }
    }
}
