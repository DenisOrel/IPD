
// Type: Intermech.Search.EditingContexts.IEditingContextServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.EditingContexts
{
    public interface IEditingContextServerService
    {
      EditingContext FindEditingContext(
        Guid userSessionGuid,
        FindEditingContextParams findEditingContextParams);

      EditingContext[] FindLinkedEdintingContexts(
        Guid userSessionGuid,
        FindEditingContextParams findEditingContextParams);

      void SaveEditingContext(Guid userSessionGuid, SaveEditingContextParams saveEditingContextParams);

      AddObjectsToEditingContextResult AddObjectsToEditingContext(
        Guid userSessionGuid,
        AddObjectsToEditingContextParams addObjectsToEditingContextParams);

      void ReplaceVersionInEditingContext(
        Guid userSessionGuid,
        long objectVersionID,
        long replacementVersionID,
        long editingContextVersionID);

      _Object[] FindObjectsForAddToEditingContext(
        Guid userSessionGuid,
        AddObjectsToEditingContextParams addObjectsToEditingContextParams);

      bool CheckEditingContextEditRights(Guid userSessionGuid, long editingContextVersionID);

      void RemoveNotVersionedObjectsFromAllEditingContexts(Guid userSessionGuid);

      long[] FindProductsForDocuments(Guid userSessionGuid, long[] documentVersionIds);
    }
}
