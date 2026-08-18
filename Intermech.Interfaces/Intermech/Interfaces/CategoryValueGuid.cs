
// Type: Intermech.Interfaces.CategoryValueGuid
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    [Serializable]
    public class CategoryValueGuid : ModificationEvent
    {
      /// <summary>Гуид объекта данной категории</summary>
      public Guid CategoryGuid;

      public CategoryValueGuid(
        int aCategoryType,
        long aCategoryID,
        ActionType anActionID,
        Guid categoryGUID,
        int metadataTypeID)
        : base(aCategoryType, aCategoryID, anActionID, metadataTypeID)
      {
        this.CategoryGuid = categoryGUID;
      }

      public override int GetHashCode() => this.CategoryGuid.GetHashCode() ^ base.GetHashCode();

      public override bool Equals(object obj)
      {
        if (!(obj is CategoryValueGuid))
          return false;
        CategoryValueGuid categoryValueGuid = (CategoryValueGuid) obj;
        return categoryValueGuid.GetHashCode() == this.GetHashCode() && this.CategoryType == categoryValueGuid.CategoryType && this.CategoryID == categoryValueGuid.CategoryID && this.ActionID == categoryValueGuid.ActionID && this.CategoryGuid == categoryValueGuid.CategoryGuid;
      }
    }
}
