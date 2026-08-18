
// Type: Intermech.Cache.Storages.OnDiskStorage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Cache.Storages
{
    /// <summary>
    /// Реализует хранилище данных элементов кэша на диске в сериализованном виде,
    /// поэтому помещаемые в кэш объекты должны быть сериализуемыми. Объем любого элемента,
    /// помещаемого в хранилище оценивается количеством байт его сериализованного представления.
    /// </summary>
    public class OnDiskStorage : IStorage, ILimitedStorage, IPackedStorage
    {
      private string cacheLocation;
      private long totalSpace;
      private long freeSpace;
      private IDictionary fileNames;
      private static readonly string directorySeparator = new string(Path.DirectorySeparatorChar, 1);
      private static readonly string altDirectorySeparator = new string(Path.AltDirectorySeparatorChar, 1);

      /// <summary>Создает хранилище элементов кэша.</summary>
      /// <param name="cacheLocation">Путь к каталогу, в котором будут храниться элементы кэша</param>
      /// <param name="totalSpace">Размер хранилища в байтах</param>
      public OnDiskStorage(string cacheLocation, long totalSpace)
      {
        if (cacheLocation == null)
          throw new ArgumentNullException(nameof (cacheLocation), Resources.GetString("E_CacheLocationIsNull"));
        if (!Directory.Exists(cacheLocation))
          throw new ArgumentException(Resources.GetString("E_CacheLocationIsNotExists"));
        if (totalSpace <= 0L)
          throw new ArgumentOutOfRangeException(nameof (totalSpace), Resources.GetString("E_StoreTotalSpace"));
        this.cacheLocation = cacheLocation;
        if (!this.cacheLocation.EndsWith(OnDiskStorage.directorySeparator) && !this.cacheLocation.EndsWith(OnDiskStorage.altDirectorySeparator))
          this.cacheLocation += Path.DirectorySeparatorChar.ToString();
        this.totalSpace = totalSpace;
        this.freeSpace = this.totalSpace;
        this.fileNames = (IDictionary) new HybridDictionary();
      }

      /// <summary>
      /// Возвращает true, если у хранилища включен режим ограничения объема.
      /// </summary>
      public bool LimitsEnabled => true;

      /// <summary>Возвращает объем хранилища.</summary>
      public long TotalSpace => this.totalSpace;

      /// <summary>Возвращает объем свободного пространства в хранилище.</summary>
      public long FreeSpace => this.freeSpace;

      /// <summary>
      /// Возвращает объем, который займет элемент после помещения в кэш.
      /// </summary>
      /// <param name="data">Элемент</param>
      /// <returns>Объем элемента</returns>
      public long EstimateSpace(object data) => ((Stream) data).Length;

      /// <summary>Добавляет элемент с указанным ключем в хранилище.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      public void Add(object key, object data)
      {
        Validator.CheckKey(key);
        Validator.CheckData(data);
        MemoryStream memoryStream = (MemoryStream) data;
        string path = this.cacheLocation + Guid.NewGuid().ToString();
        this.fileNames.Add(key, (object) path);
        using (Stream stream = (Stream) new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
          memoryStream.WriteTo(stream);
        this.freeSpace -= memoryStream.Length;
      }

      /// <summary>Удаляет из хранилища элемент с указанным ключем.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      public void Remove(object key)
      {
        Validator.CheckKey(key);
        string fileName = (string) this.fileNames[key];
        int length = (int) new FileInfo(fileName).Length;
        File.Delete(fileName);
        this.fileNames.Remove(key);
        this.freeSpace += (long) length;
      }

      /// <summary>Очищает хранилище, удаляя все элементы.</summary>
      public void Flush()
      {
        foreach (DictionaryEntry fileName in this.fileNames)
          File.Delete((string) fileName.Key);
        this.fileNames.Clear();
        this.freeSpace = this.totalSpace;
      }

      /// <summary>
      /// Возвращает из хранилища элемент с указанным ключем. Если элемента с указанным ключем
      /// нет в хранилище, то результатом будет null.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <returns>Элемент</returns>
      public object GetData(object key)
      {
        Validator.CheckKey(key);
        using (FileStream fileStream = new FileStream((string) this.fileNames[key], FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
          MemoryStream data = new MemoryStream((int) fileStream.Length);
          data.SetLength(fileStream.Length);
          fileStream.Read(data.GetBuffer(), 0, (int) fileStream.Length);
          return (object) data;
        }
      }

      /// <summary>
      /// Упаковывает исходный элемент в объект, пригодный для
      /// помещения в хранилище.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <returns>Упакованное представление элемента</returns>
      public object PackObject(object key, object data)
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
        MemoryStream serializationStream = memoryStream;
        object graph = data;
        binaryFormatter.Serialize((Stream) serializationStream, graph);
        return (object) memoryStream;
      }

      /// <summary>
      /// Восстанавливает элемент из упакованного после извлечения из хранилища.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="packedData">Упакованное представление элемента</param>
      /// <returns>Элемент</returns>
      public object UnpackObject(object key, object packedData)
      {
        return new BinaryFormatter().Deserialize((Stream) packedData);
      }
    }
}
