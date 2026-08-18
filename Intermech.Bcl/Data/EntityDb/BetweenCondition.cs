
// Type: Intermech.Data.EntityDb.BetweenCondition
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data.EntityDb
{
    public sealed class BetweenCondition : PropertyValueCondition
    {
      private readonly object leftValue;
      private readonly object rightValue;

      public BetweenCondition(object propertyReference, object leftValue, object rightValue)
        : base(propertyReference)
      {
        this.leftValue = leftValue;
        this.rightValue = rightValue;
      }

      public BetweenCondition Clone()
      {
        return new BetweenCondition(this.PropertyReference, this.leftValue, this.rightValue);
      }

      protected override object DoClone() => (object) this.Clone();

      public object LeftValue => this.leftValue;

      public object RightValue => this.rightValue;

      public override string ToString()
      {
        return $"[{this.PropertyReference}] between '{this.leftValue}' and '{this.rightValue}'";
      }
    }
}
