using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverRest.Services
{
    public class CRC
    {
        //метода расчета контрольной суммы CRC32 из массива байт.

       
        // Таблица значений CRC


        private static readonly byte[] CRCTable =
        {
            0,  94, 188, 226,  97,  63, 221, 131, 194, 156, 126,  32, 163, 253,  31,  65,
      157, 195,  33, 127, 252, 162,  64,  30,  95,   1, 227, 189,  62,  96, 130, 220,
       35, 125, 159, 193,  66,  28, 254, 160, 225, 191,  93,   3, 128, 222,  60,  98,
      190, 224,   2,  92, 223, 129,  99,  61, 124,  34, 192, 158,  29,  67, 161, 255,
       70,  24, 250, 164,  39, 121, 155, 197, 132, 218,  56, 102, 229, 187,  89,   7,
      219, 133, 103,  57, 186, 228,   6,  88,  25,  71, 165, 251, 120,  38, 196, 154,
      101,  59, 217, 135,   4,  90, 184, 230, 167, 249,  27,  69, 198, 152, 122,  36,
      248, 166,  68,  26, 153, 199,  37, 123,  58, 100, 134, 216,  91,   5, 231, 185,
      140, 210,  48, 110, 237, 179,  81,  15,  78,  16, 242, 172,  47, 113, 147, 205,
       17,  79, 173, 243, 112,  46, 204, 146, 211, 141, 111,  49, 178, 236,  14,  80,
      175, 241,  19,  77, 206, 144, 114,  44, 109,  51, 209, 143,  12,  82, 176, 238,
       50, 108, 142, 208,  83,  13, 239, 177, 240, 174,  76,  18, 145, 207,  45, 115,
      202, 148, 118,  40, 171, 245,  23,  73,   8,  86, 180, 234, 105,  55, 213, 139,
       87,   9, 235, 181,  54, 104, 138, 212, 149, 203,  41, 119, 244, 170,  72,  22,
      233, 183,  85,  11, 136, 214,  52, 106,  43, 117, 151, 201,  74,  20, 246, 168,
      116,  42, 200, 150,  21,  75, 169, 247, 182, 232,  10,  84, 215, 137, 107,  53

        };
        // Метод для соединения любого количества массивов byte[] в один
        private static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }
        /// <summary>
        /// Concatenates two or more arrays into a single one.
        /// </summary>
        public static T[] ConcatA<T>(params T[][] arrays)
        {
            // return (from array in arrays from arr in array select arr).ToArray();

            var result = new T[arrays.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < arrays.Length; x++)
            {
                arrays[x].CopyTo(result, offset);
                offset += arrays[x].Length;
            }
            return result;
        }

        // Метод для расчета контрольной суммы crc32
        public static byte[] Calculate(byte[] Value)
        {
            UInt32 CRCVal = 0xffffffff;
            for (int i = 0; i < Value.Length; i++)
            {
                CRCVal = (CRCVal >> 8) ^ CRCTable[(CRCVal & 0xff) ^ Value[i]];
            }
            CRCVal ^= 0xffffffff; // Toggle operation
            byte[] Result = new byte[4];

            Result[0] = (byte)(CRCVal >> 24);
            Result[1] = (byte)(CRCVal >> 16);
            Result[2] = (byte)(CRCVal >> 8);
            Result[3] = (byte)(CRCVal);

            return Result;
        }
        public static byte[] CRC8_131(byte[] Message)
        {
            //выдаваемый массив CRC
            byte[] CRC = new byte[256];
            ushort Register = 0; // создаем регистр, в котором будем сохранять высчитанный CRC
            ushort Polynom = 0x131; //Указываем полином,
            try
            {
                for (int i = 0; i < Message.Length; i++) // для каждого байта в принятом\отправляемом сообщении проводим следующие операции(байты сообщения без принятого CRC)
                {
                    Register = (ushort)CRCTable[(Register ^ Message[i])]; // Делим через XOR регистр на выбранный байт сообщения(от младшего к старшему)
                                                                          //Register = (ushort)(Register ^ Message[i]);
                    for (int j = 8; j < 0; j--) // для каждого бита в выбранном байте делим полученный регистр на полином
                    {
                        if ((ushort)(Register & 0x80) == 0x80) //если старший бит равен  то
                        {
                            Register = (ushort)(Register << 1); //сдвигаем на один бит влево
                            Register = (ushort)(Register ^ Polynom); //делим регистр на полином по XOR
                        }
                        else //если старший бит равен 0 то
                        {
                            Register = (ushort)(Register << 1); // сдвигаем регистр влево
                        }
                    }
                    CRC[i] = (byte)(Register); // присваеваем старший байт полученного регистра младшему байту результата CRC (CRClow)
                }
                // CRC[0] = (byte)(Register); // присваеваем старший байт полученного регистра младшему байту результата CRC (CRClow)
                //CRC[0] = (byte)(Register & 0x00FF); // присваеваем младший байт полученного регистра старшему байту результата CRC (CRCHi) это условность Modbus — обмен байтов местами.
            }
            catch(Exception ex)
            {
                Console.WriteLine("CRC8 error:____" + ex);
            }
            return CRC;
        }
        public static int SUM_CRC8_131(byte[] Message)
        {
            //byte[] CRC = new byte[1];
            ushort Register = 0; // создаем регистр, в котором будем сохранять высчитанный CRC
            for (int i = 0; i < Message.Length; i++)
            {
                Register = (ushort)(Register + (~Message[i])); //инвертируем и суммируем, как указано в писании
            }
                
            Register = (ushort)(Register & 0x3F);  //  убираем лишние старшие биты (так как нужны только 6 младших)
            Register= (ushort)(Register << 8);
            var CRC8 = BitConverter.ToInt32(CRC8_131(Message),0);
            var SUM = Register|CRC8;

            //  собираем контрольные суммы в однои слово, так, чтоб CRC8 было в младшем байте, 
            //  а 6-ти битовая в старшем байте. 6+8 = 14 бит

            return SUM;
        }
        public static byte[] Coding(byte[] buff)
        {
            byte[] bufHL = new byte[3];
            byte[] lbuf = new byte[1];
            byte[] hbuf = new byte[]{ 0x7F };
            //  кодирование длины сообщения
            var lb0 = (buff.Length & 0x7F) | 0x80;
            var lb1 = ((buff.Length >> 7) & 0x7F) | 0x80;
            bufHL[0] = 0x02;  //   маркер начала пакета
            bufHL[1] = (byte)lb0;       // Длина пакета (младшие 7 бит)
            bufHL[2] = (byte)lb1;       // Длина пакета (старшие 7 бит)

            for (int i = 0; i < buff.Length; i++)
            {
                lbuf[0] = (byte)(buff[i] ^ 0x80);                  // У каждого байта b инвертируется старший бит (операция b=b^0x80).
                if (lbuf[0] >= 0x20 && lbuf[0] != 0x7F)    // Если получившийся байт b больше или равен 0x20 и не равен 0x7F, то он остается без изменений (b).
                {
                    var LOW = bufHL.Length + 1;
                    var HIGHT = ConcatA(bufHL, lbuf);
                    bufHL = ConcatArrays(HIGHT, BitConverter.GetBytes(LOW));

                }
                else
                {
                    if (lbuf[0] < 0x20 || lbuf[0] == 0x7F)     //  Если получившийся байт меньше, чем 0x20 или равен 0x7F то он заменяется на 2 байта: 0x7F и (b | 0x80). 
                    {
                        lbuf[0] = (byte)(lbuf[0] | 0x80);
                        var HIGHT = ConcatA(bufHL, lbuf, hbuf);
                        var LOW = bufHL.Length + 2;
                        bufHL = ConcatArrays(HIGHT, BitConverter.GetBytes(LOW));
                    }
                }
            }
            var BCRC8 = SUM_CRC8_131(buff);
            var b1 = bufHL;
            var b2 = BitConverter.GetBytes((BCRC8 & 0x7F) | 0x80);
            var b3 = BitConverter.GetBytes(((BCRC8 >> 7) & 0x7F )| 0x80);
            var b4 = BitConverter.GetBytes(0x03);
            var b5 = BitConverter.GetBytes(bufHL.Length + 3);


            bufHL = ConcatA(b1,b2,b3,b4,b5);
            return bufHL;
        }
    }
}
