using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverRest.Services
{
    public class CMD
    {
       
        public static byte CMD_(string command)
        {
            //Проверка соединения
            if (command == "0x01")
            {
                return 0x01;
            }
            //Установка яркости
            if (command == "0x02")
            {
                return 0x02;
            }
            //Установка часов табло
            if (command == "0x03")
            {
                return 0x03;
            }
            //Чтение часов 
            if (command == "0x04")
            {
                return 0x04;
            }
            //Вывод текста в текстовые зоны
            if (command == "0x05")
            {
                return 0x05;
            }

            //Вывод текста в текстовые поля
            if (command == "0x1B")
            {
                return 0x1B;
            }
            //Вывод данных на семисегментные индикаторы по строкам и столбцам
            if (command == "0x0F")
            {
                return 0x0F;
            }
            //Считывание данных (семисегментные индикаторы) по строкам и столбцам
            if (command == "0x10")
            {
                return 0x10;
            }
            //Установка значений счетчиков
            if (command == "0x53")
            {
                return 0x53;
            }
            //Считывание значений счетчиков
            if (command == "0x52")
            {
                return 0x52;
            }
            return Convert.ToByte(command);
        }
    }
}
