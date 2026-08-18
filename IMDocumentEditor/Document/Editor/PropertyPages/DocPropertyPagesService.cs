// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.PropertyPages.DocPropertyPagesService
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Editor.PropertyPages;

internal class DocPropertyPagesService
{
  private List<IPropertyPage> _pages;
  private DocPropertyPagesForm _pagesForm;

  public event EventHandler Changed;

  public DocPropertyPagesService()
  {
    this._pages = new List<IPropertyPage>();
    this._pagesForm = new DocPropertyPagesForm(this);
  }

  public void AddPage(string path, IPropertyPage page)
  {
    this._pagesForm.AddPage(path, page);
    if (page == null)
      return;
    page.Changed += new EventHandler(this.Page_Changed);
    if (this._pages.Contains(page))
      return;
    this._pages.Add(page);
  }

  internal void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  internal void Apply()
  {
    foreach (IPropertyPage page in this._pages)
      page.Apply();
  }

  internal void Cancel()
  {
    foreach (IPropertyPage page in this._pages)
      page.Cancel();
  }

  internal void ShowDialog()
  {
    int num = (int) this._pagesForm.ShowDialog();
  }

  internal void ShowPages()
  {
  }

  private void Page_Changed(object sender, EventArgs e) => this.OnChanged();
}
