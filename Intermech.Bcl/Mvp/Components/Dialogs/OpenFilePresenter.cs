
// Type: Intermech.Mvp.Components.Dialogs.OpenFilePresenter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Mvp.Components.Dialogs
{
    public sealed class OpenFilePresenter : SelectionDialogPresenter<IOpenFileView>
    {
      private string title;
      private string initialDirectory;
      private string fileName;
      private string defaultExt;
      private string extFilter;
      private bool allowMultiSelect;
      private List<string> selectedFiles = new List<string>(0);

      public string Title
      {
        get => this.title;
        set
        {
          this.CheckAllowPropertyChange();
          this.title = value;
        }
      }

      public string InitialDirectory
      {
        get => this.initialDirectory;
        set
        {
          this.CheckAllowPropertyChange();
          this.initialDirectory = value;
        }
      }

      public string FileName
      {
        get => this.fileName;
        set
        {
          this.CheckAllowPropertyChange();
          this.fileName = value;
        }
      }

      public string DefaultExtension
      {
        get => this.defaultExt;
        set
        {
          this.CheckAllowPropertyChange();
          this.defaultExt = value;
        }
      }

      public string ExtensionFilter
      {
        get => this.extFilter;
        set
        {
          this.CheckAllowPropertyChange();
          this.extFilter = value;
        }
      }

      public bool AllowMultiSelect
      {
        get => this.allowMultiSelect;
        set
        {
          this.CheckAllowPropertyChange();
          this.allowMultiSelect = value;
        }
      }

      public List<string> SelectedFiles => this.selectedFiles;

      protected override void OnAttachView()
      {
        base.OnAttachView();
        this.View.Title = this.title;
        this.View.InitialDirectory = this.initialDirectory;
        this.View.FileName = this.fileName;
        this.View.DefaultExtension = this.defaultExt;
        this.View.ExtensionFilter = this.extFilter;
        this.View.AllowMultiSelect = this.allowMultiSelect;
      }

      protected override void OnAcceptDialogResult(object sender, EventArgs e)
      {
        base.OnAcceptDialogResult(sender, e);
        this.selectedFiles.Clear();
        this.selectedFiles.AddRange((IEnumerable<string>) this.View.SelectedFiles);
      }
    }
}
