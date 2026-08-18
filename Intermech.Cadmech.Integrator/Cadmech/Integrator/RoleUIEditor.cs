// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.RoleUIEditor
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class RoleUIEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return (!(context.PropertyDescriptor.PropertyType == typeof (UserRoleMarker)) ? 0 : (!context.PropertyDescriptor.IsReadOnly ? 1 : 0)) == 0 ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    UserRoleMarker userRoleMarker = (UserRoleMarker) value;
    int rolesTypeId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      rolesTypeId = sessionKeeper.Session.IdentHelper.RolesTypeID;
    long[] numArray = SelectionWindow.SelectObjects("Роли пользователей", "Выберите роль пользователя, на которую будут распространяться настройки", rolesTypeId, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray != null && numArray.Length != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(numArray[0], true);
        userRoleMarker = new UserRoleMarker(dbObject.ObjectGUID, dbObject.Caption);
      }
    }
    return (object) userRoleMarker;
  }
}
