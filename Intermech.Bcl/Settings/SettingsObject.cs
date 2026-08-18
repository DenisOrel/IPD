
// Type: Intermech.Settings.SettingsObject
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Settings
{
    public abstract class SettingsObject
    {
      private readonly LinkedList<ISettingsCell> cells;
      private readonly LinkedList<object> validators;

      public SettingsObject()
      {
        this.cells = new LinkedList<ISettingsCell>();
        this.validators = new LinkedList<object>();
        this.CreateCells((ICollection<ISettingsCell>) this.cells);
        this.CreateValidators((ICollection<object>) this.validators);
        foreach (ISettingsCell cell in this.cells)
          this.AttachCell(cell);
      }

      protected virtual void CreateCells(ICollection<ISettingsCell> cells)
      {
      }

      protected virtual void CreateValidators(ICollection<object> validators)
      {
      }

      private void AttachCell(ISettingsCell cell)
      {
        cell.RawValueChanged += new EventHandler(this.CellChangedHandler);
        cell.GetErrorText += new EventHandler<ErrorTextArgs>(this.GetErrorTextHandler);
      }

      public void Validate()
      {
        lock (this)
        {
          foreach (IValueCell cell in this.cells)
            cell.Validate();
        }
      }

      public string GetFirstError()
      {
        lock (this)
        {
          foreach (ISettingsCell cell in this.cells)
          {
            if (cell.State == ValueCellState.Invalid)
              return cell.Error;
          }
          return (string) null;
        }
      }

      public LinkedList<string> GetErrors()
      {
        lock (this)
        {
          LinkedList<string> errors = new LinkedList<string>();
          foreach (ISettingsCell cell in this.cells)
          {
            if (cell.State == ValueCellState.Invalid)
              errors.AddLast(cell.Error);
          }
          return errors;
        }
      }

      private void CellChangedHandler(object sender, EventArgs e)
      {
        if (this.Changed == null)
          return;
        this.Changed((object) this, EventArgs.Empty);
      }

      private void GetErrorTextHandler(object sender, ErrorTextArgs e)
      {
        e.Text = string.Format(LocalizationHolder.rm.GetString("SR_815"), (object) ((ISettingsCell) sender).DisplayName, (object) e.Text);
        if (this.GetErrorText == null)
          return;
        this.GetErrorText((object) this, e);
      }

      public event EventHandler Changed;

      public event EventHandler<ErrorTextArgs> GetErrorText;
    }
}
