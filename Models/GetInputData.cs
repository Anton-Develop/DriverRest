using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DriverRest.Models
{
    public class GetInputData
    {
        public UInt32 SrcAddr { get; set; }     
        public UInt32 DstAddr { get; set; }     
        public string IP { get; set; }       
        public string CMD { get; set; }
        public string strNum { get; set; }       
        public string Color { get; set; }   
        public string Alighn { get; set; }    
        public string TextSTR { get; set; } 
       


    }
   
    
}
