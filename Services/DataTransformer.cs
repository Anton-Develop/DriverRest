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
            
           
            byte[] vs1=new byte[0];
            
            //Проверка соединения
            if (command == "0x01")
            {
                
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
                
               
                int len = 1;
                byte[] vs = new byte[len];
                vs[0] = Convert.ToByte(TextSTR);
                paket.Data = (byte[])vs.Clone();
                vs1 = Data_Services.StructToBytes(paket);

               

               

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


                int len = 6;
                byte[] vs = new byte[len];

                string[] words = TextSTR.Split(' ');
                vs[0] = Convert.ToByte(words[0]);
                vs[1] = Convert.ToByte(words[1]);
                vs[2] = Convert.ToByte(words[2]);
                vs[3] = Convert.ToByte(words[3]);
                vs[4] = Convert.ToByte(words[4]);
                vs[5] = Convert.ToByte(words[5]);
               
                paket.Data = (byte[])vs.Clone();
                vs1 = Data_Services.StructToBytes(paket);

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
                

                int len = (byte)(9 + array.Length);
                byte[] vs = new byte[len];
                vs[0] = 0;// number displey;
                vs[1] = Convert.ToByte(STRNum);
                vs[2] = Convert.ToByte(Align);
                vs[3] = 0x80;
                vs[4] = 0;// speed text;
                vs[5]= Convert.ToByte(Color);
                vs[6] = 0;
                vs[7] = 0;
                vs[8] = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 9; j < vs.Length; j++)
                    {
                        vs[j] = array[i];
                    }

                }
                paket.Data = (byte[])vs.Clone();
                vs1 = Data_Services.StructToBytes(paket);
            }

            //Вывод текста в текстовые поля
            if (command == "0x1B")
            {
               
            }
            //Вывод данных на семисегментные индикаторы по строкам и столбцам
            if (command == "0x0F")
            {
               
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
