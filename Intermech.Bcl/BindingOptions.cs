
// Type: Intermech.BindingOptions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>Extensions for BindingOptions</summary>
    public struct BindingOptions : ISerializable, IEquatable<BindingOptions>, IEquatable<BindingFlags>
    {
      [UsedImplicitly]
      private BindingFlags _flags;

      public BindingFlags Flags
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._flags;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._flags = value;
      }

      public BindingOptions(BindingFlags flags) => this._flags = flags;

      public BindingOptions([NotNull] SerializationInfo info, StreamingContext context)
      {
        this._flags = (BindingFlags) info.GetValue(nameof (Flags), typeof (BindingFlags));
      }

      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("Flags", (object) this._flags);
      }

      public void ValidateForUse()
      {
        Intermech.Diagnostics.Check.Assert((this._flags & BindingFlags.Static) != BindingFlags.Default || (this._flags & BindingFlags.Instance) != 0, "Ether Static or Instance must be in BindingFlags!");
        Intermech.Diagnostics.Check.Assert((this._flags & BindingFlags.Public) != BindingFlags.Default || (this._flags & BindingFlags.NonPublic) != 0, "Ether Public or NonPublic must be in BindingFlags!");
      }

      /// <summary>Specifies that the case of the member name should not be considered when binding.</summary>
      public bool IgnoreCase
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.IgnoreCase) == BindingFlags.IgnoreCase;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.IgnoreCase : this._flags ^ BindingFlags.IgnoreCase;
        }
      }

      /// <summary>Specifies that only members declared at the level of the supplied type's hierarchy should be considered.
      /// Inherited members are not considered.</summary>
      public bool DeclaredOnly
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.DeclaredOnly) == BindingFlags.DeclaredOnly;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.DeclaredOnly : this._flags ^ BindingFlags.DeclaredOnly;
        }
      }

      /// <summary>Specifies that instance members are to be included in the search.</summary>
      public bool Instance
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.Instance) == BindingFlags.Instance;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.Instance : this._flags ^ BindingFlags.Instance;
          if (value || this.Static)
            return;
          this.Static = true;
        }
      }

      /// <summary>Specifies that static members are to be included in the search.</summary>
      public bool Static
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.Static) == BindingFlags.Static;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.Static : this._flags ^ BindingFlags.Static;
          if (value || this.Instance)
            return;
          this.Instance = true;
        }
      }

      /// <summary>Specifies that public members are to be included in the search.</summary>
      public bool Public
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.Public) == BindingFlags.Public;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.Public : this._flags ^ BindingFlags.Public;
          if (value || this.NonPublic)
            return;
          this.NonPublic = true;
        }
      }

      /// <summary>Specifies that non-public members are to be included in the search.</summary>
      public bool NonPublic
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.NonPublic) == BindingFlags.NonPublic;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.NonPublic : this._flags ^ BindingFlags.NonPublic;
          if (value || this.Public)
            return;
          this.Public = true;
        }
      }

      /// <summary>Specifies that public and protected static members up the hierarchy should be returned. Private static members
      /// in inherited classes are not returned. Static members include fields, methods, events, and properties. Nested
      /// types are not returned.</summary>
      public bool FlattenHierarchy
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.FlattenHierarchy) == BindingFlags.FlattenHierarchy;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.FlattenHierarchy : this._flags ^ BindingFlags.FlattenHierarchy;
        }
      }

      /// <summary>Specifies that a method is to be invoked. This must not be a constructor or a type initializer.
      /// This flag is passed to an <see langword="InvokeMember" /> method to invoke a method.</summary>
      public bool InvokeMethod
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.InvokeMethod) == BindingFlags.InvokeMethod;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.InvokeMethod : this._flags ^ BindingFlags.InvokeMethod;
        }
      }

      /// <summary>Specifies that reflection should create an instance of the specified type. Calls the constructor that matches
      /// the given arguments. The supplied member name is ignored. If the type of lookup is not specified, (Instance |
      /// Public) will apply. It is not possible to call a type initializer. This flag is passed to an
      /// <see langword="InvokeMember" /> method to invoke a constructor.</summary>
      public bool CreateInstance
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.CreateInstance) == BindingFlags.CreateInstance;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.CreateInstance : this._flags ^ BindingFlags.CreateInstance;
        }
      }

      /// <summary>Specifies that the value of the specified field should be returned.
      /// This flag is passed to an <see langword="InvokeMember" /> method to get a field value.</summary>
      public bool GetField
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.GetField) == BindingFlags.GetField;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.GetField : this._flags ^ BindingFlags.GetField;
        }
      }

      /// <summary>Specifies that the value of the specified field should be set.
      /// This flag is passed to an <see langword="InvokeMember" /> method to set a field value.</summary>
      public bool SetField
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.SetField) == BindingFlags.SetField;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.SetField : this._flags ^ BindingFlags.SetField;
        }
      }

      /// <summary>Specifies that the value of the specified property should be returned.
      /// This flag is passed to an <see langword="InvokeMember" /> method to invoke a property getter.</summary>
      public bool GetProperty
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.GetProperty) == BindingFlags.GetProperty;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.GetProperty : this._flags ^ BindingFlags.GetProperty;
        }
      }

      /// <summary>Specifies that the value of the specified property should be set. For COM properties, specifying this binding
      /// flag is equivalent to specifying <see langword="PutDispProperty" /> and <see langword="PutRefDispProperty" />.
      /// This flag is passed to an <see langword="InvokeMember" /> method to invoke a property setter.</summary>
      public bool SetProperty
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.SetProperty) == BindingFlags.SetProperty;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.SetProperty : this._flags ^ BindingFlags.SetProperty;
        }
      }

      /// <summary>Specifies that the <see langword="PROPPUT" /> member on a COM object should be invoked.
      /// <see langword="PROPPUT" /> specifies a property-setting function that uses a value. Use
      /// <see langword="PutDispProperty" /> if a property has both <see langword="PROPPUT" /> and
      /// <see langword="PROPPUTREF" /> and you need to distinguish which one is called.</summary>
      public bool PutDispProperty
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.PutDispProperty) == BindingFlags.PutDispProperty;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.PutDispProperty : this._flags ^ BindingFlags.PutDispProperty;
        }
      }

      /// <summary>Specifies that the <see langword="PROPPUTREF" /> member on a COM object should be invoked.
      /// <see langword="PROPPUTREF" /> specifies a property-setting function that uses a reference instead of a value. Use
      /// <see langword="PutRefDispProperty" /> if a property has both <see langword="PROPPUT" /> and
      /// <see langword="PROPPUTREF" /> and you need to distinguish which one is called.</summary>
      public bool PutRefDispProperty
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.PutRefDispProperty) == BindingFlags.PutRefDispProperty;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.PutRefDispProperty : this._flags ^ BindingFlags.PutRefDispProperty;
        }
      }

      /// <summary>Specifies that types of the supplied arguments must exactly match the types of the corresponding formal
      /// parameters. Reflection throws an exception if the caller supplies a non-null <see langword="Binder" /> object,
      /// since that implies that the caller is supplying <see langword="BindToXXX" /> implementations that will pick the
      /// appropriate method.</summary>
      public bool ExactBinding
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.ExactBinding) == BindingFlags.ExactBinding;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.ExactBinding : this._flags ^ BindingFlags.ExactBinding;
        }
      }

      /// <summary>Not implemented.</summary>
      public bool SuppressChangeType
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.SuppressChangeType) == BindingFlags.SuppressChangeType;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.SuppressChangeType : this._flags ^ BindingFlags.SuppressChangeType;
        }
      }

      /// <summary>Returns the set of members whose parameter count matches the number of supplied arguments. This binding flag
      /// is used for methods with parameters that have default values and methods with variable arguments (varargs). This
      /// flag should only be used with
      /// <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
      public bool OptionalParamBinding
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.OptionalParamBinding) == BindingFlags.OptionalParamBinding;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.OptionalParamBinding : this._flags ^ BindingFlags.OptionalParamBinding;
        }
      }

      /// <summary>Used in COM interop to specify that the return value of the member can be ignored.</summary>
      public bool IgnoreReturn
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (this._flags & BindingFlags.IgnoreReturn) == BindingFlags.IgnoreReturn;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._flags = value ? this._flags & BindingFlags.IgnoreReturn : this._flags ^ BindingFlags.IgnoreReturn;
        }
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static implicit operator BindingFlags(BindingOptions bindingOptions)
      {
        return bindingOptions._flags;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static implicit operator BindingOptions(BindingFlags bindingFlags)
      {
        return new BindingOptions(bindingFlags);
      }

      public bool Equals(BindingOptions other) => this._flags == other._flags;

      public bool Equals(BindingFlags other) => this._flags == other;

      public override bool Equals(object obj)
      {
        if (obj == null)
          return false;
        if (obj is BindingOptions other1 && this.Equals(other1))
          return true;
        object other2;
        return (other2 = obj) is BindingFlags && this.Equals((BindingFlags) other2);
      }

      public override int GetHashCode() => this._flags.GetHashCode();

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool operator ==(BindingOptions left, BindingOptions right)
      {
        return left._flags == right._flags;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool operator !=(BindingOptions left, BindingOptions right)
      {
        return left._flags != right._flags;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool operator ==(BindingOptions left, BindingFlags right) => left._flags == right;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool operator !=(BindingOptions left, BindingFlags right) => left._flags != right;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool operator ==(BindingFlags left, BindingOptions right) => left == right._flags;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool operator !=(BindingFlags left, BindingOptions right) => left != right._flags;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static BindingOptions operator +(BindingOptions left, BindingOptions right)
      {
        return (BindingOptions) (left._flags & right._flags);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static BindingOptions operator +(BindingOptions left, BindingFlags right)
      {
        return (BindingOptions) (left._flags & right);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static BindingOptions operator -(BindingFlags left, BindingOptions right)
      {
        return (BindingOptions) (left & right._flags);
      }
    }
}
