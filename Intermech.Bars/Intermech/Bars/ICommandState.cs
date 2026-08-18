
// Type: Intermech.Bars.ICommandState
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll


namespace Intermech.Bars
{
    public interface ICommandState
    {
      string CommandName { get; set; }

      bool Visible { get; set; }

      bool Enabled { get; set; }

      bool Checked { get; set; }

      string Text { get; set; }

      int ImageIndex { get; set; }

      string ToolTipText { get; set; }

      object Tag { get; set; }

      object Sender { get; set; }
    }
}
