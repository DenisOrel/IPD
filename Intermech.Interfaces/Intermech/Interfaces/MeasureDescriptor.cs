
// Type: Intermech.Interfaces.MeasureDescriptor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Описатель единицы измерения</summary>
    [Serializable]
    public class MeasureDescriptor
    {
      /// <summary>Коэф. приведения к базовой величине. Для базовой величины == 1</summary>
      public double K;
      /// <summary>Ид. физической величины, которую измеряют данной единицей измерения</summary>
      public long PhysicalQuantityID;
      /// <summary>Наименование единицы измерения (килограмм)</summary>
      public string LongName;
      /// <summary>Краткое наименование единицы измерения (кг)</summary>
      public string ShortName;
      /// <summary>Используется ли данная единица измерения по умолчанию</summary>
      public bool IsDefault;
      /// <summary>ID объекта-описателя данной единицы измерения</summary>
      public long MeasureID;
      /// <summary>Нормализованные краткие наименования единицы измерения, включая дополнительные наименования (например, кг и kg)</summary>
      public string[] ShortNameIndex;
      /// <summary>Список операций над единицами измерения, в результате которых получается данная единица измерения</summary>
      public string[] OperationsList;
      /// <summary>Guid физической величины, которую измеряют данной единицей измерения</summary>
      public Guid PhysicalQuantityGuid;
      /// <summary>Гуид единицы измерения</summary>
      public Guid MeasureGuid;

      public MeasureDescriptor()
      {
      }

      /// <summary>Этот извращенный конструктор нужен тока для присвоения пустого значения данной структуре</summary>
      public MeasureDescriptor(bool empty)
      {
        this.MeasureID = 0L;
        this.IsDefault = false;
        this.ShortName = string.Empty;
        this.LongName = string.Empty;
        this.ShortNameIndex = new string[0];
        this.PhysicalQuantityID = 0L;
        this.K = 0.0;
        this.OperationsList = new string[0];
        this.PhysicalQuantityGuid = Guid.Empty;
        this.MeasureGuid = Guid.Empty;
      }

      public bool Empty => this.MeasureID == 0L;

      public override string ToString()
      {
        return !this.Empty ? $"{this.LongName} ({this.ShortName})" : string.Empty;
      }
    }
}
