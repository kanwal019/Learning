using System;
using System.Collections.Generic;
using System.Text;

namespace OopsConsole.Implementation
{
    public static class SealedExtension
    {
        public static void DisplayRandomGuid(this Classes.SealedClass sealedObj)
        {
            Console.WriteLine(Guid.NewGuid());
        }
    }
}
