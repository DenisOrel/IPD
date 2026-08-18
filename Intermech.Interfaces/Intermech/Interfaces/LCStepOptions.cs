
// Type: Intermech.Interfaces.LCStepOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Опции шага ЖЦ</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("LCStepOptions")]
    [Category("Misc")]
    [Flags]
    public enum LCStepOptions
    {
      /// <summary>Нет опций</summary>
      [CustomDescription("Attribute.Interfaces_180")] None = 0,
      /// <summary>
      /// Запрет существования более одной версии объекта на данном шаге
      /// </summary>
      [CustomDescription("DisableParallelVersions")] DisableParallelVersions = 1,
      /// <summary>Фиксация базовой версии объекта</summary>
      [CustomDescription("BaseVersionStep")] BaseVersion = 2,
      /// <summary>Восстанавливать мягкую конкретизацию версий</summary>
      [CustomDescription("RestoreSoftInstantiation")] RestoreSoftInstantiation = 4,
      /// <summary>
      /// Запрет существования более одной контекстной версии объекта
      /// </summary>
      [CustomDescription("DisableContextParallelVersions")] DisableContextParallelVersions = 8,
    }
}
