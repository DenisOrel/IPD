// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadProgIdProvider
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Runtime.ComInterop;
using Intermech.Win32;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadProgIdProvider : ComObjectProvider
{
  private static readonly RegistryKeyLocation acadSoftwareRoot = new RegistryKeyLocation(RegistryHive.CurrentUser, "Software\\Autodesk\\AutoCAD");
  private static readonly Regex acadRevisionPattern = new Regex("^R(?<major>\\d+)\\.(?<minor>\\d+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
  private string versionIndependentProgId;

  public AcadProgIdProvider(string versionIndependentProgId, bool inprocessServer)
    : base(inprocessServer)
  {
    this.versionIndependentProgId = !string.IsNullOrEmpty(versionIndependentProgId) ? versionIndependentProgId : throw new ArgumentException("Не задан ProgID COM-объекта приложения.", nameof (versionIndependentProgId));
  }

  public override Type GetComType(bool throwOnError)
  {
    string activeProgId = this.TryGetActiveProgId();
    if (activeProgId != null)
      return Type.GetTypeFromProgID(activeProgId, throwOnError);
    if (throwOnError)
      throw new COMException($"COM-класс '{this.versionIndependentProgId}' не зарегистрирован на этом компьютере. Возможно, соответствующее приложение не установлено.");
    return (Type) null;
  }

  public override object TryGetRunningInstance()
  {
    try
    {
      string activeProgId = this.TryGetActiveProgId();
      return activeProgId != null ? Marshal.GetActiveObject(activeProgId) : (object) null;
    }
    catch (COMException ex)
    {
      return (object) null;
    }
  }

  public override bool IsRegistered()
  {
    string activeProgId = this.TryGetActiveProgId();
    return activeProgId != null && Type.GetTypeFromProgID(activeProgId, false) != (Type) null;
  }

  private string TryGetActiveProgId()
  {
    AcadProgIdProvider.AcadRevision? activeAcadRevision = this.TryGetActiveAcadRevision();
    if (!activeAcadRevision.HasValue)
      return (string) null;
    return activeAcadRevision.Value.Minor != 0 ? $"{this.versionIndependentProgId}.{activeAcadRevision.Value.Major}.{activeAcadRevision.Value.Minor}" : $"{this.versionIndependentProgId}.{activeAcadRevision.Value.Major}";
  }

  private AcadProgIdProvider.AcadRevision? TryGetActiveAcadRevision()
  {
    using (RegistryBuilder registryBuilder = new RegistryBuilder(AcadProgIdProvider.acadSoftwareRoot, false))
    {
      if (registryBuilder.KeyExists)
      {
        string input = registryBuilder.GetValue("CurVer") as string;
        if (!string.IsNullOrEmpty(input))
        {
          Match match = AcadProgIdProvider.acadRevisionPattern.Match(input);
          if (match.Success)
            return new AcadProgIdProvider.AcadRevision?(new AcadProgIdProvider.AcadRevision(int.Parse(match.Groups["major"].Value), int.Parse(match.Groups["minor"].Value)));
        }
      }
    }
    return new AcadProgIdProvider.AcadRevision?();
  }

  private struct AcadRevision(int major, int minor)
  {
    private int major = major;
    private int minor = minor;

    public int Major => this.major;

    public int Minor => this.minor;
  }
}
