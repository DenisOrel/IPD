// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfViewerPreferences
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf
{
    public class PdfViewerPreferences : IPdfWrapper
    {
      private PdfCatalog m_catalog;
      private bool m_centerWindow;
      private PdfDictionary m_dictionary;
      private bool m_displayDocTitle;
      private bool m_fitWindow;
      private bool m_hideMenubar;
      private bool m_hideToolbar;
      private bool m_hideWindowUI;
      private PdfPageLayout m_pageLayout;
      private PdfPageMode m_pageMode;
      private PageScalingMode m_pageScaling;

      internal PdfViewerPreferences() => this.m_dictionary = new PdfDictionary();

      internal PdfViewerPreferences(PdfCatalog catalog)
      {
        this.m_dictionary = new PdfDictionary();
        this.m_catalog = catalog != null ? catalog : throw new ArgumentNullException(nameof (catalog));
      }

      public bool CenterWindow
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary = (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (CenterWindow)))
                this.m_centerWindow = bool.Parse((pdfDictionary[nameof (CenterWindow)] as PdfBoolean).Value.ToString());
            }
            else if (this.m_dictionary["ViewerPreferences"] is PdfDictionary)
            {
              PdfDictionary pdfDictionary = this.m_dictionary["ViewerPreferences"] as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (CenterWindow)))
                this.m_centerWindow = bool.Parse((pdfDictionary[nameof (CenterWindow)] as PdfBoolean).Value.ToString());
            }
          }
          return this.m_centerWindow;
        }
        set
        {
          this.m_centerWindow = value;
          this.m_dictionary = (PdfDictionary) this.m_catalog;
          if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
          {
            ((this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary).SetBoolean(nameof (CenterWindow), this.m_centerWindow);
          }
          else
          {
            if (!(this.m_dictionary["ViewerPreferences"] is PdfDictionary))
              return;
            (this.m_dictionary["ViewerPreferences"] as PdfDictionary).SetBoolean(nameof (CenterWindow), this.m_centerWindow);
          }
        }
      }

      public bool DisplayTitle
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary = (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary.ContainsKey("DisplayDocTitle"))
                this.m_displayDocTitle = bool.Parse((pdfDictionary["DisplayDocTitle"] as PdfBoolean).Value.ToString());
            }
            else if (this.m_dictionary["ViewerPreferences"] is PdfDictionary)
            {
              PdfDictionary pdfDictionary = this.m_dictionary["ViewerPreferences"] as PdfDictionary;
              if (pdfDictionary.ContainsKey("DisplayDocTitle"))
                this.m_displayDocTitle = bool.Parse((pdfDictionary["DisplayDocTitle"] as PdfBoolean).Value.ToString());
            }
          }
          return this.m_displayDocTitle;
        }
        set
        {
          this.m_displayDocTitle = value;
          this.m_dictionary = (PdfDictionary) this.m_catalog;
          if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
          {
            ((this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary).SetBoolean("DisplayDocTitle", this.m_displayDocTitle);
          }
          else
          {
            if (!(this.m_dictionary["ViewerPreferences"] is PdfDictionary))
              return;
            (this.m_dictionary["ViewerPreferences"] as PdfDictionary).SetBoolean("DisplayDocTitle", this.m_displayDocTitle);
          }
        }
      }

      public bool FitWindow
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary = (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (FitWindow)))
                this.m_fitWindow = bool.Parse((pdfDictionary[nameof (FitWindow)] as PdfBoolean).Value.ToString());
            }
            else if (this.m_dictionary["ViewerPreferences"] is PdfDictionary)
            {
              PdfDictionary pdfDictionary = this.m_dictionary["ViewerPreferences"] as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (FitWindow)))
                this.m_fitWindow = bool.Parse((pdfDictionary[nameof (FitWindow)] as PdfBoolean).Value.ToString());
            }
          }
          return this.m_fitWindow;
        }
        set
        {
          this.m_fitWindow = value;
          this.m_dictionary = (PdfDictionary) this.m_catalog;
          if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
          {
            ((this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary).SetBoolean(nameof (FitWindow), this.m_fitWindow);
          }
          else
          {
            if (!(this.m_dictionary["ViewerPreferences"] is PdfDictionary))
              return;
            (this.m_dictionary["ViewerPreferences"] as PdfDictionary).SetBoolean(nameof (FitWindow), this.m_fitWindow);
          }
        }
      }

      public bool HideMenubar
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary = (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (HideMenubar)))
                this.m_hideMenubar = bool.Parse((pdfDictionary[nameof (HideMenubar)] as PdfBoolean).Value.ToString());
            }
            else if (this.m_dictionary["ViewerPreferences"] is PdfDictionary)
            {
              PdfDictionary pdfDictionary = this.m_dictionary["ViewerPreferences"] as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (HideMenubar)))
                this.m_hideMenubar = bool.Parse((pdfDictionary[nameof (HideMenubar)] as PdfBoolean).Value.ToString());
            }
          }
          return this.m_hideMenubar;
        }
        set
        {
          this.m_hideMenubar = value;
          this.m_dictionary = (PdfDictionary) this.m_catalog;
          if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
          {
            ((this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary).SetBoolean(nameof (HideMenubar), this.m_hideMenubar);
          }
          else
          {
            if (!(this.m_dictionary["ViewerPreferences"] is PdfDictionary))
              return;
            (this.m_dictionary["ViewerPreferences"] as PdfDictionary).SetBoolean(nameof (HideMenubar), this.m_hideMenubar);
          }
        }
      }

      public bool HideToolbar
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary = (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (HideToolbar)))
                this.m_hideToolbar = bool.Parse((pdfDictionary[nameof (HideToolbar)] as PdfBoolean).Value.ToString());
            }
            else if (this.m_dictionary["ViewerPreferences"] is PdfDictionary)
            {
              PdfDictionary pdfDictionary = this.m_dictionary["ViewerPreferences"] as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (HideToolbar)))
                this.m_hideToolbar = bool.Parse((pdfDictionary[nameof (HideToolbar)] as PdfBoolean).Value.ToString());
            }
          }
          return this.m_hideToolbar;
        }
        set
        {
          this.m_hideToolbar = value;
          this.m_dictionary = (PdfDictionary) this.m_catalog;
          if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
          {
            ((this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary).SetBoolean(nameof (HideToolbar), this.m_hideToolbar);
          }
          else
          {
            if (!(this.m_dictionary["ViewerPreferences"] is PdfDictionary))
              return;
            (this.m_dictionary["ViewerPreferences"] as PdfDictionary).SetBoolean(nameof (HideToolbar), this.m_hideToolbar);
          }
        }
      }

      public bool HideWindowUI
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary = (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (HideWindowUI)))
                this.m_hideWindowUI = bool.Parse((pdfDictionary[nameof (HideWindowUI)] as PdfBoolean).Value.ToString());
            }
            else if (this.m_dictionary["ViewerPreferences"] is PdfDictionary)
            {
              PdfDictionary pdfDictionary = this.m_dictionary["ViewerPreferences"] as PdfDictionary;
              if (pdfDictionary.ContainsKey(nameof (HideWindowUI)))
                this.m_hideWindowUI = bool.Parse((pdfDictionary[nameof (HideWindowUI)] as PdfBoolean).Value.ToString());
            }
          }
          return this.m_hideWindowUI;
        }
        set
        {
          this.m_hideWindowUI = value;
          this.m_dictionary = (PdfDictionary) this.m_catalog;
          if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
          {
            ((this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary).SetBoolean(nameof (HideWindowUI), this.m_hideWindowUI);
          }
          else
          {
            if (!(this.m_dictionary["ViewerPreferences"] is PdfDictionary))
              return;
            (this.m_dictionary["ViewerPreferences"] as PdfDictionary).SetBoolean(nameof (HideWindowUI), this.m_hideWindowUI);
          }
        }
      }

      public PdfPageLayout PageLayout
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if (this.m_dictionary.ContainsKey(nameof (PageLayout)))
              this.m_pageLayout = (PdfPageLayout) Enum.Parse(typeof (PdfPageLayout), (this.m_dictionary[nameof (PageLayout)] as PdfName).Value.ToString(), true);
          }
          return this.m_pageLayout;
        }
        set
        {
          this.m_pageLayout = value;
          PdfDictionary.SetName((PdfDictionary) this.m_catalog, nameof (PageLayout), this.m_pageLayout.ToString());
        }
      }

      public PdfPageMode PageMode
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if (this.m_dictionary[nameof (PageMode)] != null)
              this.m_pageMode = (PdfPageMode) Enum.Parse(typeof (PdfPageMode), (this.m_dictionary[nameof (PageMode)] as PdfName).Value, true);
          }
          return this.m_pageMode;
        }
        set
        {
          this.m_pageMode = value;
          PdfDictionary.SetName((PdfDictionary) this.m_catalog, nameof (PageMode), this.m_pageMode.ToString());
        }
      }

      public PageScalingMode PageScaling
      {
        get
        {
          if (this.m_catalog.LoadedDocument != null)
          {
            this.m_dictionary = (PdfDictionary) this.m_catalog;
            if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary = (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary.ContainsKey("PrintScaling"))
                this.m_pageScaling = (PageScalingMode) Enum.Parse(typeof (PageScalingMode), (pdfDictionary["PrintScaling"] as PdfName).Value.ToString(), true);
            }
            else if (this.m_dictionary["ViewerPreferences"] is PdfDictionary)
            {
              PdfDictionary pdfDictionary = this.m_dictionary["ViewerPreferences"] as PdfDictionary;
              if (pdfDictionary.ContainsKey("PrintScaling"))
                this.m_pageScaling = (PageScalingMode) Enum.Parse(typeof (PageScalingMode), (pdfDictionary["PrintScaling"] as PdfName).Value.ToString(), true);
            }
          }
          return this.m_pageScaling;
        }
        set
        {
          this.m_pageScaling = value;
          this.m_dictionary = (PdfDictionary) this.m_catalog;
          if ((object) (this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder) != null)
          {
            ((this.m_dictionary["ViewerPreferences"] as PdfReferenceHolder).Object as PdfDictionary).SetName("PrintScaling", this.m_pageScaling.ToString());
          }
          else
          {
            if (!(this.m_dictionary["ViewerPreferences"] is PdfDictionary))
              return;
            (this.m_dictionary["ViewerPreferences"] as PdfDictionary).SetName("PrintScaling", this.m_pageScaling.ToString());
          }
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
