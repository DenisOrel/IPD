// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.BlankConsts
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Expert;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

public static class BlankConsts
{
  public const string F_GROUP_NAME = "F_GROUP_NAME";
  public const string F_GROUP_ID = "F_GROUP_ID";
  public const string F_ATTRIBUTE_ID = "F_ATTRIBUTE_ID";
  public const string F_ATTRIBUTE_TYPE = "F_ATTRIBUTE_TYPE";
  public const string F_NAME = "F_NAME";
  public const string OLE = "OLE";
  public const string GDWGPICT = "dwgpict";

  public static class ObjectType
  {
    public static int BlankSetupId;
    public static readonly string BlankSetupGuid = "cadd99ae-306c-11d8-b4e9-00304f19f545";

    static ObjectType()
    {
      BlankConsts.ObjectType.BlankSetupId = MetaDataHelper.GetObjectTypeID(BlankConsts.ObjectType.BlankSetupGuid);
    }
  }

  public static class AttrFile
  {
    public static int AttrFileID;
    public static readonly string AttrFileGuid = "cad0004b-306c-11d8-b4e9-00304f19f545";

    static AttrFile()
    {
      BlankConsts.AttrFile.AttrFileID = MetaDataHelper.GetAttributeTypeID(BlankConsts.AttrFile.AttrFileGuid);
    }
  }

  public static class Template
  {
    public static int TemplateID;
    public static readonly string TemplateGuid = ExpertAttrGUIDs.attTemplateLink;

    static Template()
    {
      BlankConsts.Template.TemplateID = MetaDataHelper.GetAttributeTypeID(BlankConsts.Template.TemplateGuid);
    }
  }

  public static class GroupDocument
  {
    public static int GroupDocumentID;
    public static readonly string GroupDocumentGuid = "cadd9a1e-306c-11d8-b4e9-00304f19f545";

    static GroupDocument()
    {
      BlankConsts.GroupDocument.GroupDocumentID = MetaDataHelper.GetAttributeTypeID(BlankConsts.GroupDocument.GroupDocumentGuid);
    }
  }

  public static class AttributeName
  {
    public static string NameId = "cad00020-306c-11d8-b4e9-00304f19f545";
  }
}
