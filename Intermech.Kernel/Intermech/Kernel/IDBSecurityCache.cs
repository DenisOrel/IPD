// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IDBSecurityCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Kernel;

public interface IDBSecurityCache
{
  void ClearCache();

  void ClearCache(CategoryValue aCategory);

  void AddToCache(CategoryValue aCategory, AccessInfo accessResult);

  bool CheckAccess(CategoryValue aCategory, bool aDefaultAccess, bool aThrowACException);

  void ClearCacheForGroup(long aGroup, CategoryValue aCategory);

  AccessInfo CheckAccessInCache(CategoryValue aCategory);

  void ClearCacheIfNeed();

  void ClearCategoryCache(
    int categoryType,
    long categoryId,
    Dictionary<ActionType, bool> accessActions);
}
