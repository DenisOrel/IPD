
// Type: Intermech.Search.ObjectListFilters.IObjectListFiltersClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Search.ObjectListFilters;

public interface IObjectListFiltersClientService
{
  ObjectListFilter[] GetAllFilters();

  ObjectListFilter[] GetFiltersForObjectType(int objectTypeID);

  void RefreshCache();

  ObjectListFilter CreateNewFilter(ObjectListFilterType type);

  void RemoveFilter(long objectVersionID);
}
