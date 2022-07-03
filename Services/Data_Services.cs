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


        public static byte CRC8_131(byte[] buffcopy)
        {
            
           
            var poly = 0x131;                             //  задаём полином для 0, 4, 5 и 8 битов, как в описании  CRC X8 + X5 + X4 + 1
            int crc = 0;
            var buff = buffcopy;
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
    }

       
}
