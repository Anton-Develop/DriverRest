using System;
using System.ComponentModel.DataAnnotations;

namespace DriverRest.Models
{
    public class AddInputData
    {
        /* Class for transition for Tablo*/
        public UInt32 SrcAddr { get; set; }     //адрес отправителя
        public UInt32 DstAddr { get; set; }     //адрес получателя
        public byte PId { get; set; }       //идентификатор пакета - инкрементируемое или случайное число,
        public byte Cmd { get; set; }       // код команды
        public byte Flags { get; set; }    // флаги-опции команды. В нормальном режиме равен 0.
        public byte Status { get; set; }    //статус выполнения команды
        public UInt32 DataLen { get; set; } //длина данных в поле Data, от 0 до 511
        public byte[] Data {get;set;}
    }
}
