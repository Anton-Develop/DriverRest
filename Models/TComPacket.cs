using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DriverRest.Models
{
      
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        [Serializable]
        public  struct TcomPaket
        {

            public uint SrcAddr;
            public uint DstAddr;
            public byte PId;
            public byte Cmd;
            public byte Status;
            public uint DataLen;
            public byte[] Data;
        };
       
      

    
}
