using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
    public class StringArgs : EventArgs
    {
        private readonly string args;

        public StringArgs(string args)
        {
            this.args = args;
        }

        public string Args
        {
            get { return args; }
        }
    }
}
