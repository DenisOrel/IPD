// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.DocTraceInfo
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Document.Model;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Элемент содержащий информацию о документе / комплекте</summary>
public class DocTraceInfo
{
  /// <summary>Текст / наименование элемента в дереве</summary>
  protected string _text = string.Empty;
  /// <summary>Содержимое документа</summary>
  protected ImDocument _doc;
  /// <summary>Информации с трассировкой</summary>
  protected XmlDocument _traceInfo;
  /// <summary>
  /// 
  /// </summary>
  protected string[] _report;
  /// <summary>Список дочерних элементов</summary>
  protected List<DocTraceInfo> _childItems;

  /// <summary>Конструктор</summary>
  /// <param name="text"></param>
  /// <param name="doc"></param>
  public DocTraceInfo(string text, ImDocument doc)
    : this(text, doc, (XmlDocument) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="text"></param>
  /// <param name="doc"></param>
  /// <param name="traceInfo"></param>
  public DocTraceInfo(string text, ImDocument doc, XmlDocument traceInfo)
    : this(text, doc, traceInfo, (string[]) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="text"></param>
  /// <param name="doc"></param>
  /// <param name="traceInfo"></param>
  /// <param name="report"></param>
  public DocTraceInfo(string text, ImDocument doc, XmlDocument traceInfo, string[] report)
  {
    this._text = text;
    this._doc = doc;
    this._traceInfo = traceInfo;
    this._report = report;
    this._childItems = new List<DocTraceInfo>();
  }

  /// <summary>Удаление мусора</summary>
  public virtual void ClearData()
  {
    this._text = string.Empty;
    this._report = (string[]) null;
    if (this._traceInfo != null)
    {
      this._traceInfo.RemoveAll();
      this._traceInfo = (XmlDocument) null;
    }
    if (this._doc != null)
    {
      this._doc.Dispose();
      this._doc = (ImDocument) null;
    }
    if (this._childItems == null)
      return;
    foreach (DocTraceInfo childItem in this._childItems)
      childItem?.ClearData();
    this._childItems.Clear();
    this._childItems = (List<DocTraceInfo>) null;
  }

  /// <summary>Текст / наименование элемента в дереве</summary>
  public virtual string Text
  {
    get => this._text;
    set => this._text = value;
  }

  /// <summary>Содержимое документа</summary>
  public virtual ImDocument Doc
  {
    get => this._doc;
    set => this._doc = value;
  }

  /// <summary>Информации с трассировкой</summary>
  public virtual XmlDocument TraceInfo
  {
    get => this._traceInfo;
    set => this._traceInfo = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public virtual string[] Report
  {
    get => this._report;
    set => this._report = value;
  }

  /// <summary>Список дочерних элементов</summary>
  public virtual List<DocTraceInfo> ChildItems => this._childItems;
}
