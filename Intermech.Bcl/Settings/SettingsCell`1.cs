
// Type: Intermech.Settings.SettingsCell`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Settings
{
    public class SettingsCell<T> : SyncValueCell<T>, ISettingsCell, IValueCell
    {
      private readonly string displayName;

      public SettingsCell(object syncRoot, string displayName, T value)
        : base(syncRoot, value)
      {
        this.displayName = !string.IsNullOrEmpty(displayName) ? displayName : throw new ArgumentException();
      }

      protected override void OnValidating()
      {
        base.OnValidating();
        if (this.ValidatingCell != null)
          this.ValidatingCell((object) this, EventArgs.Empty);
        if (this.ValidatingGroup == null)
          return;
        this.ValidatingGroup((object) this, EventArgs.Empty);
      }

      public string DisplayName => this.displayName;

      public event EventHandler ValidatingCell;

      public event EventHandler ValidatingGroup;
    }
}
