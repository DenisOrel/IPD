
// Type: Intermech.Interfaces.BitsArrayConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Interfaces
{
    /// <summary>Свалка констант для класса BitsArray</summary>
    public static class BitsArrayConsts
    {
      /// <summary>
      /// BitsArray: неверно задано значение ёмкости битового массива: {0}
      /// </summary>
      public static readonly string Exception1 = LocalizationHolder.rm.GetString("Interfaces_60");
      /// <summary>
      /// BitsArray: неверно задан индекс бита: {0}, ёмкость массива: {1}
      /// </summary>
      public static readonly string Exception2 = LocalizationHolder.rm.GetString("Interfaces_61");
      /// <summary>
      /// BitsArray: запрошено извлечение {0} бит начиная с индекса {1], а ёмкость массива составляет {2} бит
      /// </summary>
      public static readonly string Exception3 = LocalizationHolder.rm.GetString("Interfaces_62");
      /// <summary>
      /// BitsArray: запрошена вставка {0} бит начиная с позиции {1], а ёмкость массива составляет {2} бит
      /// </summary>
      public static readonly string Exception4 = LocalizationHolder.rm.GetString("Interfaces_63");
      /// <summary>BitsArray: значение параметра bits равно null</summary>
      public static readonly string Exception5 = LocalizationHolder.rm.GetString("Interfaces_64");
    }
}
