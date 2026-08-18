
// Type: Intermech.Interfaces.Briefcase.BriefcaseStatics
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;
using System.Text;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Summary description for BriefcaseStatics.</summary>
    public class BriefcaseStatics
    {
      private static readonly string[] MetadataExportListSchemaArray = new string[18]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlMetadataExportListDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlMetadataExportListDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlMetadataRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        $"              <xs:element name=\"{BriefcaseConsts.XmlCategoryTag}\" type=\"xs:int\" />",
        $"              <xs:element name=\"{BriefcaseConsts.XmlIdTag}\" type=\"xs:string\" />",
        $"              <xs:element name=\"{BriefcaseConsts.XmlExternalTag}\" type=\"xs:string\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] ObjectsSchemaArray = new string[35]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlObjectsDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlObjectsDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlObjectRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_OBJECT_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_OBJECTGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_ID\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_IDGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_LC_STEP\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_VERSION_ID\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_PARENT_ID\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_CHKOUT_BY\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_CHKOUTGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_OBJECT_VER_TYPE\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_OBJECT_TYPE\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_OWNER_ID\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_OWNERGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_MODIFY_DATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_LEVEL_ID\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_OBJ_CREATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "              <xs:element name=\"CAPTION\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_PROJECT_ID\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_PROJECTGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_ACCESS\" type=\"xs:int\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] ObjAttributesSchemaArray = new string[30]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlObjAttributesDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlObjAttributesDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlAttributeRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_ATTRIBUTE_ID\" type=\"xs:int\" />",
        "              <xs:element name=\"F_OBJECT_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_INLIST_ID\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_INTEGER_VALUE\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_INTEGERGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_DOUBLE_VALUE\" type=\"xs:double\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_DOUBLEGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_STRING_VALUE\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_DATE_VALUE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_FILESIZE\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_ARC_METHOD\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_NOTE\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_PATH2FILE\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_LINKTYPE\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_AUTHOR\" type=\"xs:string\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] RelationsSchemaArray = new string[21]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlRelationsDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlRelationsDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlRelationRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_PRJLINK_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_PRJ_GUID\" type=\"xs:string\" />",
        "              <xs:element name=\"F_PROJ_ID\" type=\"xs:string\" />",
        "              <xs:element name=\"F_PART_ID\" type=\"xs:string\" />",
        "              <xs:element name=\"F_RELATION_TYPE\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_CREATE_DATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] RelAttributesSchemaArray = new string[30]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlRelAttributesDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlRelAttributesDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlAttributeRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_ATTRIBUTE_ID\" type=\"xs:int\" />",
        "              <xs:element name=\"F_PRJLINK_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_INLIST_ID\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_INTEGER_VALUE\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_INTEGERGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_DOUBLE_VALUE\" type=\"xs:double\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_DOUBLEGUID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_STRING_VALUE\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_DATE_VALUE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_FILESIZE\" type=\"xs:long\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_ARC_METHOD\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_NOTE\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_PATH2FILE\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_LINKTYPE\" type=\"xs:int\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_AUTHOR\" type=\"xs:string\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] ObjLCStepsSchemaArray = new string[18]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlObjLCStepsDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlObjLCStepsDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlObjLCStepsRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_OBJECT_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_LC_STEP\" type=\"xs:int\" />",
        "              <xs:element name=\"F_START_DATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] ContextsSchemaArray = new string[16 /*0x10*/]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlContextsDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlContextsDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlContextsRecordTag}\">",
        "          <xs:complexType>",
        "           <xs:attribute name=\"id\" type=\"xs:string\" use=\"required\"/>",
        "           <xs:attribute name=\"modification_id\" type=\"xs:string\" use=\"required\"/>",
        "           <xs:attribute name=\"content\" type=\"xs:string\" use=\"required\"/>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] ExportContentSchemaArray = new string[17]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlExportContentDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlExportContentDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlExportAttributeRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_OBJECT_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_CATEGORY_ID\" type=\"xs:int\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] MetadataSecuritySchemaArray = new string[23]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlMetadataSecurityDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlMetadataSecurityDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlSecurityRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_CATEGORY_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_CATEGORY_TYPE\" type=\"xs:int\" />",
        "              <xs:element name=\"F_RIGHT_ID\" type=\"xs:int\" />",
        "              <xs:element name=\"F_USER_ID\" type=\"xs:string\" />",
        "              <xs:element name=\"F_RIGHT_TYPE\" type=\"xs:int\" />",
        "              <xs:element name=\"F_OWNER_ID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_END_DATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_BEGIN_DATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };
      private static readonly string[] ObjSecuritySchemaArray = new string[23]
      {
        "<?xml version=\"1.0\" standalone=\"yes\"?>",
        $"<xs:schema id=\"{BriefcaseConsts.XmlObjSecurityDatasetName}\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
        $"  <xs:element name=\"{BriefcaseConsts.XmlObjSecurityDatasetName}\" msdata:IsDataSet=\"true\" msdata:Locale=\"ru-RU\">",
        "    <xs:complexType>",
        "      <xs:choice maxOccurs=\"unbounded\">",
        $"        <xs:element name=\"{BriefcaseConsts.XmlSecurityRecordTag}\">",
        "          <xs:complexType>",
        "            <xs:sequence>",
        "              <xs:element name=\"F_CATEGORY_ID\" type=\"xs:long\" />",
        "              <xs:element name=\"F_CATEGORY_TYPE\" type=\"xs:int\" />",
        "              <xs:element name=\"F_RIGHT_ID\" type=\"xs:int\" />",
        "              <xs:element name=\"F_USER_ID\" type=\"xs:string\" />",
        "              <xs:element name=\"F_RIGHT_TYPE\" type=\"xs:int\" />",
        "              <xs:element name=\"F_OWNER_ID\" type=\"xs:string\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_BEGIN_DATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "              <xs:element name=\"F_END_DATE\" type=\"xs:dateTime\" minOccurs=\"0\" />",
        "            </xs:sequence>",
        "          </xs:complexType>",
        "        </xs:element>",
        "      </xs:choice>",
        "    </xs:complexType>",
        "  </xs:element>",
        "</xs:schema>"
      };

      private static void WriteCustomFile(string schemaFileName, string[] strings)
      {
        using (FileStream fileStream = new FileStream(schemaFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
          using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.UTF8))
          {
            foreach (string str in strings)
              streamWriter.WriteLine(str);
          }
        }
      }

      public static void WriteMetadataExportListXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.MetadataExportListSchemaArray);
      }

      public static void WriteObjectsXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.ObjectsSchemaArray);
      }

      public static void WriteObjAttributesXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.ObjAttributesSchemaArray);
      }

      public static void WriteRelationsXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.RelationsSchemaArray);
      }

      public static void WriteRelAttributesXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.RelAttributesSchemaArray);
      }

      public static void WriteObjLCStepsXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.ObjLCStepsSchemaArray);
      }

      public static void WriteContextsXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.ContextsSchemaArray);
      }

      public static void WriteExportContentXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.ExportContentSchemaArray);
      }

      public static void WriteMetadataSecurityXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.MetadataSecuritySchemaArray);
      }

      public static void WriteObjSecurityXMLSchema(string schemaFileName)
      {
        BriefcaseStatics.WriteCustomFile(schemaFileName, BriefcaseStatics.ObjSecuritySchemaArray);
      }
    }
}
