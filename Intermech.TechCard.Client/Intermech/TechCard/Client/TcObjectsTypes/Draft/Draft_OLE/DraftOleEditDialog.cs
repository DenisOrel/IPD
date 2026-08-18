// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE.DraftOleEditDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;

/// <summary>Диалог редактирования OLE эскизов</summary>
public static class DraftOleEditDialog
{
  /// <summary>Вызов диалога редактирования</summary>
  /// <param name="stream">Содержимое OLE объекта</param>
  /// <returns></returns>
  public static bool ShowModal(ref Stream stream)
  {
    return DraftOleEditDialog.ShowModal(ref stream, "", false);
  }

  /// <summary>Вызов диалога редактирования</summary>
  /// <param name="stream">Содержимое OLE объекта</param>
  /// <param name="draftName">Наименование эскиза</param>
  /// <param name="openEditor"></param>
  /// <param name="readOnly"></param>
  /// <returns></returns>
  public static bool ShowModal(
    ref Stream stream,
    string draftName,
    bool openEditor,
    bool readOnly = false)
  {
    using (DraftOleEditForm draftOleEditForm = new DraftOleEditForm())
    {
      draftOleEditForm.OleStream = stream;
      draftOleEditForm.DraftName = draftName;
      draftOleEditForm.NeedOpenEditor = openEditor;
      if (draftOleEditForm.ShowDialog() != DialogResult.OK)
        return false;
      if (!readOnly)
        stream = Stream.Synchronized(draftOleEditForm.OleStream);
      return true;
    }
  }

  /// <summary>Вызов диалога создания объекта</summary>
  /// <param name="draftName">Наименование эскиза</param>
  /// <param name="openEditor"></param>
  /// <param name="stream">Содержимое OLE объекта</param>
  /// <returns></returns>
  public static bool CreateOle(string draftName, bool openEditor, out Stream stream)
  {
    stream = (Stream) null;
    using (DraftOleEditForm draftOleEditForm = new DraftOleEditForm())
    {
      draftOleEditForm.DraftName = draftName;
      draftOleEditForm.NeedOpenEditor = openEditor;
      draftOleEditForm.NeedCreateObject = true;
      if (draftOleEditForm.ShowDialog() != DialogResult.OK)
        return false;
      stream = Stream.Synchronized(draftOleEditForm.OleStream);
      return true;
    }
  }
}
