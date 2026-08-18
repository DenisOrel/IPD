// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.OpenDocumentsVisualStateItem
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Intermech.Collections;
using Intermech.IO;
using Intermech.Runtime.ComInterop.Proxies;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Элемент состояния UI CAD-системы, обеспечивающий сохранение и восстановление списка открытых документов.
/// </summary>
/// <remarks>
/// <para>
/// Cохранению (и восстановлению) подлежат только документы с файлом на диске.
/// А все новые и еще не сохраненные на диск документы игнорируются.</para>
/// <para>
/// Реализация не является thread safe.</para>
/// </remarks>
internal sealed class OpenDocumentsVisualStateItem : ApplicationVisualStateItem<ICadProxy>
{
  private PathCollection savedOpenFiles;

  /// <summary>Создает объект.</summary>
  public OpenDocumentsVisualStateItem() => this.savedOpenFiles = new PathCollection();

  /// <summary>
  /// Заполняет элемент, сохраняя текущее состояние UI приложения.
  /// </summary>
  /// <param name="cadSystem">Объект приложения</param>
  protected override void DoSaveState(ICadProxy cadSystem)
  {
    base.DoSaveState(cadSystem);
    this.savedOpenFiles.Clear();
    foreach (ICadDocumentProxy openDocument in cadSystem.GetOpenDocuments(false))
      this.savedOpenFiles.Add(openDocument.GetMasterFile());
  }

  /// <summary>
  /// Восстанавливает элемент, используя сохраненное состояние UI приложения.
  /// </summary>
  /// <param name="cadSystem">Объект приложения</param>
  protected override void DoRestoreState(ICadProxy cadSystem)
  {
    base.DoRestoreState(cadSystem);
    List<ICadDocumentProxy> openDocuments = cadSystem.GetOpenDocuments(false);
    if (openDocuments.Count != 0 && this.savedOpenFiles.Count != 0)
      CollectionUtils.RemoveAll<ICadDocumentProxy>((IList<ICadDocumentProxy>) openDocuments, (Predicate<ICadDocumentProxy>) (x => this.savedOpenFiles.Contains(x.GetMasterFile())));
    if (openDocuments.Count != 0)
    {
      foreach (ICadDocumentProxy cadDocumentProxy in openDocuments)
        cadDocumentProxy.Close(true);
    }
    foreach (string savedOpenFile in (OrderedList<string>) this.savedOpenFiles)
      cadSystem.OpenDocument(savedOpenFile);
  }
}
