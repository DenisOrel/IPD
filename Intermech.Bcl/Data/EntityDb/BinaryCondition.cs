
// Type: Intermech.Data.EntityDb.BinaryCondition
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Text;


namespace Intermech.Data.EntityDb
{
    public sealed class BinaryCondition : PropertyValueCondition
    {
      private readonly BinaryOperator op;
      private readonly object value;

      public BinaryCondition(object propertyReference, BinaryOperator op, object value)
        : base(propertyReference)
      {
        this.op = op;
        this.value = value;
      }

      public BinaryCondition Clone()
      {
        return new BinaryCondition(this.PropertyReference, this.op, this.value);
      }

      protected override object DoClone() => (object) this.Clone();

      public BinaryOperator Operator => this.op;

      public object Value => this.value;

      public override string ToString()
      {
        return $"[{this.PropertyReference}] {BinaryCondition.GetOpText(this.op)} '{BinaryCondition.GetValueText(this.value)}'";
      }

      private static string GetValueText(object value)
      {
        if (value is ICollection collection)
        {
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
          {
            StringBuilder stringBuilder = objectPoolScope.Object;
            foreach (object obj in (IEnumerable) collection)
            {
              if (stringBuilder.Length > 0)
                stringBuilder.Append(", ");
              stringBuilder.Append(BinaryCondition.GetValueText(obj));
            }
            if (stringBuilder.Length > 0)
            {
              stringBuilder.Insert(0, '{');
              stringBuilder.Append('}');
            }
            return stringBuilder.ToString();
          }
        }
        return value != null ? value.ToString() : string.Empty;
      }

      private static string GetOpText(BinaryOperator op)
      {
        switch (op)
        {
          case BinaryOperator.Equal:
            return "==";
          case BinaryOperator.In:
            return "in";
          case BinaryOperator.Less:
            return "<";
          case BinaryOperator.LessOrEqual:
            return "<=";
          case BinaryOperator.Greater:
            return ">";
          case BinaryOperator.GreaterOrEqual:
            return ">=";
          default:
            throw new NotImplementedException();
        }
      }
    }
}
