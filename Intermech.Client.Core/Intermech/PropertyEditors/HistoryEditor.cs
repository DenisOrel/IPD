
// Type: Intermech.PropertyEditors.HistoryEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.History;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Выбор значения из ранее введенных значений атрибута</summary>
public class HistoryEditor : UITypeEditor
{
  private long id = -1;
  private AttributableElements kind;
  private int attributeId = -1;
  private Guid attrGuid = Guid.Empty;
  private FieldTypes attrType = FieldTypes.ftString;
  private ObjectsHistory objectsHistory;

  /// <summary>Выбор значения из history значений атрибута</summary>
  /// <param name="aId">id объекта/связи</param>
  /// <param name="aElement">объект/связь</param>
  /// <param name="aAttributeId">id атрибута</param>
  public HistoryEditor(long aId, AttributableElements aElement, int aAttributeId)
  {
    this.id = aId;
    this.kind = aElement;
    this.attributeId = aAttributeId;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.attrGuid == Guid.Empty)
      {
        IDBAttributeType attributeType1 = sessionKeeper.Session.GetAttributeType(this.attributeId);
        if (attributeType1 != null)
        {
          this.attrGuid = attributeType1.PropertiesStructure.AttributeGuid;
          this.attrType = attributeType1.AttributeType;
          this.objectsHistory = new ObjectsHistory((object) this.id, this.kind, (object) this.attrGuid);
          bool flag = true;
          IMSAttribute4 imsAttribute4 = (IMSAttribute4) null;
          switch (this.kind)
          {
            case AttributableElements.Object:
              IDBObject dbObject = sessionKeeper.Session.GetObject(this.id);
              if (dbObject != null)
              {
                imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, this.attributeId);
                break;
              }
              break;
            case AttributableElements.Relation:
              IDBRelation relation = sessionKeeper.Session.GetRelation(this.id);
              if (relation != null)
              {
                imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(relation.RelationType, this.attributeId);
                break;
              }
              break;
          }
          if (imsAttribute4 != null)
          {
            flag = (imsAttribute4.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit;
          }
          else
          {
            IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(this.attributeId);
            if (attributeType2 != null)
              flag = (attributeType2.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit;
          }
          this.objectsHistory.ReadOnly = flag;
        }
      }
      if (this.attrGuid == Guid.Empty || this.objectsHistory == null)
        return value;
      this.objectsHistory.SelectedValue = value;
      return this.objectsHistory.ShowDialog() == DialogResult.OK ? this.objectsHistory.SelectedValue : value;
    }
  }
}
