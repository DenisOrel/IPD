using Intermech.ComparisonPlugins.PDFComparison.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Intermech.ComparisonPlugins.PDFComparison.Common
{
    public class PageList
    {
      private SamplePage _currentPage = SamplePage.Empty;

      public event EventHandler PageUpdated;

      private List<SamplePage> pages { get; }

      public SamplePage CurrentPage
      {
        get => this._currentPage;
        private set
        {
          this._currentPage = value;
          EventHandler pageUpdated = this.PageUpdated;
          if (pageUpdated == null)
            return;
          pageUpdated((object) null, EventArgs.Empty);
        }
      }

      public PageList() => this.pages = new List<SamplePage>();

      private void LoadPages(List<SamplePage> pages)
      {
        if (pages.Count <= 0)
          return;
        this.pages.ForEach((Action<SamplePage>) (page => page.Dispose()));
        this.pages.Clear();
        this.pages.AddRange((IEnumerable<SamplePage>) pages);
        this.CurrentPage = this.pages[0];
      }

      public void LoadFile(FileDescription comparedFile)
      {
        if (comparedFile.FileData.Length == 0)
          return;
        this.FileName = comparedFile.Caption;
        List<Image> images = PDFReader.ExtractImages(comparedFile.FileData);
        int pageNumber = 0;
        Func<Image, SamplePage> selector = (Func<Image, SamplePage>) (image => new SamplePage(++pageNumber, image));
        this.LoadPages(images.Select<Image, SamplePage>(selector).ToList<SamplePage>());
      }

      public int Count => this.pages.Count;

      public string FileName { get; private set; }

      public void NextPage() => this.SetPage(this.CurrentPage.Number + 1);

      public void PrevPage() => this.SetPage(this.CurrentPage.Number - 1);

      public void SetPage(int number)
      {
        int index = this.pages.FindIndex((Predicate<SamplePage>) (p => p.Number == number));
        if (index < 0)
          return;
        this.CurrentPage = this.pages.ElementAt<SamplePage>(index);
      }
    }
}
