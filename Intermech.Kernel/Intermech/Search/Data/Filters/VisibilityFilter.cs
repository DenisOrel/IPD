// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.VisibilityFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.ObjectsVisiblity;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Filters;

public sealed class VisibilityFilter(Intermech.Kernel.UserSession userSession) : FilterBase((IUserSession) userSession)
{
  public override bool Apply(Applicability applicability)
  {
    return this.ApplyInternal((RelationObjectBase) applicability);
  }

  public override bool Apply(CompositionPart compositionPart)
  {
    return this.ApplyInternal((RelationObjectBase) compositionPart);
  }

  public override List<ColumnDescriptor> Columns
  {
    get
    {
      return new List<ColumnDescriptor>()
      {
        new ColumnDescriptor()
        {
          AttributeID = (object) Constants.ObjectVisibilityAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_OWNER_ID,
          AttributeSource = AttributeSourceTypes.Object
        }
      };
    }
  }

  public override void Configure(FilterOptions options) => base.Configure(options);

  private bool ApplyInternal(RelationObjectBase relationObject)
  {
    if (relationObject == null)
      throw new ArgumentNullException(nameof (relationObject));
    if (!(relationObject.Object.Attributes.GetAttributeValue(Constants.ObjectVisibilityAttributeTypeID) is string attributeValue))
      return true;
    ObjectsVisibility settings = new ObjectsVisibility(attributeValue);
    if (settings.Rights.ContainsKey(this.UserSession.IdentHelper.AllUsersGroupID) && settings.Rights[this.UserSession.IdentHelper.AllUsersGroupID] == ObjectsVisibilityFlags.Hidden)
      this.SetStatuses(ObjectsVisibilityConstants.ObjectsVisiblityModuleGuid, relationObject.Object, 2);
    else
      this.SetStatuses(ObjectsVisibilityConstants.ObjectsVisiblityModuleGuid, relationObject.Object, 1);
    return !this.UserSession.EnabledVisibilityFiltration || this.UserSession.IsAdmin || DBRecordSet.ObjectsVisibilityFiltration.Visible(this.UserSession, settings, relationObject.Object.OwnerVersionID);
  }
}
