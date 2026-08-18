
// Type: Intermech.Redline.RedlineLayer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Diagnostics;


namespace Intermech.Redline;

/// <summary>Сведения о замечаниях</summary>
[DebuggerDisplay("{ParentID==0 ? \"\" : \"*\"} {UserID.Split(new char[] { '|' })[0]} {NameRemark} {StatusRemark}")]
public class RedlineLayer : IDisposable
{
  /// <summary>поле ФИО (Name + "|" + ID)</summary>
  public string UserID = "";
  /// <summary>название замечания</summary>
  public string NameRemark = "";
  /// <summary>Комментарий к действию</summary>
  public string Comment = "";
  /// <summary>дата изменения</summary>
  public DateTime Time = DateTime.Now;
  /// <summary>название бизнес-процесса</summary>
  public string NameBusiness = "";
  /// <summary>шага бизнес-процесса</summary>
  public string StepBusiness = "";
  /// <summary>Графа подписи </summary>
  public string Signature = "";
  /// <summary>Статус замечания</summary>
  public EStatusRemark StatusRemark = EStatusRemark.eInconsistent;
  /// <summary> редактирование замечания запрещено</summary>
  public bool LockRemark;
  /// <summary>индификатор объекта</summary>
  public ulong RedObjectID;
  /// <summary>индификатор родительского объекта</summary>
  public ulong ParentID;
  /// <summary>объект для работы с откатом Комментария(.Text)</summary>
  public MapText CommentText;
  /// <summary>объект для работы с откатом Графа подписи(.Text)</summary>
  public MapText SignatureText;

  /// <summary>создать объект для работы Комментария</summary>
  /// <returns>объект для работы Комментария</returns>
  public MapText CreateCommentText()
  {
    MapText mapText = new MapText();
    mapText.Selectable = false;
    mapText.AutoResizes = false;
    mapText.AutoRescales = false;
    mapText.Multiline = true;
    mapText.Visible = false;
    mapText.Text = this.Comment;
    MapText commentText = mapText;
    this.CommentText = mapText;
    return commentText;
  }

  /// <summary>создать объект для работы Графа подписи</summary>
  /// <returns>объект для работы Графа подписи</returns>
  public MapText CreateSignatureText()
  {
    MapText mapText = new MapText();
    mapText.Selectable = false;
    mapText.AutoResizes = false;
    mapText.AutoRescales = false;
    mapText.Multiline = false;
    mapText.Visible = false;
    mapText.Text = this.Signature;
    MapText signatureText = mapText;
    this.SignatureText = mapText;
    return signatureText;
  }

  /// <summary>удаление ссылок MapText</summary>
  public void ClearObject()
  {
    this.CommentText = (MapText) null;
    this.SignatureText = (MapText) null;
  }

  public RedlineLayer()
  {
    this.ClearObject();
    this.UndoManager = new MapUndoManager();
  }

  /// <summary>Undo для графики</summary>
  public MapUndoManager UndoManager { get; protected set; }

  public override string ToString() => this.UserID.Split('|')[0];

  public void Dispose()
  {
    this.CommentText?.Dispose();
    this.SignatureText?.Dispose();
  }
}
