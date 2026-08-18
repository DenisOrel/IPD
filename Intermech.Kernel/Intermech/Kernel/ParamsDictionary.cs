// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ParamsDictionary
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class ParamsDictionary
{
  private Dictionary<int, object> _ParamsDict;
  private static Dictionary<string, int> ColumnIDs = new Dictionary<string, int>(182);
  private int _RowsCount;

  static ParamsDictionary()
  {
    ParamsDictionary.ColumnIDs.Add("F_LINKTYPE", 1);
    ParamsDictionary.ColumnIDs.Add("F_AUTHOR", 2);
    ParamsDictionary.ColumnIDs.Add("F_PREV_DATE", 3);
    ParamsDictionary.ColumnIDs.Add("F_SCHEDULE", 4);
    ParamsDictionary.ColumnIDs.Add("F_EVENT_KIND", 5);
    ParamsDictionary.ColumnIDs.Add("F_IMMEDIATE_RUN", 6);
    ParamsDictionary.ColumnIDs.Add("F_ERROR_MSG", 7);
    ParamsDictionary.ColumnIDs.Add("F_WORD", 8);
    ParamsDictionary.ColumnIDs.Add("F_WORD_ID", 9);
    ParamsDictionary.ColumnIDs.Add("F_TF", 10);
    ParamsDictionary.ColumnIDs.Add("F_TF_IDF", 11);
    ParamsDictionary.ColumnIDs.Add("F_OBJECT_COUNT", 12);
    ParamsDictionary.ColumnIDs.Add("F_CHECKOUT_DATE", 13);
    ParamsDictionary.ColumnIDs.Add("F_SNAPSHOT_ID", 14);
    ParamsDictionary.ColumnIDs.Add("F_SNAPSHOT_DATE", 15);
    ParamsDictionary.ColumnIDs.Add("F_TABLE_ID", 16 /*0x10*/);
    ParamsDictionary.ColumnIDs.Add("F_CLASSIVKEY", 17);
    ParamsDictionary.ColumnIDs.Add("F_CATALOG_ID", 18);
    ParamsDictionary.ColumnIDs.Add("F_LINK_ID", 19);
    ParamsDictionary.ColumnIDs.Add("F_TEXT", 20);
    ParamsDictionary.ColumnIDs.Add("F_HASHTEXT", 21);
    ParamsDictionary.ColumnIDs.Add("F_LANGUAGES", 22);
    ParamsDictionary.ColumnIDs.Add("F_CULTURE_ID", 23);
    ParamsDictionary.ColumnIDs.Add("F_PROJECT_ID", 24);
    ParamsDictionary.ColumnIDs.Add("F_SCHEMA_ID", 25);
    ParamsDictionary.ColumnIDs.Add("F_OPTIMIZED", 26);
    ParamsDictionary.ColumnIDs.Add("F_READ_DURATION", 27);
    ParamsDictionary.ColumnIDs.Add("F_SEEK_DURATION", 28);
    ParamsDictionary.ColumnIDs.Add("F_WRITE_DURATION", 29);
    ParamsDictionary.ColumnIDs.Add("F_READ", 30);
    ParamsDictionary.ColumnIDs.Add("F_SEEK", 31 /*0x1F*/);
    ParamsDictionary.ColumnIDs.Add("F_WRITE", 32 /*0x20*/);
    ParamsDictionary.ColumnIDs.Add("F_PRJ_GUID", 33);
    ParamsDictionary.ColumnIDs.Add("F_TABLE_NAME", 34);
    ParamsDictionary.ColumnIDs.Add("F_MASK", 35);
    ParamsDictionary.ColumnIDs.Add("F_OPTIONS", 36);
    ParamsDictionary.ColumnIDs.Add("F_SET_DATE", 37);
    ParamsDictionary.ColumnIDs.Add("F_STATUS", 38);
    ParamsDictionary.ColumnIDs.Add("F_CONTENT", 39);
    ParamsDictionary.ColumnIDs.Add("F_DEL_TIME", 40);
    ParamsDictionary.ColumnIDs.Add("F_WORK_CAPTION", 41);
    ParamsDictionary.ColumnIDs.Add("F_OBJ_CREATE", 42);
    ParamsDictionary.ColumnIDs.Add("CAPTION", 43);
    ParamsDictionary.ColumnIDs.Add("F_INVIEW", 44);
    ParamsDictionary.ColumnIDs.Add("F_FIRST", 45);
    ParamsDictionary.ColumnIDs.Add("F_DEFAULT_DESCRIPT", 46);
    ParamsDictionary.ColumnIDs.Add("F_DESCRIPTION", 47);
    ParamsDictionary.ColumnIDs.Add("F_TREE_LEVEL", 48 /*0x30*/);
    ParamsDictionary.ColumnIDs.Add("F_MODIFY_MODE", 49);
    ParamsDictionary.ColumnIDs.Add("F_PARENT_KEY", 50);
    ParamsDictionary.ColumnIDs.Add("F_PARAMS", 51);
    ParamsDictionary.ColumnIDs.Add("F_ROUTE_ID", 52);
    ParamsDictionary.ColumnIDs.Add("F_PUBLIC_LC", 53);
    ParamsDictionary.ColumnIDs.Add("F_FROM_STEP", 54);
    ParamsDictionary.ColumnIDs.Add("F_TO_STEP", 55);
    ParamsDictionary.ColumnIDs.Add("F_DELETED", 56);
    ParamsDictionary.ColumnIDs.Add("F_GROUP_NAME", 57);
    ParamsDictionary.ColumnIDs.Add("F_MULTIPLE_VALUED", 58);
    ParamsDictionary.ColumnIDs.Add("F_UNIQUE", 59);
    ParamsDictionary.ColumnIDs.Add("F_ANY_ATTRIBUTES", 60);
    ParamsDictionary.ColumnIDs.Add("F_CAPTION_ATTRIBUTE", 61);
    ParamsDictionary.ColumnIDs.Add("F_OBJ_NAME", 62);
    ParamsDictionary.ColumnIDs.Add("F_OBJECTLINK_ID", 63 /*0x3F*/);
    ParamsDictionary.ColumnIDs.Add("F_ZIPSIZE", 64 /*0x40*/);
    ParamsDictionary.ColumnIDs.Add("F_INTEGER_VALUE", 65);
    ParamsDictionary.ColumnIDs.Add("F_DOUBLE_VALUE", 66);
    ParamsDictionary.ColumnIDs.Add("F_DATE_VALUE", 67);
    ParamsDictionary.ColumnIDs.Add("F_STRING_VALUE", 68);
    ParamsDictionary.ColumnIDs.Add("F_LANGUAGE_ID", 69);
    ParamsDictionary.ColumnIDs.Add("F_OBJECT_ID", 70);
    ParamsDictionary.ColumnIDs.Add("F_PRJLINK_ID", 71);
    ParamsDictionary.ColumnIDs.Add("F_LEVEL_ID", 72);
    ParamsDictionary.ColumnIDs.Add("F_FORMULA", 73);
    ParamsDictionary.ColumnIDs.Add("F_FORMULA_ID", 74);
    ParamsDictionary.ColumnIDs.Add("F_FOLDER_ID", 75);
    ParamsDictionary.ColumnIDs.Add("F_GUID", 76);
    ParamsDictionary.ColumnIDs.Add("F_ATTRIBUTE_TYPE", 77);
    ParamsDictionary.ColumnIDs.Add("F_ALIAS", 78);
    ParamsDictionary.ColumnIDs.Add("F_SHORT_NAME", 79);
    ParamsDictionary.ColumnIDs.Add("F_NAME", 80 /*0x50*/);
    ParamsDictionary.ColumnIDs.Add("F_APPLICABILITY_ID", 81);
    ParamsDictionary.ColumnIDs.Add("F_DISPLAY", 82);
    ParamsDictionary.ColumnIDs.Add("F_VALIDATION_RULE", 83);
    ParamsDictionary.ColumnIDs.Add("F_REQUIRED", 84);
    ParamsDictionary.ColumnIDs.Add("F_PUBLIC", 85);
    ParamsDictionary.ColumnIDs.Add("F_OBJECT_TYPE", 86);
    ParamsDictionary.ColumnIDs.Add("F_ATTRIBUTE_ID", 87);
    ParamsDictionary.ColumnIDs.Add("F_TOOBJECT_ID", 88);
    ParamsDictionary.ColumnIDs.Add("F_AREA_ID", 89);
    ParamsDictionary.ColumnIDs.Add("F_AREA_NAME", 90);
    ParamsDictionary.ColumnIDs.Add("F_AREA_NOTE", 91);
    ParamsDictionary.ColumnIDs.Add("F_NOTE", 92);
    ParamsDictionary.ColumnIDs.Add("F_GROUP_ID", 93);
    ParamsDictionary.ColumnIDs.Add("F_VALUE", 94);
    ParamsDictionary.ColumnIDs.Add("F_PARAM_NAME", 95);
    ParamsDictionary.ColumnIDs.Add("F_SECTION_ID", 96 /*0x60*/);
    ParamsDictionary.ColumnIDs.Add("F_USER_ID", 97);
    ParamsDictionary.ColumnIDs.Add("F_MODULE_NAME", 98);
    ParamsDictionary.ColumnIDs.Add("F_RIGHT_TYPE", 99);
    ParamsDictionary.ColumnIDs.Add("F_RIGHT_ID", 100);
    ParamsDictionary.ColumnIDs.Add("F_CATEGORY_ID", 101);
    ParamsDictionary.ColumnIDs.Add("F_CATEGORY_TYPE", 102);
    ParamsDictionary.ColumnIDs.Add("F_KEY", 103);
    ParamsDictionary.ColumnIDs.Add("F_DEFAULT_VALUE", 104);
    ParamsDictionary.ColumnIDs.Add("F_SIZE_TYPE", 105);
    ParamsDictionary.ColumnIDs.Add("F_TYPE_DESCRIPTION", 106);
    ParamsDictionary.ColumnIDs.Add("F_COMPUTED", 107);
    ParamsDictionary.ColumnIDs.Add("F_DEFAULT", 108);
    ParamsDictionary.ColumnIDs.Add("F_LANGUAGE_NAME", 109);
    ParamsDictionary.ColumnIDs.Add("F_HUMAN_ID", 110);
    ParamsDictionary.ColumnIDs.Add("F_IMBASE_KEY", 111);
    ParamsDictionary.ColumnIDs.Add("F_AUDIT_TYPE", 112 /*0x70*/);
    ParamsDictionary.ColumnIDs.Add("F_END_DATE", 113);
    ParamsDictionary.ColumnIDs.Add("F_BEGIN_DATE", 114);
    ParamsDictionary.ColumnIDs.Add("F_EVENT_TYPE", 115);
    ParamsDictionary.ColumnIDs.Add("F_COMPUTER_NAME", 116);
    ParamsDictionary.ColumnIDs.Add("F_OBJECT_NAME", 117);
    ParamsDictionary.ColumnIDs.Add("F_EVENT_ID", 118);
    ParamsDictionary.ColumnIDs.Add("F_REVISION_ID", 119);
    ParamsDictionary.ColumnIDs.Add("F_VERSION_ID", 120);
    ParamsDictionary.ColumnIDs.Add("F_ID", 121);
    ParamsDictionary.ColumnIDs.Add("F_DEFAULT_RELATION", 122);
    ParamsDictionary.ColumnIDs.Add("F_VERSIONABLE", 123);
    ParamsDictionary.ColumnIDs.Add("F_HUMAN_ID_RULE", 124);
    ParamsDictionary.ColumnIDs.Add("F_OBJ_TYPE_NAME", 125);
    ParamsDictionary.ColumnIDs.Add("F_INLIST_ID", 126);
    ParamsDictionary.ColumnIDs.Add("F_PARENT_ID", 128 /*0x80*/);
    ParamsDictionary.ColumnIDs.Add("F_ICON", 129);
    ParamsDictionary.ColumnIDs.Add("F_LITERA", 130);
    ParamsDictionary.ColumnIDs.Add("F_LEVEL_NAME", 131);
    ParamsDictionary.ColumnIDs.Add("F_START_DATE", 132);
    ParamsDictionary.ColumnIDs.Add("F_KEY_ID", 133);
    ParamsDictionary.ColumnIDs.Add("F_ACCESS_TYPE", 134);
    ParamsDictionary.ColumnIDs.Add("F_LC_NAME", 135);
    ParamsDictionary.ColumnIDs.Add("F_LC_STEP", 136);
    ParamsDictionary.ColumnIDs.Add("F_CREATE_DATE", 137);
    ParamsDictionary.ColumnIDs.Add("F_PART_ID", 138);
    ParamsDictionary.ColumnIDs.Add("F_PROJ_ID", 139);
    ParamsDictionary.ColumnIDs.Add("F_SAVE_HISTORY", 140);
    ParamsDictionary.ColumnIDs.Add("F_RELATION_KIND", 141);
    ParamsDictionary.ColumnIDs.Add("F_CHKOUTFILE", 142);
    ParamsDictionary.ColumnIDs.Add("F_REVERSE_NAME", 143);
    ParamsDictionary.ColumnIDs.Add("F_TYPE_NAME", 144 /*0x90*/);
    ParamsDictionary.ColumnIDs.Add("F_RELATION_TYPE", 145);
    ParamsDictionary.ColumnIDs.Add("F_PATH_TYPE", 146);
    ParamsDictionary.ColumnIDs.Add("F_PATH", 147);
    ParamsDictionary.ColumnIDs.Add("F_PATH_ID", 148);
    ParamsDictionary.ColumnIDs.Add("F_MODIFY_DATE", 149);
    ParamsDictionary.ColumnIDs.Add("F_OWNER_ID", 150);
    ParamsDictionary.ColumnIDs.Add("F_OBJECT_VER_TYPE", 151);
    ParamsDictionary.ColumnIDs.Add("F_CHKOUT_BY", 152);
    ParamsDictionary.ColumnIDs.Add("F_TRY_COUNT", 153);
    ParamsDictionary.ColumnIDs.Add("F_DEADLOCK_DATE", 154);
    ParamsDictionary.ColumnIDs.Add("F_INT_INFO", 155);
    ParamsDictionary.ColumnIDs.Add("F_DATE", 156);
    ParamsDictionary.ColumnIDs.Add("F_STRING_INFO", 157);
    ParamsDictionary.ColumnIDs.Add("F_GUID_TYPE", 158);
    ParamsDictionary.ColumnIDs.Add("F_ARC_METHOD", 159);
    ParamsDictionary.ColumnIDs.Add("F_FILEDATE", 160 /*0xA0*/);
    ParamsDictionary.ColumnIDs.Add("F_FILESIZE", 161);
    ParamsDictionary.ColumnIDs.Add("F_FILEBODY", 162);
    ParamsDictionary.ColumnIDs.Add("F_FILENAME", 163);
    ParamsDictionary.ColumnIDs.Add("F_FILE_ID", 164);
    ParamsDictionary.ColumnIDs.Add("F_DELETE_DATE", 165);
    ParamsDictionary.ColumnIDs.Add("F_CONSTRAINT_MODE", 166);
    ParamsDictionary.ColumnIDs.Add("F_CLONE_RELATIONS", 167);
    ParamsDictionary.ColumnIDs.Add("F_MIN_LINKS", 168);
    ParamsDictionary.ColumnIDs.Add("F_MAX_LINKS", 169);
    ParamsDictionary.ColumnIDs.Add("F_INOBJECT_TYPE", 170);
    ParamsDictionary.ColumnIDs.Add("F_DRAW_DATA", 171);
    ParamsDictionary.ColumnIDs.Add("F_MASTER_ID", 172);
    ParamsDictionary.ColumnIDs.Add("F_SOURCE_ID", 173);
    ParamsDictionary.ColumnIDs.Add("F_CONTEXT_ID", 174);
    ParamsDictionary.ColumnIDs.Add("F_MODIFICATION_ID", 175);
    ParamsDictionary.ColumnIDs.Add("F_WORKINGCOPY_ID", 176 /*0xB0*/);
    ParamsDictionary.ColumnIDs.Add("F_BASE_VERSION", 177);
    ParamsDictionary.ColumnIDs.Add("F_SITE_ID", 178);
    ParamsDictionary.ColumnIDs.Add("F_ACCESS", 179);
    ParamsDictionary.ColumnIDs.Add("F_STORAGE_ID", 180);
    ParamsDictionary.ColumnIDs.Add("F_CREATOR_ID", 181);
    ParamsDictionary.ColumnIDs.Add("F_REL_CREATOR", 182);
  }

  public void Create(DataRow row)
  {
    if (row == null)
      return;
    this._RowsCount = 1;
    this._ParamsDict = new Dictionary<int, object>(row.Table.Columns.Count);
    for (int index = 0; index < row.Table.Columns.Count; ++index)
    {
      int key;
      if (ParamsDictionary.ColumnIDs.TryGetValue(row.Table.Columns[index].ColumnName, out key))
        this._ParamsDict.Add(key, row[index]);
    }
  }

  public object this[int index]
  {
    get => this._ParamsDict[index];
    set => this._ParamsDict[index] = value;
  }

  public int RowsCount => this._RowsCount;

  public void Clear()
  {
    this._ParamsDict.Clear();
    this._RowsCount = 0;
  }
}
