using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DriverRest.Models
{
      
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        //[Serializable]
        public  struct TcomPaket
        {

            public uint SrcAddr { get; set; }
            public uint DstAddr { get; set; }
            public byte PId { get; set; }
            public byte Cmd { get; set; }
            public byte Status { get; set; }
            public uint DataLen { get; set; }
            public byte[] Data { get; set; }
    };
       
      

    
}
