// Decompiled with JetBrains decompiler
// Type: Intermech.Project.CustomDisplayName
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;

#nullable disable
namespace Intermech.Project;

internal class CustomDisplayName : CustomDisplayNameBase
{
  protected internal CustomDisplayName([NotNull, NotWhitespace] string displayName)
    : base(Localization.Resources, displayName)
  {
  }
}
