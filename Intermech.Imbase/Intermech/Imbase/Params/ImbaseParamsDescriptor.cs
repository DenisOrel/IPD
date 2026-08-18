// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.ImbaseParamsDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.ComponentModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase.Params.CommonParams;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.Params;

internal class ImbaseParamsDescriptor : ICustomTypeDescriptor
{
  private ImbaseParamsContainer _imbaseParamsContainer;
  [NonSerialized]
  private PropertyDescriptorCollection _pdc;

  private PropertyDescriptorCollection CreatePdc(Attribute[] attributes)
  {
    if (this._imbaseParamsContainer == null)
      return (PropertyDescriptorCollection) null;
    ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, false);
    List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties((object) this._imbaseParamsContainer.CommonParams, attributes, true);
    PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties((object) this._imbaseParamsContainer.UserParams, attributes, true);
    System.ComponentModel.PropertyDescriptor propDesc1 = properties1["DeleteRecordMode"];
    if (propDesc1 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc1);
      if (service == null || !service.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (EnumCustomConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.TableEditor.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.CommonParams.DeleteRecordMode.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.CommonParams.DeleteRecordMode.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc2 = properties1["AnalizeHiddenRecords"];
    if (propDesc2 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc2);
      if (service == null || !service.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.TableView.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.CommonParams.AnalizeHiddenRecords.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.CommonParams.AnalizeHiddenRecords.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc3 = properties1["UseExtendedSecurityCheckForIndexes"];
    if (propDesc3 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc3);
      if (service == null || !service.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.IndexesParams.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.CommonParams.UseExtendedSecurityCheck.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.CommonParams.UseExtendedSecurityCheck.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc4 = properties1["DenyFewLinksForSameTable"];
    if (propDesc4 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc4);
      if (service == null || !service.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.CreateLinks.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.CommonParams.DenyFewLinksForSameTable.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.CommonParams.DenyFewLinksForSameTable.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc5 = properties1["CheckApplicabilityBeforeCreateComposition"];
    if (propDesc5 != null)
    {
      CustomPropertyDescriptor propertyDescriptor1 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc5);
      if (service == null || !service.IsAdmin)
        propertyDescriptor1.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor1.AddAttribute((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
      propertyDescriptor1.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomYesNoTypeConverter)));
      propertyDescriptor1.AddAttribute((Attribute) new CustomCategory("Imbase.CreateRelationAndLink.CategoryName"));
      propertyDescriptor1.AddAttribute((Attribute) new CustomDisplayName("Imbase.CommonParams.CheckApplicabilityBeforeCreateComposition.Name"));
      propertyDescriptor1.AddAttribute((Attribute) new CustomDescription("Imbase.CommonParams.CheckApplicabilityBeforeCreateComposition.Description"));
      if (this._imbaseParamsContainer.CommonParams.CheckApplicabilityBeforeCreateComposition)
      {
        System.ComponentModel.PropertyDescriptor propDesc6 = properties1["FolderApplicabilityIcons"];
        if (propDesc6 != null)
        {
          CustomPropertyDescriptor propertyDescriptor2 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc6);
          propertyDescriptor2.AddAttribute((Attribute) new ReadOnlyAttribute(true));
          propertyDescriptor2.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomTypeConverter)));
          propertyDescriptor2.AddAttribute((Attribute) new CustomDisplayName("Imbase.FolderImages.Name"));
          propertyDescriptor2.AddAttribute((Attribute) new CustomDescription("Imbase.FolderImages.Description"));
          PropertyDescriptorCollection properties3 = TypeDescriptor.GetProperties((object) this._imbaseParamsContainer.CommonParams.FolderApplicabilityIcons, attributes, true);
          System.ComponentModel.PropertyDescriptor propDesc7 = properties3["NoRestrictionImage"];
          if (propDesc7 != null)
          {
            CustomPropertyDescriptor propertyDescriptor3 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.FolderApplicabilityIcons, propDesc7);
            if (service == null || !service.IsAdmin)
              propertyDescriptor3.AddAttribute((Attribute) new ReadOnlyAttribute(true));
            propertyDescriptor3.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomIconConverter)));
            propertyDescriptor3.AddAttribute((Attribute) new CustomCategory("Imbase.FolderImages.CategoryName"));
            propertyDescriptor3.AddAttribute((Attribute) new CustomDisplayName("Imbase.NoRestriction.Name"));
            propertyDescriptor3.AddAttribute((Attribute) new CustomDescription("Imbase.NoRestriction.Description"));
            propertyDescriptor2.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor3);
          }
          System.ComponentModel.PropertyDescriptor propDesc8 = properties3["DenyAddRecordImage"];
          if (propDesc8 != null)
          {
            CustomPropertyDescriptor propertyDescriptor4 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.FolderApplicabilityIcons, propDesc8);
            if (service == null || !service.IsAdmin)
              propertyDescriptor4.AddAttribute((Attribute) new ReadOnlyAttribute(true));
            propertyDescriptor4.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomIconConverter)));
            propertyDescriptor4.AddAttribute((Attribute) new CustomCategory("Imbase.FolderImages.CategoryName"));
            propertyDescriptor4.AddAttribute((Attribute) new CustomDisplayName("Imbase.DenyAddRecord.Name"));
            propertyDescriptor4.AddAttribute((Attribute) new CustomDescription("Imbase.DenyAddRecord.Description"));
            propertyDescriptor2.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor4);
          }
          System.ComponentModel.PropertyDescriptor propDesc9 = properties3["DenyAddObjectImage"];
          if (propDesc9 != null)
          {
            CustomPropertyDescriptor propertyDescriptor5 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.FolderApplicabilityIcons, propDesc9);
            if (service == null || !service.IsAdmin)
              propertyDescriptor5.AddAttribute((Attribute) new ReadOnlyAttribute(true));
            propertyDescriptor5.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomIconConverter)));
            propertyDescriptor5.AddAttribute((Attribute) new CustomCategory("Imbase.FolderImages.CategoryName"));
            propertyDescriptor5.AddAttribute((Attribute) new CustomDisplayName("Imbase.DenyAddObject.Name"));
            propertyDescriptor5.AddAttribute((Attribute) new CustomDescription("Imbase.DenyAddObject.Description"));
            propertyDescriptor2.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor5);
          }
          System.ComponentModel.PropertyDescriptor propDesc10 = properties3["DenyAllImage"];
          if (propDesc10 != null)
          {
            CustomPropertyDescriptor propertyDescriptor6 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.FolderApplicabilityIcons, propDesc10);
            if (service == null || !service.IsAdmin)
              propertyDescriptor6.AddAttribute((Attribute) new ReadOnlyAttribute(true));
            propertyDescriptor6.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomIconConverter)));
            propertyDescriptor6.AddAttribute((Attribute) new CustomCategory("Imbase.FolderImages.CategoryName"));
            propertyDescriptor6.AddAttribute((Attribute) new CustomDisplayName("Imbase.DenyAll.Name"));
            propertyDescriptor6.AddAttribute((Attribute) new CustomDescription("Imbase.DenyAll.Description"));
            propertyDescriptor2.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor6);
          }
          propertyDescriptor1.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor2);
        }
        System.ComponentModel.PropertyDescriptor propDesc11 = properties2["TableRecordsApplicabilityColors"];
        if (propDesc11 != null)
        {
          CustomPropertyDescriptor propertyDescriptor7 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc11);
          propertyDescriptor7.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomTypeConverter)));
          propertyDescriptor7.AddAttribute((Attribute) new CustomDisplayName("Imbase.RecordColors.Name"));
          propertyDescriptor7.AddAttribute((Attribute) new CustomDescription("Imbase.RecordColors.Description"));
          PropertyDescriptorCollection properties4 = TypeDescriptor.GetProperties((object) this._imbaseParamsContainer.UserParams.TableRecordsApplicabilityColors, attributes, true);
          System.ComponentModel.PropertyDescriptor propDesc12 = properties4["NoResrictionsRecColor"];
          if (propDesc12 != null)
          {
            CustomPropertyDescriptor propertyDescriptor8 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams.TableRecordsApplicabilityColors, propDesc12);
            propertyDescriptor8.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ColorConverter)));
            propertyDescriptor8.AddAttribute((Attribute) new CustomCategory("Imbase.RecordColors.Name"));
            propertyDescriptor8.AddAttribute((Attribute) new CustomDisplayName("Imbase.NoRestriction.Name"));
            propertyDescriptor8.AddAttribute((Attribute) new CustomDescription("Imbase.NoRestriction.Description"));
            propertyDescriptor7.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor8);
          }
          System.ComponentModel.PropertyDescriptor propDesc13 = properties4["DenyAddRecordRecColor"];
          if (propDesc13 != null)
          {
            CustomPropertyDescriptor propertyDescriptor9 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams.TableRecordsApplicabilityColors, propDesc13);
            propertyDescriptor9.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ColorConverter)));
            propertyDescriptor9.AddAttribute((Attribute) new CustomCategory("Imbase.RecordColors.Name"));
            propertyDescriptor9.AddAttribute((Attribute) new CustomDisplayName("Imbase.DenyAddRecord.Name"));
            propertyDescriptor9.AddAttribute((Attribute) new CustomDescription("Imbase.DenyAddRecord.Description"));
            propertyDescriptor7.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor9);
          }
          System.ComponentModel.PropertyDescriptor propDesc14 = properties4["DenyAddObjectRecColor"];
          if (propDesc14 != null)
          {
            CustomPropertyDescriptor propertyDescriptor10 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams.TableRecordsApplicabilityColors, propDesc14);
            propertyDescriptor10.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ColorConverter)));
            propertyDescriptor10.AddAttribute((Attribute) new CustomCategory("Imbase.RecordColors.Name"));
            propertyDescriptor10.AddAttribute((Attribute) new CustomDisplayName("Imbase.DenyAddObject.Name"));
            propertyDescriptor10.AddAttribute((Attribute) new CustomDescription("Imbase.DenyAddObject.Description"));
            propertyDescriptor7.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor10);
          }
          System.ComponentModel.PropertyDescriptor propDesc15 = properties4["DenyAllRecColor"];
          if (propDesc15 != null)
          {
            CustomPropertyDescriptor propertyDescriptor11 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams.TableRecordsApplicabilityColors, propDesc15);
            propertyDescriptor11.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ColorConverter)));
            propertyDescriptor11.AddAttribute((Attribute) new CustomCategory("Imbase.RecordColors.Name"));
            propertyDescriptor11.AddAttribute((Attribute) new CustomDisplayName("Imbase.DenyAll.Name"));
            propertyDescriptor11.AddAttribute((Attribute) new CustomDescription("Imbase.DenyAll.Description"));
            propertyDescriptor7.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor11);
          }
          propertyDescriptor1.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor7);
        }
      }
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor1);
    }
    System.ComponentModel.PropertyDescriptor propDesc16 = properties1["NotExpandableAttributes"];
    if (propDesc16 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc16);
      if (service == null || !service.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (AttributeListTypeConverter)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (AttributeListEditor), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSynchronizationParams.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.NotExpandableAttributes.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.NotExpandableAttributes.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc17 = properties1["SkipAttributes"];
    if (propDesc17 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams, propDesc17);
      if (service == null || !service.IsAdmin)
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (AttributeListTypeConverter)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (AttributeListEditor), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSynchronizationParams.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.SkipAttributes.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.SkipAttributes.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    if (properties1["ImbaseSyncParams"] != null)
    {
      PropertyDescriptorCollection properties5 = TypeDescriptor.GetProperties((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams, attributes, true);
      System.ComponentModel.PropertyDescriptor propDesc18 = properties5["SourceDBParams"];
      if (propDesc18 != null)
      {
        CustomPropertyDescriptor propertyDescriptor12 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams, propDesc18);
        propertyDescriptor12.AddAttribute((Attribute) new ReadOnlyAttribute(true));
        propertyDescriptor12.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomTypeConverter)));
        propertyDescriptor12.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
        propertyDescriptor12.AddAttribute((Attribute) new CustomDisplayName("Imbase.SourceDBParams.Name"));
        propertyDescriptor12.AddAttribute((Attribute) new CustomDescription("Imbase.SourceDBParams.Description"));
        PropertyDescriptorCollection properties6 = TypeDescriptor.GetProperties((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams, attributes, true);
        System.ComponentModel.PropertyDescriptor propDesc19 = properties6["BaseType"];
        if (propDesc19 != null)
        {
          CustomPropertyDescriptor propertyDescriptor13 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams, propDesc19);
          if (service == null || !service.IsAdmin)
            propertyDescriptor13.AddAttribute((Attribute) new ReadOnlyAttribute(true));
          propertyDescriptor13.AddAttribute((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
          propertyDescriptor13.AddAttribute((Attribute) new TypeConverterAttribute(typeof (EnumCustomConverter)));
          propertyDescriptor13.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
          propertyDescriptor13.AddAttribute((Attribute) new CustomDisplayName("Imbase.BaseType.Name"));
          propertyDescriptor13.AddAttribute((Attribute) new CustomDescription("Imbase.BaseType.Description"));
          propertyDescriptor12.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor13);
        }
        System.ComponentModel.PropertyDescriptor propDesc20 = properties6["ServerName"];
        if (propDesc20 != null)
        {
          CustomPropertyDescriptor propertyDescriptor14 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams, propDesc20);
          if (service == null || !service.IsAdmin)
            propertyDescriptor14.AddAttribute((Attribute) new ReadOnlyAttribute(true));
          propertyDescriptor14.AddAttribute((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
          propertyDescriptor14.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
          switch (this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams.BaseType)
          {
            case BaseType.Interbase:
              propertyDescriptor14.AddAttribute((Attribute) new CustomDisplayName("Imbase.ServerNameSQL.Name"));
              propertyDescriptor14.AddAttribute((Attribute) new CustomDescription("Imbase.ServerNameSQL.Description"));
              break;
            case BaseType.MSSQL:
              propertyDescriptor14.AddAttribute((Attribute) new CustomDisplayName("Imbase.ServerNameSQL.Name"));
              propertyDescriptor14.AddAttribute((Attribute) new CustomDescription("Imbase.ServerNameSQL.Description"));
              break;
            case BaseType.Oracle:
              propertyDescriptor14.AddAttribute((Attribute) new CustomDisplayName("Imbase.ServerNameOra.Name"));
              propertyDescriptor14.AddAttribute((Attribute) new CustomDescription("Imbase.ServerNameOra.Description"));
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
          propertyDescriptor12.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor14);
        }
        System.ComponentModel.PropertyDescriptor propDesc21 = properties6["DataBaseName"];
        if (propDesc21 != null)
        {
          CustomPropertyDescriptor propertyDescriptor15 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams, propDesc21);
          if (service == null || !service.IsAdmin)
            propertyDescriptor15.AddAttribute((Attribute) new ReadOnlyAttribute(true));
          propertyDescriptor15.AddAttribute((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
          propertyDescriptor15.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
          switch (this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams.BaseType)
          {
            case BaseType.Interbase:
              propertyDescriptor15.AddAttribute((Attribute) new CustomDisplayName("Imbase.DBNameInterbase.Name"));
              propertyDescriptor15.AddAttribute((Attribute) new CustomDescription("Imbase.DBNameInterbase.Description"));
              goto case BaseType.Oracle;
            case BaseType.MSSQL:
              propertyDescriptor15.AddAttribute((Attribute) new CustomDisplayName("Imbase.DBNameSQL.Name"));
              propertyDescriptor15.AddAttribute((Attribute) new CustomDescription("Imbase.DBNameSQL.Description"));
              goto case BaseType.Oracle;
            case BaseType.Oracle:
              if (this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams.BaseType != BaseType.Oracle)
              {
                propertyDescriptor12.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor15);
                break;
              }
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
        System.ComponentModel.PropertyDescriptor propDesc22 = properties6["UserName"];
        if (propDesc22 != null)
        {
          CustomPropertyDescriptor propertyDescriptor16 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams, propDesc22);
          if (service == null || !service.IsAdmin)
            propertyDescriptor16.AddAttribute((Attribute) new ReadOnlyAttribute(true));
          propertyDescriptor16.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
          propertyDescriptor16.AddAttribute((Attribute) new CustomDisplayName("Imbase.UserName.Name"));
          propertyDescriptor16.AddAttribute((Attribute) new CustomDescription("Imbase.UserName.Description"));
          propertyDescriptor12.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor16);
        }
        System.ComponentModel.PropertyDescriptor propDesc23 = properties6["Password"];
        if (propDesc23 != null)
        {
          CustomPropertyDescriptor propertyDescriptor17 = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams.SourceDBParams, propDesc23);
          if (service == null || !service.IsAdmin)
            propertyDescriptor17.AddAttribute((Attribute) new ReadOnlyAttribute(true));
          propertyDescriptor17.AddAttribute((Attribute) new TypeConverterAttribute(typeof (PasswordConverter)));
          propertyDescriptor17.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
          propertyDescriptor17.AddAttribute((Attribute) new CustomDisplayName("Imbase.Password.Name"));
          propertyDescriptor17.AddAttribute((Attribute) new CustomDescription("Imbase.Password.Description"));
          propertyDescriptor12.ChildProperties.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor17);
        }
        propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor12);
      }
      System.ComponentModel.PropertyDescriptor propDesc24 = properties5["PumpSettingsPath"];
      if (propDesc24 != null)
      {
        CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams, propDesc24);
        if (service == null || !service.IsAdmin)
          propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
        else
          propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (CustomBrowseFolderDialogEditor), typeof (UITypeEditor)));
        propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.PumpSettingsPath.Name"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.PumpSettingsPath.Description"));
        propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
      }
      System.ComponentModel.PropertyDescriptor propDesc25 = properties5["TimePoint"];
      if (propDesc25 != null)
      {
        CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams, propDesc25);
        if (service == null || !service.IsAdmin)
          propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
        propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (DateTimeConverter)));
        propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.TimePoint.Name"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.TimePoint.Description"));
        propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
      }
      System.ComponentModel.PropertyDescriptor propDesc26 = properties5["TerminateOnError"];
      if (propDesc26 != null)
      {
        CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams, propDesc26);
        if (service == null || !service.IsAdmin)
          propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
        propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomYesNoTypeConverter)));
        propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.TerminateOnError.Name"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.TerminateOnError.Description"));
        propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
      }
      System.ComponentModel.PropertyDescriptor propDesc27 = properties5["DeleteDuplicates"];
      if (propDesc27 != null)
      {
        CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams, propDesc27);
        if (service == null || !service.IsAdmin)
          propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
        propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (CustomYesNoTypeConverter)));
        propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.DeleteDuplicates.Name"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.DeleteDuplicates.Description"));
        propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
      }
      System.ComponentModel.PropertyDescriptor propDesc28 = properties5["DefaultMeasureId"];
      if (propDesc28 != null)
      {
        CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.CommonParams.ImbaseSyncParams, propDesc28);
        if (service == null || !service.IsAdmin)
          propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
        propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (CustomMeasureEditor), typeof (UITypeEditor)));
        propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (Intermech.Search.ObjectLinkConverter)));
        propertyDescriptor.AddAttribute((Attribute) new DefaultValueAttribute(0));
        propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSyncParams.CategoryName"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.DefaultMeasureId.Name"));
        propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.DefaultMeasureId.Description"));
        propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
      }
    }
    System.ComponentModel.PropertyDescriptor propDesc29 = properties2["HideEmptyColumns"];
    if (propDesc29 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc29);
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.TableView.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.UserParams.HideEmptyColumns.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.UserParams.HideEmptyColumns.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc30 = properties2["FreezeFirstColumn"];
    if (propDesc29 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc30);
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.TableView.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.UserParams.FreezeFirstColumn.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.UserParams.FreezeFirstColumn.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc31 = properties2["UseIMHSelector"];
    if (propDesc31 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc31);
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.SelectMaterial.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.UserParams.UseIMH.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.UserParams.UseIMH.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc32 = properties2["SaveColumnsState"];
    if (propDesc32 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc32);
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.TableView.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.UserParams.SaveColumnsState.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.UserParams.SaveColumnsState.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc33 = properties2["SaveFilterState"];
    if (propDesc33 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc33);
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.TableView.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.UserParams.SaveFilterState.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.UserParams.SaveFilterState.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc34 = properties2["SaveUserFilterState"];
    if (propDesc34 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc34);
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.TableView.CategoryName"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.UserParams.SaveUserFilterState.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.UserParams.SaveUserFilterState.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    System.ComponentModel.PropertyDescriptor propDesc35 = properties2["UseExtendedLog"];
    if (propDesc35 != null)
    {
      CustomPropertyDescriptor propertyDescriptor = new CustomPropertyDescriptor((object) this._imbaseParamsContainer.UserParams, propDesc35);
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
      propertyDescriptor.AddAttribute((Attribute) new CustomCategory("Imbase.ImbaseSynchronizationParams.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDisplayName("Imbase.UseExtendedLog.Name"));
      propertyDescriptor.AddAttribute((Attribute) new CustomDescription("Imbase.UseExtendedLog.Description"));
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  internal ImbaseParamsDescriptor(ImbaseParamsContainer imbaseParamsContainer)
  {
    this._imbaseParamsContainer = imbaseParamsContainer;
  }

  public System.ComponentModel.AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._imbaseParamsContainer, true);
  }

  public string GetClassName()
  {
    return TypeDescriptor.GetClassName((object) this._imbaseParamsContainer, true);
  }

  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this._imbaseParamsContainer, true);
  }

  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this._imbaseParamsContainer, true);
  }

  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._imbaseParamsContainer, true);
  }

  public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._imbaseParamsContainer, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._imbaseParamsContainer, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._imbaseParamsContainer, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._imbaseParamsContainer, attributes, true);
  }

  public PropertyDescriptorCollection GetProperties() => this.GetProperties(new Attribute[0]);

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    this._pdc = this.CreatePdc(attributes);
    return this._pdc ?? new PropertyDescriptorCollection((System.ComponentModel.PropertyDescriptor[]) null);
  }

  public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
  {
    return pd is CustomPropertyDescriptor propertyDescriptor ? propertyDescriptor.Owner : (object) this._imbaseParamsContainer;
  }

  public void ResetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is CustomPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetOldValue((object) this._imbaseParamsContainer);
    }
  }
}
