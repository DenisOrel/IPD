// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImDocumentEnumerator
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс для промотки документов в комплектах</summary>
public class ImDocumentEnumerator : IEnumerator<ImDocumentData>, IDisposable, IEnumerator
{
  private ImDocumentData current;
  private ImDocumentData startDocument;
  private DocumentTreeNode documentsOwner;

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец страниц в пределах которой нужно проматывать страницы</param>
  public ImDocumentEnumerator(DocumentTreeNode owner)
  {
    this.documentsOwner = owner;
    this.startDocument = (ImDocumentData) null;
    this.current = (ImDocumentData) null;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец страниц в пределах которой нужно проматывать страницы</param>
  /// <param name="startPage">Страница на которую установится энумератор после первого MoveNext</param>
  public ImDocumentEnumerator(DocumentTreeNode owner, ImDocumentData startDocument)
  {
    this.documentsOwner = owner;
    this.startDocument = startDocument;
    this.current = (ImDocumentData) null;
  }

  /// <summary>Текущая страница</summary>
  public ImDocumentData Current
  {
    [DebuggerStepThrough] get => this.current;
  }

  /// <summary>Изменить текущее положение энумератора</summary>
  /// <param name="value">Новое текущее положение</param>
  public void SetCurrent(ImDocumentData value) => this.current = value;

  /// <summary>Освободить все ресурсы</summary>
  public void Dispose()
  {
    this.current = (ImDocumentData) null;
    this.documentsOwner = (DocumentTreeNode) null;
    this.startDocument = (ImDocumentData) null;
  }

  /// <summary>Текущий элемент</summary>
  object IEnumerator.Current
  {
    [DebuggerStepThrough] get => (object) this.current;
  }

  /// <summary>Перейти к следующему элементу</summary>
  /// <returns></returns>
  public bool MoveNext()
  {
    if (this.current == null)
    {
      this.current = this.startDocument == null ? DocumentsComplect.GetFirstDocument(this.documentsOwner) : this.startDocument;
      if (this.current != null)
        return true;
    }
    else
    {
      if (this.current.Parent == null)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Interfaces.Document_162"));
      ImDocumentData nextDocument = DocumentsComplect.GetNextDocument(this.current.Parent, this.current.Index, false);
      if (nextDocument != null && nextDocument.IsChildForNode(this.documentsOwner, true))
      {
        this.current = nextDocument;
        return true;
      }
    }
    return false;
  }

  /// <summary>Сбросить энумератор в начальное состояние, т.е. до первой позиции</summary>
  public void Reset() => this.current = (ImDocumentData) null;
}
