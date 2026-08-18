
// Type: DataSetSurrogate
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Data;
using System.Globalization;


[Serializable]
public class DataSetSurrogate
{
  private string _datasetName;
  private string _namespace;
  private string _prefix;
  private bool _caseSensitive;
  private CultureInfo _locale;
  private bool _enforceConstraints;
  private ArrayList _fkConstraints;
  private ArrayList _relations;
  private Hashtable _extendedProperties;
  private DataTableSurrogate[] _dataTableSurrogates;

  public DataSetSurrogate(DataSet ds)
  {
    this._datasetName = ds != null ? ds.DataSetName : throw new ArgumentNullException("The parameter dataset is null");
    this._namespace = ds.Namespace;
    this._prefix = ds.Prefix;
    this._caseSensitive = ds.CaseSensitive;
    this._locale = ds.Locale;
    this._enforceConstraints = ds.EnforceConstraints;
    this._dataTableSurrogates = new DataTableSurrogate[ds.Tables.Count];
    for (int index = 0; index < ds.Tables.Count; ++index)
      this._dataTableSurrogates[index] = new DataTableSurrogate(ds.Tables[index]);
    this._fkConstraints = this.GetForeignKeyConstraints(ds);
    this._relations = this.GetRelations(ds);
    this._extendedProperties = new Hashtable();
    if (ds.ExtendedProperties.Keys.Count <= 0)
      return;
    foreach (object key in (IEnumerable) ds.ExtendedProperties.Keys)
      this._extendedProperties.Add(key, ds.ExtendedProperties[key]);
  }

  public DataSet ConvertToDataSet()
  {
    DataSet ds = new DataSet();
    this.ReadSchemaIntoDataSet(ds);
    this.ReadDataIntoDataSet(ds);
    return ds;
  }

  public void ReadSchemaIntoDataSet(DataSet ds)
  {
    if (ds == null)
      throw new ArgumentNullException("The dataset parameter cannot be null");
    ds.DataSetName = this._datasetName;
    ds.Namespace = this._namespace;
    ds.Prefix = this._prefix;
    ds.CaseSensitive = this._caseSensitive;
    ds.Locale = this._locale;
    ds.EnforceConstraints = this._enforceConstraints;
    foreach (DataTableSurrogate dataTableSurrogate in this._dataTableSurrogates)
    {
      DataTable table = new DataTable();
      DataTable dt = table;
      dataTableSurrogate.ReadSchemaIntoDataTable(dt);
      ds.Tables.Add(table);
    }
    this.SetForeignKeyConstraints(ds, this._fkConstraints);
    this.SetRelations(ds, this._relations);
    for (int index = 0; index < ds.Tables.Count; ++index)
    {
      DataTable table = ds.Tables[index];
      this._dataTableSurrogates[index].SetColumnExpressions(table);
    }
    if (this._extendedProperties.Keys.Count <= 0)
      return;
    foreach (object key in (IEnumerable) this._extendedProperties.Keys)
      ds.ExtendedProperties.Add(key, this._extendedProperties[key]);
  }

  public void ReadDataIntoDataSet(DataSet ds)
  {
    ArrayList readOnlyList = ds != null ? this.SuppressReadOnly(ds) : throw new ArgumentNullException("The dataset parameter cannot be null");
    ArrayList constraintRulesList = this.SuppressConstraintRules(ds);
    bool enforceConstraints = ds.EnforceConstraints;
    ds.EnforceConstraints = false;
    for (int index = 0; index < ds.Tables.Count; ++index)
    {
      DataTable table = ds.Tables[index];
      this._dataTableSurrogates[index].ReadDataIntoDataTable(ds.Tables[index], false);
    }
    ds.EnforceConstraints = enforceConstraints;
    this.ResetReadOnly(ds, readOnlyList);
    this.ResetConstraintRules(ds, constraintRulesList);
  }

  private ArrayList GetForeignKeyConstraints(DataSet ds)
  {
    ArrayList foreignKeyConstraints = new ArrayList();
    for (int index1 = 0; index1 < ds.Tables.Count; ++index1)
    {
      DataTable table = ds.Tables[index1];
      for (int index2 = 0; index2 < table.Constraints.Count; ++index2)
      {
        Constraint constraint = table.Constraints[index2];
        if (constraint is ForeignKeyConstraint foreignKeyConstraint)
        {
          string constraintName = constraint.ConstraintName;
          int[] numArray1 = new int[foreignKeyConstraint.RelatedColumns.Length + 1];
          numArray1[0] = ds.Tables.IndexOf(foreignKeyConstraint.RelatedTable);
          for (int index3 = 1; index3 < numArray1.Length; ++index3)
            numArray1[index3] = foreignKeyConstraint.RelatedColumns[index3 - 1].Ordinal;
          int[] numArray2 = new int[foreignKeyConstraint.Columns.Length + 1];
          numArray2[0] = index1;
          for (int index4 = 1; index4 < numArray2.Length; ++index4)
            numArray2[index4] = foreignKeyConstraint.Columns[index4 - 1].Ordinal;
          ArrayList arrayList = new ArrayList();
          arrayList.Add((object) constraintName);
          arrayList.Add((object) numArray1);
          arrayList.Add((object) numArray2);
          arrayList.Add((object) new int[3]
          {
            (int) foreignKeyConstraint.AcceptRejectRule,
            (int) foreignKeyConstraint.UpdateRule,
            (int) foreignKeyConstraint.DeleteRule
          });
          Hashtable hashtable = new Hashtable();
          if (foreignKeyConstraint.ExtendedProperties.Keys.Count > 0)
          {
            foreach (object key in (IEnumerable) foreignKeyConstraint.ExtendedProperties.Keys)
              hashtable.Add(key, foreignKeyConstraint.ExtendedProperties[key]);
          }
          arrayList.Add((object) hashtable);
          foreignKeyConstraints.Add((object) arrayList);
        }
      }
    }
    return foreignKeyConstraints;
  }

  private void SetForeignKeyConstraints(DataSet ds, ArrayList constraintList)
  {
    foreach (ArrayList constraint in constraintList)
    {
      string constraintName = (string) constraint[0];
      int[] numArray1 = (int[]) constraint[1];
      int[] numArray2 = (int[]) constraint[2];
      int[] numArray3 = (int[]) constraint[3];
      Hashtable hashtable = (Hashtable) constraint[4];
      DataColumn[] parentColumns = new DataColumn[numArray1.Length - 1];
      for (int index = 0; index < parentColumns.Length; ++index)
        parentColumns[index] = ds.Tables[numArray1[0]].Columns[numArray1[index + 1]];
      DataColumn[] childColumns = new DataColumn[numArray2.Length - 1];
      for (int index = 0; index < childColumns.Length; ++index)
        childColumns[index] = ds.Tables[numArray2[0]].Columns[numArray2[index + 1]];
      ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(constraintName, parentColumns, childColumns);
      foreignKeyConstraint.AcceptRejectRule = (AcceptRejectRule) numArray3[0];
      foreignKeyConstraint.UpdateRule = (Rule) numArray3[1];
      foreignKeyConstraint.DeleteRule = (Rule) numArray3[2];
      if (hashtable.Keys.Count > 0)
      {
        foreach (object key in (IEnumerable) hashtable.Keys)
          foreignKeyConstraint.ExtendedProperties.Add(key, hashtable[key]);
      }
      ds.Tables[numArray2[0]].Constraints.Add((Constraint) foreignKeyConstraint);
    }
  }

  private ArrayList GetRelations(DataSet ds)
  {
    ArrayList relations = new ArrayList();
    foreach (DataRelation relation in (InternalDataCollectionBase) ds.Relations)
    {
      string relationName = relation.RelationName;
      int[] numArray1 = new int[relation.ParentColumns.Length + 1];
      numArray1[0] = ds.Tables.IndexOf(relation.ParentTable);
      for (int index = 1; index < numArray1.Length; ++index)
        numArray1[index] = relation.ParentColumns[index - 1].Ordinal;
      int[] numArray2 = new int[relation.ChildColumns.Length + 1];
      numArray2[0] = ds.Tables.IndexOf(relation.ChildTable);
      for (int index = 1; index < numArray2.Length; ++index)
        numArray2[index] = relation.ChildColumns[index - 1].Ordinal;
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) relationName);
      arrayList.Add((object) numArray1);
      arrayList.Add((object) numArray2);
      arrayList.Add((object) relation.Nested);
      Hashtable hashtable = new Hashtable();
      if (relation.ExtendedProperties.Keys.Count > 0)
      {
        foreach (object key in (IEnumerable) relation.ExtendedProperties.Keys)
          hashtable.Add(key, relation.ExtendedProperties[key]);
      }
      arrayList.Add((object) hashtable);
      relations.Add((object) arrayList);
    }
    return relations;
  }

  private void SetRelations(DataSet ds, ArrayList relationList)
  {
    foreach (ArrayList relation1 in relationList)
    {
      string relationName = (string) relation1[0];
      int[] numArray1 = (int[]) relation1[1];
      int[] numArray2 = (int[]) relation1[2];
      bool flag = (bool) relation1[3];
      Hashtable hashtable = (Hashtable) relation1[4];
      DataColumn[] parentColumns = new DataColumn[numArray1.Length - 1];
      for (int index = 0; index < parentColumns.Length; ++index)
        parentColumns[index] = ds.Tables[numArray1[0]].Columns[numArray1[index + 1]];
      DataColumn[] childColumns = new DataColumn[numArray2.Length - 1];
      for (int index = 0; index < childColumns.Length; ++index)
        childColumns[index] = ds.Tables[numArray2[0]].Columns[numArray2[index + 1]];
      DataRelation relation2 = new DataRelation(relationName, parentColumns, childColumns, false);
      relation2.Nested = flag;
      if (hashtable.Keys.Count > 0)
      {
        foreach (object key in (IEnumerable) hashtable.Keys)
          relation2.ExtendedProperties.Add(key, hashtable[key]);
      }
      ds.Relations.Add(relation2);
    }
  }

  private ArrayList SuppressReadOnly(DataSet ds)
  {
    ArrayList arrayList = new ArrayList();
    for (int index1 = 0; index1 < ds.Tables.Count; ++index1)
    {
      DataTable table = ds.Tables[index1];
      for (int index2 = 0; index2 < table.Columns.Count; ++index2)
      {
        if (table.Columns[index2].Expression == string.Empty && table.Columns[index2].ReadOnly)
        {
          table.Columns[index2].ReadOnly = false;
          arrayList.Add((object) new int[2]
          {
            index1,
            index2
          });
        }
      }
    }
    return arrayList;
  }

  private ArrayList SuppressConstraintRules(DataSet ds)
  {
    ArrayList arrayList = new ArrayList();
    for (int index1 = 0; index1 < ds.Tables.Count; ++index1)
    {
      DataTable table = ds.Tables[index1];
      for (int index2 = 0; index2 < table.Constraints.Count; ++index2)
      {
        Constraint constraint = table.Constraints[index2];
        if (constraint is ForeignKeyConstraint)
        {
          ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint) constraint;
          arrayList.Add((object) new ArrayList()
          {
            (object) new int[2]{ index1, index2 },
            (object) new int[3]
            {
              (int) foreignKeyConstraint.AcceptRejectRule,
              (int) foreignKeyConstraint.UpdateRule,
              (int) foreignKeyConstraint.DeleteRule
            }
          });
          foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
          foreignKeyConstraint.UpdateRule = Rule.None;
          foreignKeyConstraint.DeleteRule = Rule.None;
        }
      }
    }
    return arrayList;
  }

  private void ResetReadOnly(DataSet ds, ArrayList readOnlyList)
  {
    foreach (int[] numArray in readOnlyList)
    {
      int index1 = numArray[0];
      int index2 = numArray[1];
      ds.Tables[index1].Columns[index2].ReadOnly = true;
    }
  }

  private void ResetConstraintRules(DataSet ds, ArrayList constraintRulesList)
  {
    foreach (ArrayList constraintRules in constraintRulesList)
    {
      int[] numArray1 = (int[]) constraintRules[0];
      int[] numArray2 = (int[]) constraintRules[1];
      int index1 = numArray1[0];
      int index2 = numArray1[1];
      ForeignKeyConstraint constraint = (ForeignKeyConstraint) ds.Tables[index1].Constraints[index2];
      constraint.AcceptRejectRule = (AcceptRejectRule) numArray2[0];
      constraint.UpdateRule = (Rule) numArray2[1];
      constraint.DeleteRule = (Rule) numArray2[2];
    }
  }

  private bool IsSchemaIdentical(DataSet ds)
  {
    return !(ds.DataSetName != this._datasetName) && !(ds.Namespace != this._namespace) && ds.Tables.Count == this._dataTableSurrogates.Length;
  }
}
