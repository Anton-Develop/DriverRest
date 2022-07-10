# DriverRest
Services->DataTransformer -> Формирование пакета в зависимости от команды
Services->CRC->Кодирование пакета
Services->Data_Services-> Дополнительные преобразования
отсальное TCP server и прочяя хрень
Model ->TcomPaket->OutputStructura
Model->GetInputData->INPUT CLASS
Model->TcomPaket_Feedback->Данные от табло к хосту


//////REST Client Config
POST /api/Data HTTP/1.1
Host: localhost:5001
content-type: application/json

[
 
  {
    "srcAddr": 456,
    "dstAddr": 0,
    "ip": "127.0.0.1",
    "CMD": "0x10",
    "strNum": "1",
    "color": "2",
    "alighn": "4",
    "textSTR": "Измнение текста "
  
}
 
]
