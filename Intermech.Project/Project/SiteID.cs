// Decompiled with JetBrains decompiler
// Type: Intermech.Project.SiteID
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.WebPortal;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace Intermech.Project;

public class SiteID
{
  [NotNull]
  private StringBuilder _value;

  [NotNull]
  public string Value
  {
    get
    {
      char currentSiteCode = Portal.CurrentSiteCode;
      if (this._value.Length > 0 && (int) this._value[0] == (int) currentSiteCode && (this._value.Length <= 1 || this._value.Length > 1 && (int) this._value[1] == (int) currentSiteCode) && (this._value.Length <= 2 || this._value.Length > 2 && (int) this._value[2] == (int) currentSiteCode))
        this._value = new StringBuilder(string.Empty);
      return this._value.ToString();
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._value = new StringBuilder(value);
    }
  }

  public SiteID([CanBeNull] string value, [CanBeNull] ISitesCacheService srv = null)
  {
    this._value = new StringBuilder(value ?? string.Empty);
  }

  [Obsolete("После удаления ссылок на CurrentSiteCode и PortalEnabled будет удалено. Вместо этого метода надо вызывать Portal.Init")]
  public static void Init([CanBeNull] ISitesCacheService srv)
  {
  }

  [Obsolete("Use Portal.Enabled")]
  public static bool PortalEnabled
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => Portal.Enabled;
  }

  /// <summary>Код текущего узла</summary>
  [Obsolete("Use Portal.CurrentSiteCode")]
  public static char CurrentSiteCode => Portal.CurrentSiteCode;

  /// <summary>Код текущего узла, для более удобного обращения - нестатический клон статического CurrentSiteCode</summary>
  public char CurrentSite => Portal.CurrentSiteCode;

  public char Creator
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._value.Length <= 0 ? Portal.CurrentSiteCode : this._value[0];
    }
  }

  public char Owner
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._value.Length <= 1 ? Portal.CurrentSiteCode : this._value[1];
    }
    set
    {
      if (this._value.Length > 1)
        this._value[1] = value;
      else
        this.Value = new string(new char[2]
        {
          this.Creator,
          value
        });
    }
  }

  public char CompositionOwner
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._value.Length <= 2 ? Portal.CurrentSiteCode : this._value[2];
    }
    set
    {
      if (this._value.Length > 2)
        this._value[2] = value;
      else
        this.Value = new string(new char[3]
        {
          this.Creator,
          this.Owner,
          value
        });
    }
  }
}
