
// Type: Intermech.Mvp.Components.Dialogs.FolderBrowserPresenter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp.Components.Dialogs
{
    public sealed class FolderBrowserPresenter : SelectionDialogPresenter<IFolderBrowserView>
    {
      private string description;
      private string selectedPath;
      private bool allowNewFolders;

      public string Description
      {
        get => this.description;
        set
        {
          this.CheckAllowPropertyChange();
          this.description = value;
        }
      }

      public bool AllowNewFolders
      {
        get => this.allowNewFolders;
        set
        {
          this.CheckAllowPropertyChange();
          this.allowNewFolders = value;
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
        this.View.Description = this.description;
        this.View.SelectedPath = this.selectedPath;
        this.View.AllowNewFolders = this.allowNewFolders;
      }

      protected override void OnAcceptDialogResult(object sender, EventArgs e)
      {
        base.OnAcceptDialogResult(sender, e);
        this.selectedPath = this.View.SelectedPath;
      }
    }
}
