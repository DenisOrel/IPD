
// Type: Intermech.Data.EntityDb.CodeCondition
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.EntityDb
{
    public sealed class CodeCondition : IQueryCondition, ICloneable
    {
      private readonly Predicate<IEntity> filter;

      public CodeCondition(Predicate<IEntity> filter)
      {
        this.filter = filter != null ? filter : throw new ArgumentNullException(nameof (filter));
      }

      public CodeCondition Clone() => new CodeCondition(this.filter);

      object ICloneable.Clone() => (object) this.Clone();

      public Predicate<IEntity> Filter => this.filter;

      public override string ToString() => $"code: {this.filter.Method}";
    }
}
