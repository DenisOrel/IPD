
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.LayerPresenter




using Intermech.ComparisonPlugins.PDFComparison.Common;
using System;
using System.Drawing;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    public class LayerPresenter : ILayerPresenter
    {
      public event EventHandler PageUpdated;

      public event EventHandler OnSelectObjectClick;

      public Image PageImage => this.pageList.CurrentPage.Image;

      public void LoadFile(FileDescription comparedFile) => this.pageList.LoadFile(comparedFile);

      private ILayerView view { get; }

      private PageList pageList { get; }

      public LayerPresenter(ILayerView view)
      {
        this.view = view;
        this.pageList = new PageList();
        this.pageList.PageUpdated += new EventHandler(this.PageList_PageUpdated);
        view.ClickOpenButton += new EventHandler(this.View_ClickOpenButton);
        view.ChangedPageNumber += new EventHandler(this.View_ChangedPageNumber);
        view.ClickNextPageButton += new EventHandler(this.View_ClickNextPageButton);
        view.ClickPrevPageButton += new EventHandler(this.View_ClickPrevPageButton);
      }

      private void View_ClickOpenButton(object sender, EventArgs e)
      {
        EventHandler selectObjectClick = this.OnSelectObjectClick;
        if (selectObjectClick == null)
          return;
        selectObjectClick((object) this, EventArgs.Empty);
      }

      private void View_ChangedPageNumber(object sender, EventArgs e)
      {
        this.pageList.SetPage(this.view.PageNumber);
      }

      private void View_ClickNextPageButton(object sender, EventArgs e) => this.pageList.NextPage();

      private void View_ClickPrevPageButton(object sender, EventArgs e) => this.pageList.PrevPage();

      private void PageList_PageUpdated(object sender, EventArgs e)
      {
        EventHandler pageUpdated = this.PageUpdated;
        if (pageUpdated != null)
          pageUpdated((object) null, EventArgs.Empty);
        this.view.UpdateUI(this.pageList.FileName, this.pageList.CurrentPage.Number, this.pageList.Count);
      }
    }
}
