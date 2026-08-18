
// Type: Intermech.UI.Winforms.ProposedPage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.UI.Winforms
{
    /// <summary>Описывает результат выбора страницы мастера.</summary>
    public class ProposedPage
    {
      private IWizardPage page;
      private bool firstPage;
      private bool finishPage;

      public ProposedPage(IWizardPage page, bool firstPage, bool finishPage)
      {
        this.page = page;
        this.firstPage = firstPage;
        this.finishPage = finishPage;
      }

      /// <summary>Возвращает выбранную страницу мастера.</summary>
      public IWizardPage Page => this.page;

      /// <summary>Возвращает true, если это первая страница мастера.</summary>
      public bool FirstPage => this.firstPage;

      /// <summary>Возвращает true, если это последняя страница мастера.</summary>
      public bool FinishPage => this.finishPage;
    }
}
