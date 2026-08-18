
// Type: Intermech.Mvp.Components.Dialogs.SaveFilePresenter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp.Components.Dialogs
{
    public sealed class SaveFilePresenter : SelectionDialogPresenter<ISaveFileView>
    {
      private string title;
      private string initialDirectory;
      private string fileName;
      private string defaultExt;
      private string extFilter;
      private string selectedPath;

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

      public string SelectedPath
      {
        get => this.selectedPath;
        set
        {
          this.CheckAllowPropertyChange();
          this.selectedPath = value;
        }
      }

      protected override void OnAttachView()
      {
        base.OnAttachView();
        this.View.Title = this.title;
        this.View.InitialDirectory = this.initialDirectory;
        this.View.FileName = this.fileName;
        this.View.DefaultExtension = this.defaultExt;
        this.View.ExtensionFilter = this.extFilter;
      }

      protected override void OnAcceptDialogResult(object sender, EventArgs e)
      {
        base.OnAcceptDialogResult(sender, e);
        this.selectedPath = this.View.SelectedPath;
      }
    }
}
