// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.IpsMetadataEntityBase`1
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

[DebuggerDisplay("FullPropertyName")]
[Serializable]
public abstract class IpsMetadataEntityBase<TID> where TID : struct
{
  private protected TID _id;
  [NotEmpty]
  public readonly Guid Guid;
  public readonly bool Obligatory;
  private protected bool? _Found;
  [NotNull]
  [NotWhitespace]
  public readonly string FullPropertyName;
  [NotNull]
  protected readonly string _Namespace;
  [NotNull]
  private protected readonly Type _HolderType;
  [NotNull]
  private protected readonly string _IdPropertyName;

  public TID ID
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      this.CheckIsFound();
      return this._id;
    }
  }

  protected internal bool Found
  {
    get
    {
      this.CheckIsLoaded();
      return this._Found.Value;
    }
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckIsFound()
  {
    if (!this.Found)
      throw new Exception($"{this.FullPropertyName}: {this.EntityInstanceName} с Guid={this.Guid} не найден!");
  }

  public bool Loaded
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Found.HasValue;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckIsLoaded()
  {
    if (!this._Found.HasValue)
      throw new NotYetInitializedException(this._Namespace + ".MetadataLoader", $"Идентификатор {this.EntityInstanceNameInGenitiveCase} \"{this.FullPropertyName}\" ещё не инициализирован.{Environment.NewLine}Необходим вызов {this._Namespace}.MetadataLoader.Init()!");
  }

  [CanBeNull]
  protected string EntityName
  {
    get
    {
      FieldInfo field = this._HolderType.GetField(nameof (EntityName), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
      return !(field == (FieldInfo) null) ? Intermech.Diagnostics.Check.Is<string>(field.GetValue((object) null)) : throw new Exception($"У сущности {this._HolderType.FullName} не определена строковая константа EntityName содержащая имя сущности");
    }
  }

  [CanBeNull]
  protected string EntityInstanceName
  {
    get
    {
      FieldInfo field = this._HolderType.GetField(nameof (EntityInstanceName), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
      return !(field == (FieldInfo) null) ? Intermech.Diagnostics.Check.Is<string>(field.GetValue((object) null)) : throw new Exception($"У сущности {this._HolderType.FullName} не определена строковая константа EntityInstanceName содержащая имя сущности в родительном падеже");
    }
  }

  [CanBeNull]
  protected string EntityInstanceNameInGenitiveCase
  {
    get
    {
      FieldInfo field = this._HolderType.GetField(nameof (EntityInstanceNameInGenitiveCase), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
      return !(field == (FieldInfo) null) ? Intermech.Diagnostics.Check.Is<string>(field.GetValue((object) null)) : throw new Exception($"У сущности {this._HolderType.FullName} не определена строковая константа EntityInstanceNameInGenitiveCase содержащая имя сущности в родительном падеже");
    }
  }

  protected IpsMetadataEntityBase(
    [NotEmpty] TID id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : this(guid, holderType, obligatory, idPropertyName)
  {
    this._id = id;
  }

  protected IpsMetadataEntityBase(
    [NotNull, NotWhitespace] string guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : this(new Guid(guid), holderType, obligatory, idPropertyName)
  {
  }

  private IpsMetadataEntityBase([NotNull] Type holderType, bool obligatory, [NotNull, NotWhitespace] string idPropertyName)
  {
    this.Obligatory = obligatory;
    this._HolderType = holderType;
    this._Namespace = holderType.Namespace ?? string.Empty;
    this._IdPropertyName = idPropertyName;
    string str;
    if (string.IsNullOrWhiteSpace(this._Namespace))
      str = $"{holderType.Name}.{idPropertyName}";
    else
      str = $"{this._Namespace}.{holderType.Name}.{idPropertyName}";
    this.FullPropertyName = str;
  }

  protected IpsMetadataEntityBase(
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : this(holderType, obligatory, idPropertyName)
  {
    this.Guid = guid;
  }

  protected internal IpsMetadataEntityBase([NotEmpty] TID id, [NotNull] Type holderType, [NotNull, NotWhitespace] string idPropertyName)
    : this(holderType, true, idPropertyName)
  {
    this._id = id;
  }

  public override int GetHashCode()
  {
    if (!this.Loaded)
      return 0;
    return !this.Found ? this.Guid.GetHashCode() : this._id.GetHashCode();
  }

  public override string ToString()
  {
    if (!this.Loaded)
      return string.Empty;
    return !this.Found ? $"{this.FullPropertyName} Guid={this.Guid}" : $"{this.FullPropertyName} ID={this._id}";
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator TID([NotNull] IpsMetadataEntityBase<TID> entity) => entity.ID;

  public static implicit operator OneOrMore<TID>([NotNull] IpsMetadataEntityBase<TID> entity)
  {
    return (OneOrMore<TID>) entity.ID;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static explicit operator Guid([NotNull] IpsMetadataEntityBase<TID> entity) => entity.Guid;
}
