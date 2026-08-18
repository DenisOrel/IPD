
// Type: Intermech.Settings.ValueCell`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Settings
{
    public class ValueCell<T> : IValueCell<T>, IValueCell
    {
      private T rawValue;
      private ValueCellState state;
      private string error;

      public ValueCell(T value)
      {
        this.rawValue = value;
        this.state = ValueCellState.Invalidated;
      }

      /// <summary>
      /// Сбрасывает признак, что значение ячейки было проверено на допустимость. Автоматически
      /// вызывается при изменении значения в ячейке.
      /// </summary>
      public virtual void Invalidate()
      {
        if (this.state == ValueCellState.Invalidated)
          return;
        this.state = ValueCellState.Invalidated;
        this.OnInvalidated();
      }

      protected virtual void OnInvalidated()
      {
        if (this.Invalidated == null)
          return;
        this.Invalidated((object) this, EventArgs.Empty);
      }

      /// <summary>Проверяет значение ячейки на допустимость.</summary>
      public virtual void Validate()
      {
        if (this.state != ValueCellState.Invalidated)
          return;
        this.state = ValueCellState.Valid;
        this.error = (string) null;
        this.OnValidating();
        if (this.state == ValueCellState.Invalid)
          this.error = this.OnGetErrorText(this.error);
        this.OnValidated();
      }

      protected virtual void OnValidating()
      {
        if (this.Validating == null)
          return;
        this.Validating((object) this, EventArgs.Empty);
      }

      protected virtual void OnValidated()
      {
        if (this.Validated == null)
          return;
        this.Validated((object) this, EventArgs.Empty);
      }

      protected virtual string OnGetErrorText(string text)
      {
        string text1 = text;
        if (this.GetErrorText != null)
        {
          ErrorTextArgs e = new ErrorTextArgs(text1);
          this.GetErrorText((object) this, e);
          text1 = e.Text;
        }
        return text1;
      }

      public virtual T Value
      {
        get
        {
          if (this.state == ValueCellState.Invalidated)
            this.Validate();
          if (this.state == ValueCellState.Valid)
            return this.rawValue;
          throw new FaultException(this.error);
        }
      }

      public virtual ValueCellState State => this.state;

      public virtual string Error
      {
        get => this.error;
        set
        {
          if (string.Equals(this.error, value))
            return;
          if (string.IsNullOrEmpty(value))
          {
            this.state = ValueCellState.Invalidated;
            this.error = (string) null;
          }
          else
          {
            this.state = ValueCellState.Invalid;
            this.error = value;
          }
        }
      }

      public virtual T RawValue
      {
        get => this.rawValue;
        set
        {
          if (object.Equals((object) this.rawValue, (object) value))
            return;
          this.rawValue = value;
          if (this.RawValueChanged != null)
            this.RawValueChanged((object) this, EventArgs.Empty);
          this.Invalidate();
        }
      }

      public static implicit operator T(ValueCell<T> cell)
      {
        return cell != null ? cell.Value : throw new ArgumentNullException(nameof (cell));
      }

      public event EventHandler RawValueChanged;

      public event EventHandler Invalidated;

      public event EventHandler Validating;

      public event EventHandler Validated;

      public event EventHandler<ErrorTextArgs> GetErrorText;
    }
}
