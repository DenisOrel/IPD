// Decompiled with JetBrains decompiler
// Type: Intermech.Project.CustomCategory
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;

#nullable disable
namespace Intermech.Project;

internal class CustomCategory([NotNull] string category) : CustomCategoryBase(Localization.Resources, category)
{
}
