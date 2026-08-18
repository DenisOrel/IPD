// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.EditorMixHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Docking;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Editors;

internal static class EditorMixHelper
{
  internal static List<TableEditorMix> EditorsMix = new List<TableEditorMix>();

  internal static void Initialize(IServiceProvider container)
  {
    if (!(container.GetService(typeof (IContentProvider)) is IContentProvider service))
      return;
    service.ContentCallback += new GetContentCallback(EditorMixHelper.ContentProvider_ContentCallback);
  }

  private static DockControl ContentProvider_ContentCallback(Guid guid, string persistString)
  {
    long result;
    return guid.Equals(new Guid("E2E2DD9A-566F-48E2-94A0-53BD7A500CE1")) && long.TryParse(persistString, out result) && result != 0L ? (DockControl) EditorMixHelper.CreateEditor(result, -1L, -1) : (DockControl) null;
  }

  internal static TableEditorMix CreateEditor(long targetId, long parentID, int relationTypeID)
  {
    TableEditorMix editor = EditorMixHelper.FindEditor(targetId);
    if (editor == null)
    {
      editor = new TableEditorMix();
      DockManager service = ServicesManager.GetService(typeof (DockManager)) as DockManager;
      editor.Manager = service;
      editor.Initialize(targetId, parentID);
      EditorMixHelper.AddEditor(editor);
    }
    return editor;
  }

  private static void AddEditor(TableEditorMix editor) => EditorMixHelper.EditorsMix.Add(editor);

  private static TableEditorMix FindEditor(long tableId)
  {
    int count = EditorMixHelper.EditorsMix.Count;
    for (int index = 0; index < count; ++index)
    {
      if (EditorMixHelper.EditorsMix[index].TableMixId == tableId)
        return EditorMixHelper.EditorsMix[index];
    }
    return (TableEditorMix) null;
  }
}
