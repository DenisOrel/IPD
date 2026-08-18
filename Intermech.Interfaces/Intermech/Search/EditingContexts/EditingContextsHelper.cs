
// Type: Intermech.Search.EditingContexts.EditingContextsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.EditingContexts
{
    public static class EditingContextsHelper
    {
      public static bool IsEditingContextObjectTypeID(int objectTypeID)
      {
        return objectTypeID != -1 ? MetaDataHelper.IsObjectTypeEditingContext(objectTypeID) : throw new ArgumentException();
      }

      public static bool IsAnyEditingContextObjectTypeID(IEnumerable<int> objectTypeIds)
      {
        if (objectTypeIds == null)
          throw new ArgumentException();
        return objectTypeIds.Where<int>((Func<int, bool>) (o => EditingContextsHelper.IsEditingContextObjectTypeID(o))).Count<int>() != 0;
      }

      public static bool IsSimpleEditingContextObjectTypeID(int objectTypeID)
      {
        return objectTypeID != -1 ? MetaDataHelper.IsSimpleEditingContext(objectTypeID) : throw new ArgumentException();
      }

      public static bool IsEcoObjectTypeID(int objectTypeID)
      {
        if (objectTypeID == -1)
          throw new ArgumentException();
        return objectTypeID == EditingContextConstants.EcoObjectTypeID || MetaDataHelper.IsObjectTypeChildOf(objectTypeID, EditingContextConstants.EcoObjectTypeID);
      }

      public static AddObjectsToEditingContextResult AddObjectsToEditingContext(
        _Object[] objects,
        EditingContext editingContext,
        EditingContext[] linkedEditingContexts)
      {
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        if (objects == null)
          throw new ArgumentNullException(nameof (objects));
        AddObjectsToEditingContextResult editingContext1 = new AddObjectsToEditingContextResult();
        foreach (_Object @object in objects)
        {
          EditingContextsLogError editingContext2 = EditingContextsHelper.CheckObjectForAddToEditingContext(@object, editingContext, linkedEditingContexts);
          if (editingContext2 == EditingContextsLogError.None)
          {
            EditingContextItem editingContextItem = new EditingContextItem(@object);
            editingContext.Items.Add(editingContextItem);
            ++editingContext1.AddedObjectsCount;
          }
          else
          {
            EditingContextsLogEntry contextsLogEntry = new EditingContextsLogEntry(editingContext2, @object.VersionID);
            editingContext1.EditingContextLogEnties.Add(contextsLogEntry);
            ++editingContext1.SkippedObjectsCount;
          }
        }
        return editingContext1;
      }

      public static EditingContextsLogError CheckObjectForAddToEditingContext(
        _Object @object,
        EditingContext editingContext,
        EditingContext[] linkedEditingContexts)
      {
        if (@object == null)
          throw new ArgumentNullException("@object");
        if (ObjectHelper.IsUnknownObjectVersionID(@object.VersionID) || ObjectHelper.IsUnknownObjectID(@object.ID) || ObjectTypeHelper.IsUnknownObjectTypeID(@object.TypeID))
          throw new ArgumentException();
        if (editingContext == null)
          throw new ArgumentNullException(nameof (editingContext));
        if (editingContext.Items.Any<EditingContextItem>((Func<EditingContextItem, bool>) (o => Math.Abs(o.Object.VersionID) == Math.Abs(@object.VersionID))))
          return EditingContextsLogError.ExistsVersion;
        if (editingContext.Items.Any<EditingContextItem>((Func<EditingContextItem, bool>) (o => o.Object.ID == @object.ID)) || linkedEditingContexts != null && ((IEnumerable<EditingContext>) linkedEditingContexts).Any<EditingContext>((Func<EditingContext, bool>) (o => o.Items.Any<EditingContextItem>((Func<EditingContextItem, bool>) (oo => oo.Object.ID == @object.ID)))))
          return EditingContextsLogError.ExistsAnotherVersionLinked;
        if (EditingContextsHelper.IsEditingContextObjectTypeID(@object.TypeID))
          return EditingContextsLogError.IsEditingContext;
        if (!ObjectTypeHelper.IsVersionedObjectTypeID(@object.TypeID))
          return EditingContextsLogError.NonversionObject;
        return !ObjectHelper.IsUnknownObjectModificationID(@object.ModificationID) && editingContext.LinkedEditingContextID != @object.ModificationID && !EditingContextsHelper.IsSimpleEditingContextObjectTypeID(editingContext.Object.TypeID) ? EditingContextsLogError.ExistsAnotherVersion : EditingContextsLogError.None;
      }
    }
}
