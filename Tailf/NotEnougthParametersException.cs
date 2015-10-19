using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tailf
{
    [global::System.Serializable]
    public class NotEnougthParametersException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public NotEnougthParametersException() { }
        public NotEnougthParametersException(string message) : base(message) { }
        public NotEnougthParametersException(string message, Exception inner) : base(message, inner) { }
        protected NotEnougthParametersException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
