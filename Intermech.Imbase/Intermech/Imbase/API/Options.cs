// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.Options
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.API;

internal struct Options
{
  internal int level;
  internal int CMTmode;
  internal int startMenuLine;
  internal int startData;
  internal int fastReturn;
  internal bool sleepOut;
  internal bool dynamicMode;
  internal int retCode;
  internal bool tableMode;
  internal int tableShow;
  internal int tableOut;
  internal int recordLevel;
  internal bool convertToDos;
  internal string fieldName;
  internal string tempFileName;
  internal string catalogName;
  internal string basePath;
  internal string textPath;
  internal string dwgPath;
  internal string textView;
  internal string dwgView;
  internal string userId;
  internal string CMT_BaseName;
  internal string line_2;
  internal string line_3;
  internal string pathString;
  internal string tableName;
  internal string TablesList;
  internal string DynamicFields;
  internal int Flags;
  internal int FastDrag;
  internal MaterialMode progMode;

  internal void Clear()
  {
    this.Flags = 0;
    this.fieldName = string.Empty;
    this.catalogName = "$CADMECH";
    this.CMT_BaseName = string.Empty;
    this.line_2 = string.Empty;
    this.line_3 = string.Empty;
    this.userId = string.Empty;
    this.basePath = "\\\\im\\\\imbase";
    this.textPath = string.Empty;
    this.dwgPath = string.Empty;
    this.textView = string.Empty;
    this.dwgView = string.Empty;
    this.pathString = string.Empty;
    this.tableName = string.Empty;
    this.TablesList = string.Empty;
    this.DynamicFields = string.Empty;
    this.level = 0;
    this.recordLevel = 0;
    this.convertToDos = true;
    this.CMTmode = 0;
    this.startMenuLine = 3;
    this.startData = 6;
    this.fastReturn = 0;
    this.sleepOut = false;
    this.dynamicMode = false;
    this.retCode = 0;
    this.tableMode = false;
    this.tableShow = 32768 /*0x8000*/;
    this.tableOut = 16384 /*0x4000*/;
    this.progMode = MaterialMode.DIALOG;
  }
}
