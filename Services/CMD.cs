using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverRest.Models;

namespace DriverRest.Services
{
    public class CMD
    {
       
        public static byte[] CMD_(string command,string TextSTR)
        {
            TcomPaket paket;
            int len=0;
            byte[] vs = new byte[len];
            //Проверка соединения
            if (command == "0x01")
            {
                
            }
            //Установка яркости
            if (command == "0x02")
            {
                
                paket.Cmd = 0x02;
                paket.DataLen = 1;
                len = 1;
                vs[0] = Convert.ToByte(TextSTR);

                
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
            return vs;
        }
    }
}
