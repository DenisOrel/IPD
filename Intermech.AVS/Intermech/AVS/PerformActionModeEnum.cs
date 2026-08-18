// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.PerformActionModeEnum
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS;

/// <summary>Режимы выполнения действия</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum PerformActionModeEnum
{
  /// <summary>Никогда не выполнять</summary>
  [Description("Никогда")] Never,
  /// <summary>Прежде чем выполнить, спросить у пользователя</summary>
  [Description("Спросить пользователя")] AskUser,
  /// <summary>Выполнять всегда (автоматически)</summary>
  [Description("Автоматически")] Auto,
}
