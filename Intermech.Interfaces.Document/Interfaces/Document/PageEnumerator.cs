// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageEnumerator
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

/// <summary>Класс для перемещения по страницам в документах с проходом по сложной структуре комплектов и разделов</summary>
public class PageEnumerator : IEnumerator<PageData>, IDisposable, IEnumerator
{
  protected PageData current;
  private PageData startPage;
  protected DocumentTreeNode pagesOwner;

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец страниц в пределах которой нужно проматывать страницы</param>
  public PageEnumerator(DocumentTreeNode owner)
  {
    this.pagesOwner = owner;
    this.startPage = (PageData) null;
    this.current = (PageData) null;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец страниц в пределах которой нужно проматывать страницы</param>
  /// <param name="startPage">Страница на которую установится энумератор после первого MoveNext</param>
  public PageEnumerator(DocumentTreeNode owner, PageData startPage)
  {
    this.pagesOwner = owner;
    this.startPage = startPage;
    this.current = (PageData) null;
  }

  /// <summary>Текущая страница</summary>
  public PageData Current
  {
    [DebuggerStepThrough] get => this.current;
  }

  /// <summary>Изменить текущее положение энумератора</summary>
  /// <param name="value">Новое текущее положение</param>
  public void SetCurrent(PageData value) => this.current = value;

  /// <summary>Освободить все ресурсы</summary>
  public void Dispose()
  {
    this.current = (PageData) null;
    this.pagesOwner = (DocumentTreeNode) null;
    this.startPage = (PageData) null;
  }

  /// <summary>Текущий элемент</summary>
  object IEnumerator.Current
  {
    [DebuggerStepThrough] get => (object) this.current;
  }

  /// <summary>Перейти к следующему элементу</summary>
  /// <returns></returns>
  public virtual bool MoveNext()
  {
    if (this.pagesOwner == null)
      return false;
    if (this.current == null)
    {
      this.current = this.startPage == null ? ImDocumentData.GetFirstPage(this.pagesOwner) : this.startPage;
      if (this.current != null)
        return true;
    }
    else
    {
      if (this.current.Parent == null)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Interfaces.Document_162"));
      PageData nextPage = ImDocumentData.GetNextPage(this.current.Parent, this.current.Index, false);
      if (nextPage != null && nextPage.IsChildForNode(this.pagesOwner, true))
      {
        this.current = nextPage;
        return true;
      }
    }
    return false;
  }

  /// <summary>Сбросить энумератор в начальное состояние, т.е. до первой позиции</summary>
  public void Reset() => this.current = (PageData) null;
}
