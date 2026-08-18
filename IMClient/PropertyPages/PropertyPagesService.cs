
// Type: IMClient.PropertyPages.PropertyPagesService




using Intermech.Interfaces.Client;
using System;
using System.Collections;


namespace IMClient.PropertyPages
{
    internal class PropertyPagesService : IPropertyPagesService
    {
      private ArrayList _pages;
      private IServiceProvider _services;
      private PropertyPagesForm _pagesForm;

      public event EventHandler Changed;

      public PropertyPagesService(IServiceProvider services)
      {
        this._pages = new ArrayList();
        this._services = services;
        this._pagesForm = new PropertyPagesForm(services, this);
      }

      public void AddPage(string path, IPropertyPage page)
      {
        this._pagesForm.AddPage(path, page);
        if (page == null)
          return;
        page.Changed += new EventHandler(this.Page_Changed);
        if (this._pages.Contains((object) page))
          return;
        this._pages.Add((object) page);
      }

      internal void OnChanged()
      {
        if (this.Changed == null)
          return;
        this.Changed((object) this, new EventArgs());
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

      internal void ShowDialog(string defaultPath = null)
      {
        int num = (int) this._pagesForm.ShowDialog(defaultPath);
      }

      internal void ShowPages()
      {
      }

      private void Page_Changed(object sender, EventArgs e) => this.OnChanged();
    }
}
