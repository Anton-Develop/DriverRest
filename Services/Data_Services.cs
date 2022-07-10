﻿using System;
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
    public static class Data_Services

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


        public static TcomPaket_Feedback BytesToStruct(byte[] arr)
        {
            int size = Marshal.SizeOf(typeof(TcomPaket_Feedback));

            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.Copy(arr, 0, buffer, size);
            var myStruct = (TcomPaket_Feedback)Marshal.PtrToStructure(buffer, typeof(TcomPaket_Feedback));
            Marshal.FreeHGlobal(buffer);

            return myStruct;
        }


        #region new
        public static byte[] StructToByteArray<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                FieldInfo[] infos = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (FieldInfo info in infos)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (MemoryStream inms = new MemoryStream())
                    {

                        bf.Serialize(inms, info.GetValue(obj));
                        byte[] ba = inms.ToArray();
                        // for length
                        ms.Write(BitConverter.GetBytes(ba.Length), 0, sizeof(int));

                        // for value
                        ms.Write(ba, 0, ba.Length);
                    }
                }

                return ms.ToArray();
            }
        }
        #endregion




    }


}
