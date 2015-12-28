using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;


/// <summary>
/// Summary description for ADO_Net
/// </summary>
public abstract class ADO_Net
{
    public abstract DataTable readData(string Sql);
    public abstract DataTable readData(string sql, string NameTable, Boolean Bool);
    public abstract DataTable readData(string Sql, string[] ParameterConlection, Object[] valueofParameter);
    public abstract DataTable readData(string Sql, string NameTable, string[] ParameterConlection, Object[] valueofParameter);
    public abstract List<string> readData(string Sql, string[] ParameterConlection, Object[] valueofParameter, string[] ParameterOutputs);
    public abstract string readData(string Sql, string[] ParameterConlection, Object[] valueofParameter, string ParameterOutput);
    public abstract List<string> readData(string sql, string[] ParameterOutputs);
    public abstract string readData(string sql, string ParameterOutputs);
    public abstract int writeData(string sql, string[] ParameterConlection, Object[] valueofParameter);
    public abstract int writeData(string sql);
}
