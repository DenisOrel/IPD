// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.EditingContextFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Data.Filters;

public sealed class EditingContextFilter(IUserSession userSession) : FilterBase(userSession)
{
  private EditingContextsObjectContainer _editingContext;
  private static readonly TimeSpan OneDay = new TimeSpan(0, 23, 59, 59, 999);

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
          AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_MODIFICATION_ID,
          AttributeSource = AttributeSourceTypes.Object
        }
      };
    }
  }

  public override void Configure(FilterOptions options)
  {
    base.Configure(options);
    this.SetEditingContext(options.EditingContextVersionID);
  }

  private bool ApplyInternal(RelationObjectBase relationObject)
  {
    if (relationObject == null)
      throw new ArgumentNullException(nameof (relationObject));
    if (this._editingContext != null)
    {
      if (!this._editingContext.SimpleContext)
      {
        if (Math.Abs(relationObject.Object.ModificationID) == Math.Abs(this._editingContext.ModificationID))
        {
          if (!this._editingContext.ExistsLinkedVersion(relationObject.Object.VersionID))
            this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", relationObject.Object, (short) 10);
          else
            this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", relationObject.Object, (short) 11);
          return true;
        }
      }
      else if (this._editingContext.Objects != null && this._editingContext.Objects.Any<EditingContextsObjectVersion>((Func<EditingContextsObjectVersion, bool>) (o => Math.Abs(o.F_OBJECT_ID) == Math.Abs(relationObject.Object.VersionID))))
      {
        this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", relationObject.Object, (short) 10);
        return true;
      }
    }
    return false;
  }

  private void SetEditingContext(long editingContextVersionID)
  {
    this._editingContext = (this.UserSession.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).GetEditingContextsObject((object) this.UserSession, editingContextVersionID, false, true);
  }
}
