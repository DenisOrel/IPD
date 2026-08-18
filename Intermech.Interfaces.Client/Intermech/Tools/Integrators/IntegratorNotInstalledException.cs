// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IntegratorNotInstalledException
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Это исключение сбрасывается в том случае, если в базе отсутствует объект интегратора.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="integratorName">Название интегратора</param>
public sealed class IntegratorNotInstalledException(string integratorName) : IntegratorException(integratorName, IntegratorNotInstalledException.MakeMessage(integratorName))
{
  private static string MakeMessage(string integratorName)
  {
    return integratorName != null ? string.Format(LocalizationHolder.rm.GetString("SR_164"), (object) integratorName) : throw new ArgumentNullException(nameof (integratorName));
  }
}
