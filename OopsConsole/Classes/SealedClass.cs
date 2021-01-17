using System;
using System.Collections.Generic;
using System.Text;

namespace OopsConsole.Classes
{
    public sealed class SealedClass
    {
        private readonly Random _random;

        public SealedClass()
        {
            _random = new Random();
            Console.WriteLine("SealedClass Constructor Called !!");
        }

        public void DisplayRandomNumber()
        {
            Console.WriteLine(_random.Next());
        }
    }
}
