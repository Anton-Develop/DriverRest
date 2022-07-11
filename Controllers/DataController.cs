using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using DriverRest.Models;
using DriverRest.Services;
using System.Diagnostics.Tracing;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Threading;
using System.Collections;
using System.Text.Json;
using System.Runtime.Serialization.Json;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using System.Reflection;

namespace DriverRest.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    
    public class DataController :  ControllerBase
    {
        protected IDictionary<string, object> Data;

        public static int Counter = 0;


        [HttpPost]
        [SwaggerOperation(Summary = "Creates the product data", Description = "Returns newly created productData")]
        [SwaggerResponse(200, "Everything worked and returns the newly created product")]
        [SwaggerResponse(400, "If there is any error while creating product")]

       

       public IActionResult POST ([FromBody] IEnumerable<GetInputData> contex)
        {

            if (contex == null)
            {
                return BadRequest();
            }
            try
            {
                Counter+=1;
                var CountNabor = contex.Count();
               
                if( CountNabor>0)
                {
                   for (int i=0; i<CountNabor; i++)
                    {

                        Data = ObjectToDictionaryHelper.ToDictionary(contex.ElementAtOrDefault(i));


                        switch (i)
                        {
                            case 0:

                                var SrcAddr = Data.ElementAtOrDefault(0).Value;
                                var DstAddr = Data.ElementAtOrDefault(1).Value;
                                var IP = Data.ElementAtOrDefault(2).Value.ToString();
                                var CMD = Data.ElementAtOrDefault(3).Value.ToString();
                                var strNum = Data.ElementAtOrDefault(4).Value.ToString();
                                var Color = Data.ElementAtOrDefault(5).Value.ToString();
                                var Alighn = Data.ElementAtOrDefault(6).Value.ToString();
                                var TextSTR = Data.ElementAtOrDefault(7).Value.ToString();
                                
                                
                                byte pid = (byte)Counter; 

                                byte status = 1;




                                Console.WriteLine("Count" + Counter);

                                var date = DataTransformer.Date_for_CMD(Convert.ToUInt32(SrcAddr), Convert.ToUInt32(DstAddr), pid,CMD,status, TextSTR,strNum,Color,Alighn);


                                var Word_Cooding = CRC.Coding(date);
                                for (int l=0; l<Word_Cooding.Length;l++)
                                {
                                    Console.Write(Word_Cooding[l]);
                                }
                                
                                byte[] Status_Feedback;

                                #region Call TCP Server
                                //TcpHelper _TcpServer = new TcpHelper();
                                //_TcpServer.StartServer(IP, 5023,Word_Cooding,out Status_Feedback);
                                //TcomPaket_Feedback tcomPaket_Feedback = Data_Services.BytesToStruct(Status_Feedback);

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
                Console.WriteLine("Post error:___"+ex);
            }
         //   TcomPaket_Feedback[] _Feedbacks;
          //  _Feedbacks = new TcomPaket_Feedback[1];
            return Ok("Пришла пора комаров кормить" );
            
            

            
        }
        
       
        

    }
}
