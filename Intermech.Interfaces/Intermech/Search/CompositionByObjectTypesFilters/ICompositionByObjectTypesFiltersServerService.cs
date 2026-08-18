
// Type: Intermech.Search.CompositionByObjectTypesFilters.ICompositionByObjectTypesFiltersServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.CompositionByObjectTypesFilters
{
    public interface ICompositionByObjectTypesFiltersServerService
    {
      void AddFiltersToObjectComposition(
        Guid userSessionGuid,
        long[] filterVersionIds,
        long objectVersionID);

      void SaveFilter(
        Guid userSessionGuid,
        long filterVersionID,
        CompositionByObjectTypesFilter filter);

      CompositionByObjectTypesFilter FindFilterByVersionID(Guid userSessionGuid, long filterVersionID);

      void RemoveFilterFromObjectComposition(
        Guid userSessionGuid,
        long filterVersionID,
        long objectVersionID);

      string CreateTextFromFiltersInObjectComposition(Guid userSessionGuid, long objectVersionID);

      long GetCurrentUserConfigurationVersionID(Guid userSessionGuid);

      void CreateFiltersAndAddToCurrentUserConfigurationComposition(
        Guid userSessionGuid,
        CompositionByObjectTypesFilter[] filters);

      CompositionByObjectTypesFilter[] GetFiltersForCurrentUser(Guid userSessionGuid);

      CompositionByObjectTypesFilter[] GetFiltersForCurrentRole(Guid userSessionGuid);

      bool IsFilterWithNameExistsInObjectComposition(
        Guid userSessionGuid,
        string filterName,
        long objectVersionID);

      void CreateFiltersAndAddToObjectComposition(
        Guid userSessionGuid,
        CompositionByObjectTypesFilter[] filters,
        long objectVersionID);
    }
}
