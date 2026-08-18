
// Type: Intermech.Interfaces.BlobValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс предназначен для хранения информации о значении двоичного атрибута + само значение
    /// </summary>
    [Serializable]
    public class BlobValue : ICloneable
    {
      /// <summary>Говорит о том, что индекс значению не присваивали</summary>
      public const int NoIndex = -1;

      /// <summary>Структура с информацией о файле/блобе</summary>
      public BlobInformation Header { get; private set; }

      /// <summary>Массив с телом файла/блоба.</summary>
      public byte[] Data { get; private set; }

      /// <summary>Индекс значение в списке значений атрибута (или -1)</summary>
      public int Index { get; set; }

      public BlobValue(BlobInformation header, byte[] data)
      {
        this.Header = header;
        this.Data = data;
        this.Index = -1;
      }

      public BlobValue(BlobInformation header, byte[] data, int index)
      {
        this.Header = header;
        this.Data = data;
        this.Index = index;
      }

      /// <summary>Создает пустое значение</summary>
      public BlobValue()
      {
        this.Header = BlobInformation.EmptyBlobInformation();
        this.Data = (byte[]) null;
      }

      /// <summary>true если блоб не содержит значения</summary>
      public bool IsEmpty => this.Data == null;

      public object Clone()
      {
        byte[] numArray = new byte[this.Data.Length];
        Array.Copy((Array) this.Data, (Array) numArray, this.Data.Length);
        return (object) new BlobValue(this.Header.Clone(), numArray, this.Index);
      }
    }
}
