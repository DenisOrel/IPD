// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AttributeWithGuidNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class AttributeWithGuidNotFoundException : AttributeWithIdNotFoundException, ISerializable
{
  private const string Msg = "Трибут с Guid = {0} не найден";

  public Guid AttributeGuid { get; }

  public Guid Guid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.AttributeGuid;
  }

  private long AttributeID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => 0;
  }

  public AttributeWithGuidNotFoundException([NotEmpty] Guid attributeGuid, [CanBeNull] string customMessage = null)
    : base(0, customMessage)
  {
    this.AttributeGuid = attributeGuid;
  }

  public AttributeWithGuidNotFoundException([NotNull, NotWhitespace] string attributeGuid, [CanBeNull] string customMessage = null)
    : base(new Guid(attributeGuid), customMessage)
  {
    this.AttributeGuid = new Guid(attributeGuid);
  }

  protected AttributeWithGuidNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.AttributeGuid = new Guid(info.GetString("_GUID"));
  }

  [NotNull]
  public override string Message
  {
    get => this._CustomMessage ?? $"Трибут с Guid = {this.AttributeGuid} не найден";
  }

  public override int GetHashCode() => this.AttributeGuid.GetHashCode();

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (obj == this)
      return true;
    return obj is AttributeWithGuidNotFoundException notFoundException && notFoundException.AttributeGuid == this.AttributeGuid;
  }
}
