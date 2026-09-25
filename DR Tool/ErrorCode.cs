using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRPlanningTool
{
    public class ErrorCode
    {
        private string _Type = "";
        private string _Code = "";
        private string _Message = "";
        private string _Cause = "";
        private string _Resolution = "";

        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }

            set
            {
                _Code = value;
            }
        }

        public string Message
        {
            get
            {
                return _Message;
            }

            set
            {
                _Message = value;
            }
        }

        public string Cause
        {
            get
            {
                return _Cause;
            }

            set
            {
                _Cause = value;
            }
        }

        public string Resolution
        {
            get
            {
                return _Resolution;
            }

            set
            {
                _Resolution = value;
            }
        }
    }
}
