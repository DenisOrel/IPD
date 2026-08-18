
// Type: Intermech.Data.EntityDb.CompoundSetCondition
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Data.EntityDb
{
    public sealed class CompoundSetCondition : IQueryCondition, ICloneable
    {
      private readonly CompoundSetOperator op;
      private readonly List<IQueryCondition> subConditions;

      public CompoundSetCondition(CompoundSetOperator op)
      {
        this.op = op;
        this.subConditions = new List<IQueryCondition>();
      }

      public CompoundSetCondition(CompoundSetOperator op, int capacity)
      {
        this.op = op;
        this.subConditions = new List<IQueryCondition>(capacity);
      }

      public CompoundSetCondition(CompoundSetOperator op, IEnumerable<IQueryCondition> subConditions)
      {
        if (subConditions == null)
          throw new ArgumentNullException(nameof (subConditions));
        this.op = op;
        this.subConditions = new List<IQueryCondition>(subConditions);
        this.subConditions.RemoveAll((Predicate<IQueryCondition>) (item => item == null));
      }

      public CompoundSetCondition(CompoundSetOperator op, params IQueryCondition[] subConditions)
        : this(op, (IEnumerable<IQueryCondition>) subConditions)
      {
      }

      public CompoundSetCondition Clone()
      {
        CompoundSetCondition compoundSetCondition = new CompoundSetCondition(this.op, this.subConditions.Capacity);
        compoundSetCondition.SubConditions.AddRange((IEnumerable<IQueryCondition>) this.subConditions);
        return compoundSetCondition;
      }

      object ICloneable.Clone() => (object) this.Clone();

      public CompoundSetOperator Operator => this.op;

      public List<IQueryCondition> SubConditions => this.subConditions;

      public override string ToString()
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          if (this.subConditions.Count == 0)
            stringBuilder.Append("<empty>");
          else if (this.subConditions.Count == 1)
          {
            stringBuilder.Append(this.subConditions[0].ToString());
          }
          else
          {
            stringBuilder.AppendFormat("({0})", (object) this.subConditions[0]);
            for (int index = 1; index < this.subConditions.Count; ++index)
            {
              stringBuilder.Append(' ');
              stringBuilder.Append(CompoundSetCondition.GetOpText(this.op));
              stringBuilder.Append(' ');
              stringBuilder.AppendFormat("({0})", (object) this.subConditions[index]);
            }
          }
          return stringBuilder.ToString();
        }
      }

      private static string GetOpText(CompoundSetOperator op)
      {
        switch (op)
        {
          case CompoundSetOperator.Union:
            return "||";
          case CompoundSetOperator.Intersection:
            return "&&";
          case CompoundSetOperator.Complement:
            return "~~";
          default:
            throw new NotImplementedException();
        }
      }
    }
}
