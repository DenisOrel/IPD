// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CustomDescription
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Extensions;

internal class CustomDescription([NotNull, NotWhitespace] string description) : CustomDescriptionBase(Localization.AttributeResources, description)
{
}
