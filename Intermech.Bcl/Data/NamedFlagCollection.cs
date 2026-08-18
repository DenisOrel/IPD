
// Type: Intermech.Data.NamedFlagCollection
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Data
{
    public class NamedFlagCollection : ICloneable
    {
      private static readonly StringKey[] emptyFlagList = new StringKey[0];
      private OrderedList<StringKey> flagList;
      private string asString;

      public NamedFlagCollection Clone()
      {
        NamedFlagCollection namedFlagCollection = new NamedFlagCollection();
        namedFlagCollection.CopyAll(this);
        return namedFlagCollection;
      }

      object ICloneable.Clone() => (object) this.Clone();

      public void ResetAll()
      {
        if (this.flagList == null || this.flagList.Count == 0)
          return;
        this.flagList.Clear();
        this.asString = (string) null;
      }

      public bool Get(StringKey flag)
      {
        if (flag == (StringKey) null)
          throw new ArgumentNullException(nameof (flag));
        return this.flagList != null && this.flagList.Contains(flag);
      }

      public void Set(StringKey flag, bool value = true)
      {
        if (flag == (StringKey) null)
          throw new ArgumentNullException(nameof (flag));
        if (this.Get(flag) == value)
          return;
        if (value)
        {
          if (this.flagList == null)
            this.flagList = new OrderedList<StringKey>();
          this.flagList.Add(flag);
        }
        else
          this.flagList.Remove(flag);
        this.asString = (string) null;
      }

      public bool Copy(NamedFlagCollection source, StringKey flag)
      {
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        bool flag1 = !(flag == (StringKey) null) ? source.Get(flag) : throw new ArgumentNullException(nameof (flag));
        this.Set(flag, flag1);
        return flag1;
      }

      public void CopyAll(NamedFlagCollection source)
      {
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        foreach (StringKey allSetFlag in source.AllSetFlags)
          this.Set(allSetFlag);
      }

      public IEnumerable<StringKey> AllSetFlags
      {
        get
        {
          return this.flagList == null ? (IEnumerable<StringKey>) NamedFlagCollection.emptyFlagList : (IEnumerable<StringKey>) this.flagList;
        }
      }

      public bool this[StringKey flag]
      {
        [DebuggerStepThrough] get => this.Get(flag);
        [DebuggerStepThrough] set => this.Set(flag, value);
      }

      public override string ToString()
      {
        if (this.asString == null)
        {
          if (this.flagList == null)
          {
            this.asString = string.Empty;
          }
          else
          {
            using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(24 * this.flagList.Count))
            {
              StringBuilder stringBuilder = objectPoolScope.Object;
              if (this.flagList.Count != 0)
              {
                stringBuilder.Append((string) this.flagList[0]);
                for (int index = 1; index < this.flagList.Count; ++index)
                {
                  stringBuilder.Append(',');
                  stringBuilder.Append(' ');
                  stringBuilder.Append((string) this.flagList[index]);
                }
              }
              this.asString = stringBuilder.ToString();
            }
          }
        }
        return this.asString;
      }
    }
}
