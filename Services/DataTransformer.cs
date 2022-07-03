﻿using System;
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
       
        public static byte[] Date_for_CMD(uint SrcAddr, uint DstAddr, byte PId,string command, byte Status,string TextSTR)
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

               // var BYUY = Data_Services.CRC8_131();

               

            }
            //Установка часов табло
            if (command == "0x03")
            {
                
            }
            //Чтение часов 
            if (command == "0x04")
            {
                
            }
            //Вывод текста в текстовые зоны
            if (command == "0x05")
            {
                

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
