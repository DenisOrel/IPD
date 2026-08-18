
// Type: Intermech.Mvp.Components.Dialogs.SimpleMessagePresenter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Mvp.Components.Dialogs
{
    public sealed class SimpleMessagePresenter : Presenter<ISimpleMessageView>
    {
      private string caption;
      private string text;
      private MessageIcon icon;

      public SimpleMessagePresenter()
      {
      }

      public SimpleMessagePresenter(string text, string caption, MessageIcon icon)
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

      protected override void OnAttachView()
      {
        base.OnAttachView();
        this.View.Caption = this.caption;
        this.View.Text = this.text;
        this.View.Icon = this.icon;
      }
    }
}
