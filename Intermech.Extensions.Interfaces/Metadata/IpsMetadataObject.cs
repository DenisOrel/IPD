// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.IpsMetadataObject
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public class IpsMetadataObject : IpsMetadataEntityBase<long>, IInitWithSession
{
  [CanBeNull]
  private protected QuickObjectInfo? _ObjectInfo;
  private protected bool DefaultInit = true;

  [CanBeEmpty]
  public long ObjectID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      this.CheckIsFound();
      return this.ObjectInfo.ObjectID;
    }
  }

  [NotNull]
  [CanBeEmpty]
  public string Caption
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      this.CheckIsFound();
      return this.ObjectInfo.Caption;
    }
  }

  [CanBeEmpty]
  public int TypeID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      this.CheckIsFound();
      return this.ObjectInfo.ObjectTypeID;
    }
  }

  [NotEmpty]
  public QuickObjectInfo ObjectInfo
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      this.CheckIsFound();
      if (this._ObjectInfo.HasValue)
      {
        QuickObjectInfo objectInfo = this._ObjectInfo.Value;
        if (!objectInfo.Empty)
          return objectInfo;
      }
      throw new NotYetInitializedException(this._Namespace + ".MetadataLoader", $"Идентификатор {this.EntityInstanceNameInGenitiveCase} \"{this.FullPropertyName}\" ещё не инициализирован.{Environment.NewLine}Необходим вызов {this._Namespace}.MetadataLoader.Init()!");
    }
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public new void CheckIsFound()
  {
    if (!this.Found)
      throw new ObjectVersionNotFoundException(this.Guid, $"{this.FullPropertyName}: {this.EntityInstanceName} с Guid={this.Guid} не найден!");
  }

  protected IpsMetadataObject(
    [NotEmpty] long id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
  }

  protected IpsMetadataObject([NotEmpty] Guid guid, [NotNull] Type holderType, bool obligatory, [NotNull, NotWhitespace] string idPropertyName)
    : base(guid, holderType, obligatory, idPropertyName)
  {
    this._id = 0L;
  }

  void IInitWithSession.Init([NotNull] IUserSession session)
  {
    if (!this.DefaultInit)
      return;
    QuickObjectInfo objectInfo = session.GetObjectInfo(this.Guid);
    this._Found = new bool?(!objectInfo.Empty);
    if (!objectInfo.Empty)
    {
      this._id = objectInfo.ID;
      this._ObjectInfo = new QuickObjectInfo?(objectInfo);
      this.AdditionalInit(session, in objectInfo);
    }
    else if (this.Obligatory)
      throw new ObjectVersionNotFoundException(this.Guid, $"{this.FullPropertyName}: {this.EntityInstanceName} с Guid={this.Guid} не найден!");
  }

  protected virtual void AdditionalInit([NotNull] IUserSession session, [NotEmpty] in QuickObjectInfo objectInfo)
  {
  }
}
