
// Type: Intermech.Interfaces.IMemoWriter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс записи MEMO-полей</summary>
    public interface IMemoWriter
    {
      /// <summary>
      /// Открывает MEMO-поле для записи memoSize-символов. Метод возвращает true, если MEMO-поле открыто и ждет данные.
      /// </summary>
      /// <param name="memoSize">Сколько символов будет записано в поле</param>
      /// <returns>true, если MEMO-поле открыто и ждет данные</returns>
      bool OpenMemo(int memoSize);

      /// <summary>
      /// Дописывает блок символов в MEMO-поле. Если возвращает true, то запись MEMO-поля не завершена
      /// и ожидается продолжение данных, иначе записывает данные в MEMO-поле. Если data будет
      /// содержать данные более размера, указанного в OpenMemo, то будет выдано исключение.
      /// </summary>
      /// <param name="data">Записываемые данные</param>
      /// <returns>Если возвращает true, то запись MEMO-поля не завершена и ожидается продолжение данных</returns>
      bool WriteDataBlock(char[] data);

      /// <summary>Закрывает MEMO-поле и отменяет запись данных</summary>
      void CancelWrite();
    }
}
