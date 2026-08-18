// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SubstringStartFinishType
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Выбор начала подстроки</summary>
public enum SubstringStartFinishType
{
  /// <summary> Неопределённое значение </summary>
  Unknow,
  /// <summary> Начала / окончания стороки </summary>
  FinishStart,
  /// <summary> Позиции № </summary>
  FromNPosition,
  /// <summary> Подстроки № </summary>
  FromNFoundSubstring,
  /// <summary> Подстроки c конца № </summary>
  FromEndFoundNSubstring,
}
