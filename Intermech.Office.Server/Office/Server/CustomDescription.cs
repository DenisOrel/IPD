// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.CustomDescription
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using System.ComponentModel;

#nullable disable
namespace Intermech.Office.Server;

internal class CustomDescription : DescriptionAttribute
{
  public CustomDescription([NotNull] string description)
  {
    this.DescriptionValue = Localization.GetAttributeString(description);
  }
}
