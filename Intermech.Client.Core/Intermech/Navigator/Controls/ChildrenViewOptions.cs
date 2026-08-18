
// Type: Intermech.Navigator.Controls.ChildrenViewOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

/// <summary>Флажки настроек закладки</summary>
[Flags]
public enum ChildrenViewOptions
{
  /// <summary>Разрешить настройку вида</summary>
  ShowSetColumnsCommand = 1,
  /// <summary>
  /// Запретить нодам операции с родительскими путями и данными
  /// (актуально для вьюшек, в которых располагаются разнородные узлы,
  /// обладающие разными путями, например, развёрнутый состав или входимость)
  /// </summary>
  DisablePathProcessing = 2,
}
