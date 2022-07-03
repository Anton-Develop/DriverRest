using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using DriverRest.Models;

namespace DriverRest.Services
{
    public class Data_Services

    {
       
       
        private static byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            byte[] rawdata = new byte[rawsize];
            GCHandle handle = GCHandle.Alloc(rawdata, GCHandleType.Pinned);
            Marshal.StructureToPtr(anything, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return rawdata;
        }

        public void GetArray(TcomPaket str)
        {

            
                int size = Marshal.SizeOf(str);
                byte[] arr = new byte[size];

                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(str, ptr, true);
                    Marshal.Copy(ptr, arr, 0, size);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }
              //  return arr;
                        
        }

        public static byte[] StructToBytes(TcomPaket myStruct)
        {
            int size = Marshal.SizeOf(myStruct);
            byte[] arr = new byte[size];

            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(myStruct, buffer, false);
            Marshal.Copy(buffer, arr, 0, size);
            Marshal.FreeHGlobal(buffer);

            return arr;
        }


        private static TcomPaket BytesToStruct(byte[] arr)
        {
            int size = Marshal.SizeOf(typeof(TcomPaket));

            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.Copy(arr, 0, buffer, size);
            var myStruct = (TcomPaket)Marshal.PtrToStructure(buffer, typeof(TcomPaket));
            Marshal.FreeHGlobal(buffer);

            return myStruct;
        }


        private static byte CRC8_131(byte[] buffcopy)
        {
            
           
            var poly = 0x131;                             //  задаём полином для 0, 4, 5 и 8 битов, как в описании  CRC X8 + X5 + X4 + 1
            int crc = 0;
            var buff =  buffcopy;
            for(int i=0;i<buff.Length;i++)
            {
                crc = crc ^ buff[i];
               
                for (int j=8;j>0;j--)                      //  по одному смещаем биты влево и проверяем четвёртый бит, если он поднят, то инвертируем биты по полиному.
                {
                    if ((crc & 0x80) == 0x80)
                    {
                        crc = ((crc << 1) ^ poly);
                    }
                    else                                  //  если 4-й бит не поднят, то просто перемещаем биты влево
                    {
                        crc = (crc << 1);
                    }
                    
                }
            }
            
            
            return (byte)crc; 
        }
        public static byte SUM(byte[] buffcopy)
        {
            int crc = 0;//Старшие 6 бит контрольной суммы считаются так: суммируются инвертированные байты и берутся младшие 6 бит суммы. 
            var buff = buffcopy;
            byte msg = 0;
            for (int i = 0; i < buff.Length; i++)
            {
                crc = crc + (~buff[i]);
                crc = crc & 0x3F;
                var CRC8 = CRC8_131(buffcopy);
                var BCRC8 = (crc << 8) | CRC8;
                msg = (byte)BCRC8;
            }

            return msg;
        }

        public void Cooding(byte[] buffcopy)
        {
            byte[] bufHL = new byte[3];
            byte[] lbuf = new byte[1];
            byte hbuf =0x7F;
            var buff = buffcopy;
            //  кодирование длины сообщения
            var lb0 = (buff.Length & 0x7F) | 0x80;
            var lb1 = ((buff.Length >> 7) & 0x7F) | 0x80;
            //   маркер начала пакета
            bufHL[0] = 0x02;
            bufHL[1] = (byte)lb0;
            bufHL[2] = (byte)lb1;

            for (int i = 0; i < buff.Length; i++)
            {
                // У каждого байта b инвертируется старший бит (операция b=b^0x80).
                lbuf[0] = (byte)(buff[i] ^ 0x80);
                // Если получившийся байт b больше или равен 0x20 и не равен 0x7F, то он остается без изменений (b).
                if (lbuf[0] >= 0x20 && lbuf[0] != 0x7F)
                {
                    //bufHL =Array concat([bufHL, lbuf], msg.bufHL.length+1);
                    bufHL = (byte)(Buffer.BlockCopy(bufHL, 0, lbuf, (byte)bufHL.Length + 1,14));  //Concat(bufHL, lbuf,bufHL.length + 1);

                }
                else
                {
                    if ((lbuf[0]) < 0x20 || lbuf[0] == 0x7F)
                    {
                        lbuf[0] = (byte)(lbuf[0]|(0x80));
                        // msg.bufHL =Buffer.concat([msg.bufHL,lbuf,hbuf], msg.bufHL.length+2);

                    }
                }
            }
            /// msg.bufHL=Buffer.concat([msg.bufHL,Buffer.from([(msg.BCRC8 & 0x7F)|0x80, ((msg.BCRC8>>7) & 0x7F) | 0x80, 0x03])], msg.bufHL.length+3);
            /// msg.payload = msg.bufHL;
            

        }
      
    }

       
}
