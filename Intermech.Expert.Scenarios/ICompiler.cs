// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.ICompiler
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using System.Reflection;

#nullable disable
namespace Intermech.Expert.Scenarios;

internal interface ICompiler
{
  /// <summary>Компиляция сборки</summary>
  /// <param name="code">Код</param>
  /// <param name="references">Список библиотек</param>
  /// <param name="name">Имя новой сборки (без пути)</param>
  /// <returns></returns>
  Assembly Compile(string code, string[] references, string name);
}
