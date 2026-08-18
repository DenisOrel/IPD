
// Type: Intermech.Data.DaoModel.DaoServiceList
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Intermech.Data.DaoModel
{
    /// <summary>
    /// Реализует коллекцию сервисов контекста. Она допускает модификацию только в случае закрытого контекста.
    /// </summary>
    public class DaoServiceList : Collection<DaoService>
    {
      private readonly DaoContext daoContext;

      public DaoServiceList(DaoContext daoContext)
      {
        this.daoContext = daoContext != null ? daoContext : throw new ArgumentNullException(nameof (daoContext));
      }

      private void CheckContextState() => this.daoContext.RequireClosed();

      protected override void ClearItems()
      {
        this.CheckContextState();
        foreach (DaoService daoService in (IEnumerable<DaoService>) this.Items)
          daoService.DaoContext = (DaoContext) null;
        base.ClearItems();
      }

      protected override void InsertItem(int index, DaoService item)
      {
        this.CheckNewItem(item);
        this.CheckContextState();
        base.InsertItem(index, item);
        item.DaoContext = this.daoContext;
      }

      protected override void RemoveItem(int index)
      {
        this.CheckContextState();
        DaoService daoService = this.Items[index];
        base.RemoveItem(index);
        daoService.DaoContext = (DaoContext) null;
      }

      protected override void SetItem(int index, DaoService item)
      {
        this.CheckNewItem(item);
        this.CheckContextState();
        base.SetItem(index, item);
        item.DaoContext = this.daoContext;
      }

      private void CheckNewItem(DaoService item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        if (item.DaoContext != null)
          throw new ArgumentException("Сервис уже принадлежит другому контексту.", nameof (item));
      }
    }
}
