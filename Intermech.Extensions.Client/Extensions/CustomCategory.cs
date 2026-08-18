// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CustomCategory
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Extensions;

internal class CustomCategory([NotNull] string category) : CustomCategoryBase(Localization.AttributeResources, category)
{
}
