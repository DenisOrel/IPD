// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.IFoldersView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Интерфейс закладки, в которой навигация выполняется аналогично папкам в "Проводнике".
/// Закладка может указать, восстанавливать ли её как активную закладку при переходе между
/// уровнями дерева "Навигатора", либо включать стандартный механизм выбора активной закладки
/// </summary>
public interface IFoldersView : IView
{
  /// <summary>Если true, то оставлять закладку активной</summary>
  bool RemainActiveView { get; }
}
