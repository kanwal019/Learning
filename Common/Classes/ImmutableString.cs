namespace Common.Classes
{
    public class ImmutableString
    {
        #region Constants     

        internal const int MaxCapacity = 100; // Maximum capacity of the characters initialized to 100
        
        #endregion

        #region Class Variables

        private int _actualSize = 0; // Actual length of the assigned string, initialized to 0
        private char[] _characters = new char[MaxCapacity]; // Character array denoting the string initialized to 100 characters

        #endregion

        #region Member Methods

        public ImmutableString Append(char c)
        {
            // If the actual size of the assigned character surpasses the memory allocated
            if (_actualSize == _characters.Length)
            {
                char[] chars = new char[2 * _actualSize];
                _characters.CopyTo(chars, 0);
                _characters = chars;
            }  
            _characters[_actualSize] = c;
            _actualSize++;
            return this;
        }

        public string ToString()
        {
            return new string(_characters, 0, _actualSize);
        }

        #endregion
    }
}