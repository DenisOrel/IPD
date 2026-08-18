
// Type: Intermech.Checksums.ChecksumOperationType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Checksums
{
    /// <summary>Типы операций при вычислении контрольной суммы</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Checksums_ChecksumOperationType")]
    [Category("Misc")]
    public enum ChecksumOperationType
    {
      /// <summary>
      /// 
      /// </summary>
      [CustomDescription("String.Empty")] Idle,
      /// <summary>Подготовка к вычислению контрольной суммы</summary>
      [CustomDescription("Checksums_Preparing")] Preparing,
      /// <summary>Вычисление контрольной суммы</summary>
      [CustomDescription("Checksums_Calculating")] Calculating,
      /// <summary>Вычисление контрольной суммы завершено</summary>
      [CustomDescription("Checksums_Finished")] Finished,
      /// <summary>Ошибка вычисления контрольной суммы</summary>
      [CustomDescription("Checksums_Error")] Error,
    }
}
