
// Type: Intermech.Diagnostics.ArgumentDescriptor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;


namespace Intermech.Diagnostics
{
    public readonly struct ArgumentDescriptor : IEquatable<ArgumentDescriptor>, IStructuralEquatable
    {
      public readonly Modifier Modifier;
      [CanBeNull]
      public readonly string Type;
      [CanBeNull]
      public readonly string Value;

      public ArgumentDescriptor([CanBeNull] object value)
      {
        this.Modifier = Modifier.None;
        this.Type = value?.GetType().ToString();
        this.Value = value?.ToString();
      }

      private ArgumentDescriptor(in Modifier modifier, [CanBeNull] Type type, [CanBeNull] object value)
      {
        this.Modifier = modifier;
        this.Type = type?.ToString();
        this.Value = value?.ToString();
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static implicit operator ArgumentDescriptor([NotNull] Type type)
      {
        return new ArgumentDescriptor(Modifier.None, type, (object) null);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static implicit operator ArgumentDescriptor(in (Type Type, object Value) tuple)
      {
        // ISSUE: explicit reference operation
        ref Modifier local = @Modifier.None;
            Type type = tuple.Item1;
        if ((object) type == null)
          type = tuple.Item2?.GetType();
        object obj = tuple.Item2 ?? (object) "NULL";
        return new ArgumentDescriptor(in local, type, obj);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static implicit operator ArgumentDescriptor(in (Modifier Modifier, Type Type) tuple)
      {
        return new ArgumentDescriptor(tuple.Item1, tuple.Item2, (object) null);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static implicit operator ArgumentDescriptor(in (Modifier Modifier, object Value) tuple)
      {
        return new ArgumentDescriptor(tuple.Item1, tuple.Item2?.GetType(), tuple.Item2 ?? (object) "NULL");
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static implicit operator ArgumentDescriptor(
        in (Modifier Modifier, Type Type, object Value) tuple)
      {
        // ISSUE: explicit reference operation
        ref Modifier local = @tuple.Item1;
            Type type = tuple.Item2;
        if ((object) type == null)
          type = tuple.Item3?.GetType();
        object obj = tuple.Item3 ?? (object) "NULL";
        return new ArgumentDescriptor(in local, type, obj);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Deconstructor(out Modifier modifier, [CanBeNull] out string type, [CanBeNull] out string value)
      {
        modifier = this.Modifier;
        type = this.Type;
        value = this.Value;
      }

      public override bool Equals(object other)
      {
        return other != null && other is ArgumentDescriptor other1 && this.Equals(other1);
      }

      public bool Equals([CanBeNull] object other, IEqualityComparer comparer)
      {
        return other != null && other is ArgumentDescriptor other1 && this.Equals(other1);
      }

      public bool Equals(ArgumentDescriptor other)
      {
        return this.Modifier == other.Modifier && string.Equals(this.Type, other.Type, StringComparison.InvariantCulture) && string.Equals(this.Value, other.Value, StringComparison.InvariantCulture);
      }

      public override int GetHashCode()
      {
        int modifier = (int) this.Modifier;
        string type = this.Type;
        int hashCode1 = type != null ? type.GetHashCode() : 0;
        string str = this.Value;
        int hashCode2 = str != null ? str.GetHashCode() : 0;
        return HashCode.Combine(modifier, hashCode1, hashCode2);
      }

      public int GetHashCode(IEqualityComparer comparer)
      {
        return HashCode.Combine((int) this.Modifier, comparer.GetHashCode((object) this.Type), comparer.GetHashCode((object) this.Value));
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
        if (this.Modifier != Modifier.None)
        {
          bool flag = false;
          if ((this.Modifier & Modifier.In) != Modifier.None)
          {
            stringBuilder.Append("IN");
            flag = true;
          }
          if ((this.Modifier & Modifier.Out) != Modifier.None)
            stringBuilder.Append(flag ? ", OUT" : "OUT");
          if ((this.Modifier & Modifier.Ref) != Modifier.None)
            stringBuilder.Append(flag ? ", REF" : "REF");
          stringBuilder.Append(' ');
        }
        if (this.Type != null)
          stringBuilder.Append(this.Value == null ? this.Type : $"({this.Type}) ");
        else if (this.Value == null)
          stringBuilder.Append("UNKNOWN");
        if (this.Value != null)
          stringBuilder.Append(this.Value);
        return stringBuilder.ToString();
      }

      [NotNull]
      public static implicit operator string(in ArgumentDescriptor argument) => argument.ToString();
    }
}
