using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISA.Dependency
{
    public class AddOrRemoveBusinessApplicationEventArgs : EventArgs
    {
        private BusinessApplication _Application = null;

        public AddOrRemoveBusinessApplicationEventArgs()
        {

        }

        public AddOrRemoveBusinessApplicationEventArgs(BusinessApplication Application)
        {
            _Application = Application;
        }

        public BusinessApplication Application
        {
            get
            {
                return _Application;
            }
            set
            {
                _Application = value;
            }
        }

    }
}
