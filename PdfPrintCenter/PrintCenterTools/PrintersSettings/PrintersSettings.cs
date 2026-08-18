
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings




using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;


namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings
{
    [DataContract]
    internal class PrintersSettings : FreezableObject, ICloneable
    {
      private IDictionary<string, List<string>> _formatsToPrinters;
      private IList<string> _printersOrder;

      public PrintersSettings()
      {
        this.FormatsToPrinters = (IDictionary<string, List<string>>) new Dictionary<string, List<string>>();
        this.PrintersOrder = (IList<string>) new List<string>();
      }

      [DataMember]
      public IDictionary<string, List<string>> FormatsToPrinters
      {
        get => this._formatsToPrinters;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (FormatsToPrinters));
          this._formatsToPrinters = value;
        }
      }

      [DataMember]
      public IList<string> PrintersOrder
      {
        get => this._printersOrder;
        set
        {
          this.RequireNotFrozenBeforePropertyChange(nameof (PrintersOrder));
          this._printersOrder = value;
        }
      }

      public object Clone()
      {
        Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings = new Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings();
        foreach (KeyValuePair<string, List<string>> formatsToPrinter in (IEnumerable<KeyValuePair<string, List<string>>>) this.FormatsToPrinters)
          printersSettings.FormatsToPrinters.Add(formatsToPrinter.Key, formatsToPrinter.Value);
        printersSettings.PrintersOrder = (IList<string>) new List<string>((IEnumerable<string>) this.PrintersOrder);
        return (object) printersSettings;
      }

      protected override void DoFreeze()
      {
        this.FormatsToPrinters = (IDictionary<string, List<string>>) new ReadOnlyDictionary<string, List<string>>(this.FormatsToPrinters);
        this.PrintersOrder = (IList<string>) new ReadOnlyCollection<string>(this.PrintersOrder);
      }

      public override bool Equals(object obj)
      {
        return obj is Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings && this.IsFormatsToPrintersEquals(printersSettings.FormatsToPrinters) && this.PrintersOrder.SequenceEqual<string>((IEnumerable<string>) printersSettings.PrintersOrder);
      }

      public override int GetHashCode()
      {
        return (36686455 * -1521134295 + EqualityComparer<IDictionary<string, List<string>>>.Default.GetHashCode(this.FormatsToPrinters)) * -1521134295 + EqualityComparer<IList<string>>.Default.GetHashCode(this.PrintersOrder);
      }

      private bool IsFormatsToPrintersEquals(IDictionary<string, List<string>> obj)
      {
        if (this.FormatsToPrinters.Count != obj.Count || !this.FormatsToPrinters.Keys.All<string>(new Func<string, bool>(obj.Keys.Contains)))
          return false;
        foreach (string key in (IEnumerable<string>) this.FormatsToPrinters.Keys)
        {
          if (this.FormatsToPrinters[key].Count != obj[key].Count || !this.FormatsToPrinters[key].All<string>(new Func<string, bool>(obj[key].Contains)))
            return false;
        }
        return true;
      }
    }
}
