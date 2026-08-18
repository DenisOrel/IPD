
// Type: Intermech.Interfaces.Client.OwnerIdAllocator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис выделения пакетов для временных правил подбора.
/// </summary>
internal static class OwnerIdAllocator
{
  private const int ItemsLimit = 60;
  private static readonly OwnerIdAllocator.ReusableItems availableItems = new OwnerIdAllocator.ReusableItems();
  private static readonly List<OwnerIdAllocator.UsedItems> allocatedItems = new List<OwnerIdAllocator.UsedItems>(60);

  [MethodImpl(MethodImplOptions.Synchronized)]
  public static VersionsRulePackage Allocate()
  {
    VersionsRulePackage package = !OwnerIdAllocator.availableItems.NoMoreItems || OwnerIdAllocator.Deallocate() != 0 ? new VersionsRulePackage(OwnerIdAllocator.availableItems.Allocate()) : throw new FaultException(LocalizationHolder.rm.GetString("Client.Core_1574"));
    OwnerIdAllocator.allocatedItems.Add(new OwnerIdAllocator.UsedItems(package));
    return package;
  }

  private static int Deallocate()
  {
    int num = 0;
    int index = 0;
    while (index < OwnerIdAllocator.allocatedItems.Count)
    {
      OwnerIdAllocator.UsedItems allocatedItem = OwnerIdAllocator.allocatedItems[index];
      if (allocatedItem.IsDead)
      {
        OwnerIdAllocator.allocatedItems.RemoveAt(index);
        OwnerIdAllocator.availableItems.Free(allocatedItem.Value);
        ++num;
      }
      else
        ++index;
    }
    return num;
  }

  private sealed class ReusableItems
  {
    private readonly string ownerIdPrefix;
    private readonly LinkedList<string> items;

    public ReusableItems()
    {
      this.ownerIdPrefix = "DynamicRule.";
      this.items = new LinkedList<string>((IEnumerable<string>) this.FindDynamicItems());
      if (this.items.Count < 60)
      {
        this.GenerateMissingItems();
      }
      else
      {
        if (this.items.Count <= 60)
          return;
        this.DeleteExtraItems();
      }
    }

    private string[] FindDynamicItems()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return Array.FindAll<string>(((IVersionRulesCacheService) sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService))).GetFiltrationSettingsList((object) sessionKeeper.Session.SessionGUID), (Predicate<string>) (item => item.StartsWith(this.ownerIdPrefix)));
    }

    private void GenerateMissingItems()
    {
      while (this.items.Count < 60)
        this.items.AddLast(this.ownerIdPrefix + Guid.NewGuid().ToString("B"));
    }

    private void DeleteExtraItems()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IVersionRulesCacheService customService = (IVersionRulesCacheService) sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService));
        while (this.items.Count > 60)
        {
          string OwnerID = this.items.Last.Value;
          this.items.RemoveLast();
          try
          {
            customService.DeleteRuleTuning((object) sessionKeeper.Session.SessionGUID, OwnerID);
          }
          catch
          {
          }
        }
      }
    }

    public bool NoMoreItems => this.items.Count == 0;

    public void Free(string value) => this.items.AddFirst(value);

    public string Allocate()
    {
      string str = this.items.First.Value;
      this.items.RemoveFirst();
      return str;
    }
  }

  private sealed class UsedItems
  {
    private readonly string value;
    private readonly WeakReference usageRef;

    public UsedItems(VersionsRulePackage package)
    {
      this.value = package.OwnerId;
      this.usageRef = new WeakReference((object) package, false);
    }

    public string Value => this.value;

    public bool IsDead => !this.usageRef.IsAlive;
  }
}
