using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using DriverRest.Models;
using DriverRest.Services;

using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Threading;
using System.Collections;
using System.Text.Json;
using System.Runtime.Serialization.Json;

namespace DriverRest.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DataController :  ControllerBase
    {
        private IDictionary<string, object> Data;
        private IDictionary<string, object> Data2;
        // private List<GetInputData> Data2; 

        private byte counter_pid;
        [HttpPost]
        [SwaggerOperation(Summary = "Creates the product data", Description = "Returns newly created productData")]
        [SwaggerResponse(200, "Everything worked and returns the newly created product")]
        [SwaggerResponse(400, "If there is any error while creating product")]

       

       public IActionResult POST ([FromBody] IEnumerable<GetInputData> contex)
        {
            counter_pid = (byte)(counter_pid + 1);
            if (contex == null)
            {
                return BadRequest();
            }
            try
            {

                var CountNabor = contex.Count();
               
                if( CountNabor>0)
                {
                   for (int i=0; i<CountNabor; i++)
                    {

                        Data = ObjectToDictionaryHelper.ToDictionary(contex.ElementAtOrDefault(i));
                       
                        
                        switch (i)
                        {
                            case 0:
                                
                                var  SrcAddr = Data.ElementAtOrDefault(0).Value;
                                var  DstAddr = Data.ElementAtOrDefault(1).Value;
                                var  IP      = Data.ElementAtOrDefault(2).Value;
                                var  CMD     = Data.ElementAtOrDefault(3).Value.ToString();
                                var  strNum  = Data.ElementAtOrDefault(4).Value;
                                var  Color   = Data.ElementAtOrDefault(5).Value;
                                var  Alighn  = Data.ElementAtOrDefault(6).Value;
                                var  TextSTR = Data.ElementAtOrDefault(7).Value.ToString();
                                byte pid = 0;
                                byte status = 1;
                               
                                
                                                                
                                var date = DataTransformer.Date_for_CMD(Convert.ToUInt32(SrcAddr), Convert.ToUInt32(DstAddr), pid,CMD,status, TextSTR);

                                var crc = Data_Services.SUM(date);

                                // var BYUY = Data_Services.CRC8_131();
                                Console.WriteLine("CRC8_131___"+crc);
                                //Console.WriteLine(date.Length);




                                #region Call TCP Server


                                #endregion
                                break;
                            case 1:
                                var SrcAddr1 = Data.ElementAtOrDefault(0).Value;
                                var DstAddr1 = Data.ElementAtOrDefault(1).Value;
                                var IP1 = Data.ElementAtOrDefault(2).Value;
                                var CMD1 = Data.ElementAtOrDefault(3).Value.ToString();
                                var strNum1 = Data.ElementAtOrDefault(4).Value;
                                var Color1 = Data.ElementAtOrDefault(5).Value;
                                var Alighn1 = Data.ElementAtOrDefault(6).Value;
                                var TextSTR1 = Data.ElementAtOrDefault(7).Value;

                             
                                break;
                            case 2:
                                var SrcAddr2 = Data.ElementAtOrDefault(0).Value;
                                var DstAddr2 = Data.ElementAtOrDefault(1).Value;
                                var IP2 = Data.ElementAtOrDefault(2).Value;
                                var CMD2 = Data.ElementAtOrDefault(3).Value;
                                var strNum2 = Data.ElementAtOrDefault(4).Value;
                                var Color2 = Data.ElementAtOrDefault(5).Value;
                                var Alighn2 = Data.ElementAtOrDefault(6).Value;
                                var TextSTR2 = Data.ElementAtOrDefault(7).Value;

                                break;

                               
                        }



                    }
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return Ok("Пришла пора комаров кормить");
            
            

            
        }
        
       
        

    }
}
