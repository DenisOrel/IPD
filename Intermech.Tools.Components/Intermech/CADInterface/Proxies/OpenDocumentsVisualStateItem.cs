// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.OpenDocumentsVisualStateItem
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.IO;
using Intermech.Runtime.ComInterop.Proxies;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Элемент состояния UI CAD-системы, обеспечивающий сохранение и восстановление списка открытых документов.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
internal sealed class OpenDocumentsVisualStateItem : ApplicationVisualStateItem<CADSystemProxy>
{
  private PathCollection savedOpenFiles;

  /// <summary>Создает объект.</summary>
  public OpenDocumentsVisualStateItem() => this.savedOpenFiles = new PathCollection();

  /// <summary>
  /// Заполняет элемент, сохраняя текущее состояние UI CAD-системы.
  /// </summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  protected override void DoSaveState(CADSystemProxy cadSystem)
  {
    base.DoSaveState(cadSystem);
    this.savedOpenFiles.Clear();
    ICollection<string> openFiles = cadSystem.GetOpenFiles(true);
    if (openFiles.Count == 0)
      return;
    this.savedOpenFiles.AddRange((IEnumerable<string>) openFiles);
  }

  /// <summary>
  /// Восстанавливает элемент, используя сохраненное состояние UI CAD-системы.
  /// </summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  protected override void DoRestoreState(CADSystemProxy cadSystem)
  {
    base.DoRestoreState(cadSystem);
    PathCollection pathCollection = new PathCollection((IEnumerable<string>) cadSystem.GetOpenFiles(true));
    if (pathCollection.Count != 0 && this.savedOpenFiles.Count != 0)
      CollectionUtils.RemoveAll<string>((IList<string>) pathCollection, new Predicate<string>(((OrderedList<string>) this.savedOpenFiles).Contains));
    if (pathCollection.Count != 0)
      cadSystem.CloseFiles((ICollection<string>) pathCollection);
    foreach (string savedOpenFile in (OrderedList<string>) this.savedOpenFiles)
      cadSystem.OpenDocument(savedOpenFile, true);
  }
}
