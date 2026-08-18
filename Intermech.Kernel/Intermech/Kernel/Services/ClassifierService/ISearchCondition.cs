// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.ISearchCondition
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;


namespace Intermech.Kernel.Services.ClassifierService;

internal interface ISearchCondition
{
  ConditionStructure[] GetConditions(IUserSession session, int objectType);
}
