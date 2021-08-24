using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRVideoCall.Model
{
    public class BaseModel
    {
        public long ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
