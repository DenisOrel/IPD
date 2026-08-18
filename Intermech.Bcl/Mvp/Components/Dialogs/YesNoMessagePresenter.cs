
// Type: Intermech.Mvp.Components.Dialogs.YesNoMessagePresenter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Mvp.Components.Dialogs
{
    public sealed class YesNoMessagePresenter : SelectionDialogPresenter<IYesNoMessageView>
    {
      private string caption;
      private string text;
      private MessageIcon icon;
      private bool allowCancel;
      private bool isCancelled;

      public YesNoMessagePresenter()
      {
      }

      public YesNoMessagePresenter(string text, string caption, MessageIcon icon)
      {
        this.Text = text;
        this.Caption = caption;
        this.Icon = icon;
      }

      public string Caption
      {
        get => this.caption;
        set
        {
          this.CheckAllowPropertyChange();
          this.caption = value;
        }
      }

      public string Text
      {
        get => this.text;
        set
        {
          this.CheckAllowPropertyChange();
          this.text = value;
        }
      }

      public MessageIcon Icon
      {
        get => this.icon;
        set
        {
          this.CheckAllowPropertyChange();
          this.icon = value;
        }
      }

      public bool AllowCancel
      {
        get => this.allowCancel;
        set
        {
          this.CheckAllowPropertyChange();
          this.allowCancel = value;
        }
      }

      public bool IsCancelled => this.isCancelled;

      protected override void OnAttachView()
      {
        base.OnAttachView();
        this.View.Caption = this.caption;
        this.View.Text = this.text;
        this.View.Icon = this.icon;
        this.View.AllowCancel = this.allowCancel;
        this.isCancelled = false;
      }

      protected override void OnDetachView()
      {
        base.OnDetachView();
        if (!this.allowCancel || this.IsSuccessful || !this.View.IsCancelled)
          return;
        this.isCancelled = true;
      }
    }
}
