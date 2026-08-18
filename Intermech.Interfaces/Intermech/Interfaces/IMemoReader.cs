
// Type: Intermech.Interfaces.IMemoReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс чтения MEMO-атрибутов на клиенте</summary>
    public interface IMemoReader
    {
      /// <summary>
      /// Открывает MEMO-поле и возвращает его размер в символах. dataBlockSize - размер блоков,
      /// которые он будет отдавать на клиента при чтении (в символах).
      /// Если dataBlockSize=0, то размер блока равен размеру реально хранимых данных.
      /// </summary>
      /// <param name="dataBlockSize">Размер читаемого блока данных</param>
      /// <returns>Размер поля</returns>
      int OpenMemo(int dataBlockSize);

      /// <summary>
      /// Читает следующий блок данных размером dataBlockSize. Если dataBlockSize==0,
      /// то размер блока берется заданный в OpenMemo. Если возвращенный блок имеет длину 0
      /// (или меньше dataBlockSize), то данные закончились и мемо-поле закрыто.
      /// </summary>
      /// <param name="dataBlockSize">Размер читаемого блока данных</param>
      /// <returns>Результаты чтения</returns>
      char[] ReadDataBlock(int dataBlockSize);

      /// <summary>Читает оставшиеся данные</summary>
      /// <returns>Результаты чтения</returns>
      char[] ReadDataBlock();

      /// <summary>Закрывает MEMO-поле и высвобождает занятые ресурсы.</summary>
      void CloseMemo();
    }
}
