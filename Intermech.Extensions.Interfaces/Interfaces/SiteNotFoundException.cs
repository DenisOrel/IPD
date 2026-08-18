// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.SiteNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class SiteNotFoundException : 
  KernelException,
  IEquatable<SiteNotFoundException>,
  ISerializable
{
  private const string Msg = "Сайт {0} не найден!";
  public const char UnknownSiteCode = '?';
  protected readonly SiteNotFoundException.Params _Params;

  public long? ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Params.ID;
  }

  public Guid Guid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Params.Guid;
  }

  public char? Code
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Params.Code;
  }

  [CanBeNull]
  public string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Params.Name;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public SiteNotFoundException()
    : this(new SiteNotFoundException.Params())
  {
  }

  public SiteNotFoundException([NotEmpty] long siteID, [CanBeNull] string customMessage = null)
    : this(new SiteNotFoundException.Params(new long?(siteID)), customMessage)
  {
  }

  public SiteNotFoundException([NotEmpty] Guid siteGuid, [CanBeNull] string customMessage = null)
    : this(new SiteNotFoundException.Params(guid: new Guid?(siteGuid)), customMessage)
  {
  }

  public SiteNotFoundException([NotEmpty] char siteCode, [CanBeNull] string customMessage = null)
    : this(new SiteNotFoundException.Params(code: new char?(siteCode)), customMessage)
  {
  }

  public SiteNotFoundException([NotNull] string siteName, [CanBeNull] string customMessage)
    : this(new SiteNotFoundException.Params(name: siteName), customMessage)
  {
  }

  public SiteNotFoundException(in SiteNotFoundException.Params siteParams, [CanBeNull] string customMessage = null)
    : base(customMessage ?? SiteNotFoundException.CreateMessage(in siteParams))
  {
    this._Params = siteParams;
  }

  [NotNull]
  private static string CreateMessage(in SiteNotFoundException.Params siteParams)
  {
    if (!string.IsNullOrWhiteSpace(siteParams.Name))
      return $"Сайт портала \"{siteParams.Name}\" не найден!";
    if (siteParams.ID.HasValue)
      return $"Сайт портала с ID={siteParams.ID.Value} не найден!";
    if (siteParams.Code.HasValue)
      return $"Сайт портала с кодом '{siteParams.Code.Value}' не найден!";
    return !(siteParams.Guid != Guid.Empty) ? "Сайт портала не найден!" : $"Сайт портала с GUID={siteParams.Guid} не найден!";
  }

  [SecuritySafeCritical]
  protected SiteNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._Params = info.GetValue<SiteNotFoundException.Params>("Params");
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Params", (object) this._Params, typeof (SiteNotFoundException.Params));
  }

  public bool Equals(SiteNotFoundException other)
  {
    if (other == null)
      return false;
    return this == other || this._Params.Equals((object) other._Params);
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return !(obj.GetType() != this.GetType()) && this.Equals((SiteNotFoundException) obj);
  }

  public override int GetHashCode() => this._Params.GetHashCode();

  public readonly struct Params(long? id = null, Guid? guid = null, char? code = null, [CanBeNull] string name = null)
  {
    public readonly long? ID = id;
    public readonly Guid Guid = guid ?? Guid.Empty;
    public readonly char? Code = code;
    [CanBeNull]
    public readonly string Name = name;
  }
}
