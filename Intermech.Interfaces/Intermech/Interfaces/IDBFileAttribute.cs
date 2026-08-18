
// Type: Intermech.Interfaces.IDBFileAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс файловых атрибутов</summary>
    public interface IDBFileAttribute : IDBAttribute, IDBSessionable
    {
      /// <summary>
      /// Функция инициализирует атрибут файлами из объекта-прототипа номер prototypeID.
      /// Если prototypeID == 0, то ф-ция сама ищет объект-прототип. Если найдено более одного
      /// прототипа, то ф-ция возвращает массив идентификаторов объектов-прототипов и не производит
      /// инициализацию атрибута. Если не найдено ни одного прототипа, то ф-ция
      /// null. Если найден один прототип, то ф-ция возвращает массив с одним элементом, в котором
      /// находится ид. объекта-прототипа, и производит инициализацию атрибута файлами этого прототипа.
      /// </summary>
      /// <param name="prototypeID">Идентификатор объекта-прототипа</param>
      /// <returns>Массив идентификаторов объектов-прототипов</returns>
      long[] SetPrototype(long prototypeID);

      /// <summary>
      /// Функция переинициализирует имя файла значением, вычисленным по формуле атрибута-прототипа protoAttribute.
      /// </summary>
      /// <param name="protoAttribute">Атрибут-прототип</param>
      void SetDefaultFileName(IDBAttribute protoAttribute);

      /// <summary>
      /// Функция возвращает имя файла для данного атрибута, используя формулу вычисления имен файлов из настроек прототипов для данного типа объектов
      /// </summary>
      string GetNewFileName();

      /// <summary>Возвращает тип текущего файла</summary>
      FileTypes FileType { get; }

      /// <summary>Метод переименовывает текущий файл</summary>
      /// <param name="newFileName">Новое имя файла</param>
      void Rename(string newFileName);

      /// <summary>Возвращает массив с описателями значений атрибута</summary>
      /// <returns></returns>
      BlobInformation[] GetBlobInformation();
    }
}
