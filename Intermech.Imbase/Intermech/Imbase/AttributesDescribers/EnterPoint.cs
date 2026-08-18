// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.EnterPoint
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal sealed class EnterPoint
{
  public string SiteCode { get; private set; }

  public string SiteName { get; private set; }

  public EnterPoint()
    : this(string.Empty, string.Empty)
  {
  }

  public EnterPoint(string siteCode)
  {
    this.SiteCode = siteCode;
    this.SiteName = string.Empty;
  }

  public EnterPoint(string siteCode, string siteName)
  {
    this.SiteCode = siteCode;
    this.SiteName = siteName;
  }

  public override string ToString()
  {
    return string.IsNullOrEmpty(this.SiteName) ? this.SiteCode : this.SiteName;
  }
}
