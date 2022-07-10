using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DriverRest.Models
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    //[Serializable]
    public struct TcomPaket_Feedback
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
