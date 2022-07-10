using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DriverRest.Models;

namespace DriverRest.Services
{
    public class DataTransformer
    {
       
        public static byte[] Date_for_CMD(uint SrcAddr, uint DstAddr, byte PId,string command, byte Status,string TextSTR,string STRNum,string Color,string Align)
        {


            byte[] vs1 = Array.Empty<byte>();//  new byte[0];
            
            //Проверка соединения
            if (command == "0x01")
            {
                TcomPaket paket = new TcomPaket
                {
                    SrcAddr = (SrcAddr),
                    DstAddr = (DstAddr),
                    PId = PId,
                    Cmd = 0x01
                };
                vs1 = Data_Services.StructToByteArray<TcomPaket>(paket);
            }
            //Установка яркости
            if (command == "0x02")
            {

                TcomPaket paket=new TcomPaket();
                Console.WriteLine("Установка яркости");
                paket.SrcAddr = (SrcAddr);
                paket.DstAddr = (DstAddr);
                paket.PId = PId;
                paket.Cmd = 0x02;
                paket.Status = Status;
                paket.DataLen = 1;
                paket.Data = new byte[paket.DataLen];


                paket.Data[0] = Convert.ToByte(TextSTR);
                
                vs1 = Data_Services.StructToByteArray<TcomPaket>(paket);





            }
            //Установка часов табло Сторка должна быть через пробел!
            if (command == "0x03")
            {
                TcomPaket paket = new TcomPaket();
                Console.WriteLine("Установка часов");
                paket.SrcAddr = (SrcAddr);
                paket.DstAddr = (DstAddr);
                paket.PId = PId;
                paket.Cmd = 0x03;
                paket.Status = Status;
                paket.DataLen = 6;
                paket.Data = new byte[paket.DataLen];



                string[] words = TextSTR.Split(' ');
                paket.Data[0] = Convert.ToByte(words[0]);
                paket.Data[1] = Convert.ToByte(words[1]);
                paket.Data[2] = Convert.ToByte(words[2]);
                paket.Data[3] = Convert.ToByte(words[3]);
                paket.Data[4] = Convert.ToByte(words[4]);
                paket.Data[5] = Convert.ToByte(words[5]);
               
              
                vs1 = Data_Services.StructToByteArray<TcomPaket>(paket);

            }
            //Чтение часов 
            if (command == "0x04")
            {
                
            }
            //Вывод текста в текстовые зоны
            if (command == "0x05")
            {
                TcomPaket paket = new TcomPaket();
                
                Console.WriteLine("Вывод текста в текстовые зоны");
                paket.SrcAddr = (SrcAddr);
                paket.DstAddr = (DstAddr);
                paket.PId = PId;
                paket.Cmd = 0x05;
                paket.Status = Status;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding("windows-1251");
                byte[] array = encoding.GetBytes(TextSTR);
                
               
                paket.DataLen = (byte)(9+ array.Length);
                paket.Data = new byte[paket.DataLen];


                paket.Data[0] = 0;// number displey;
                paket.Data[1] = Convert.ToByte(STRNum);
                paket.Data[2] = Convert.ToByte(Align);
                paket.Data[3] = (byte)8;
                paket.Data[4] = 0;// speed text;
                paket.Data[5]= Convert.ToByte(Color);
                paket.Data[6] = 0;
                paket.Data[7] = 0;
                paket.Data[8] = 0;
               
                for (int i = 0; i < array.Length; i++)
                {
                    paket.Data[9+i] = array[i];
                   
                }
                
               
               
                vs1 = Data_Services.StructToByteArray<TcomPaket>(paket);
                

            }

            //Вывод текста в текстовые поля
            if (command == "0x1B")
            {
                TcomPaket paket = new TcomPaket();
                Console.WriteLine("Вывод текста в текстовые зоны");
                paket.SrcAddr = (SrcAddr);
                paket.DstAddr = (DstAddr);
                paket.PId = PId;
                paket.Cmd = 0x1B;
                paket.Status = Status;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding("windows-1251");
                byte[] array = encoding.GetBytes(TextSTR);
                paket.DataLen = (byte)(7 + array.Length);
                paket.Data = new byte[paket.DataLen];


                paket.Data[0] = 0;// number row;
                paket.Data[1] = 0;//number cells
                paket.Data[2] = Convert.ToByte(STRNum);
                paket.Data[3] = Convert.ToByte(Color);
                paket.Data[4] = Convert.ToByte(Align);
                paket.Data[5] = 0x80;
                paket.Data[6] = 0; //this const NOT USED!
                

                for (int i = 0; i < array.Length; i++)
                {

                    paket.Data[7 + i] = array[i];
                    


                }
               
                vs1 = Data_Services.StructToByteArray<TcomPaket>(paket);
            }
            //Вывод данных на семисегментные индикаторы по строкам и столбцам
            if (command == "0x0F")
            {
                TcomPaket pakets = new TcomPaket();
                Console.WriteLine("Вывод данных на семисегментные индикаторы по строкам и столбцам");

               
                pakets.SrcAddr= (SrcAddr);
                pakets.DstAddr = DstAddr;
                pakets.PId = PId;
                pakets.Status = Status;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding("windows-1251");
                byte[] array = encoding.GetBytes(TextSTR);
                pakets.DataLen = (byte)(9 + array.Length);
                pakets.Data = new byte[pakets.DataLen];
                pakets.Data[0] = 1; //Number row
                pakets.Data[1] = 2; //Number cells
                pakets.Data[2] = Convert.ToByte(Align);
                pakets.Data[3] = 8;
                pakets.Data[6] = 0; //Number effect;
                pakets.Data[7] = 0;
                for (int i=0; i<array.Length;i++)
                {
                    pakets.Data[9 + i] = array[i];
                }
                vs1 = Data_Services.StructToByteArray<TcomPaket>(pakets);


            }
            //Считывание данных (семисегментные индикаторы) по строкам и столбцам
            if (command == "0x10")
            {
               
            }
            //Установка значений счетчиков
            if (command == "0x53")
            {
                
            }
            //Считывание значений счетчиков
            if (command == "0x52")
            {
                
            }
            return vs1;
        }
    }
}
