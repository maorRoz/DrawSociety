using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DrawSocietyServer.DrawSocietyData
{
    public class DrawSocietyDataLayer
    {
        private static DrawSocietyDataLayer _instance;

        public static DrawSocietyDataLayer Instance => _instance ?? (_instance = new DrawSocietyDataLayer());

        private SqlConnection _dbConnection;
        private DrawSocietyDataLayer()
        {
            var localDbPath = new SqlConnectionStringBuilder
            {
                DataSource = ".\\SQLEXPRESS",
                InitialCatalog = "DrawSocietyServerContext",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };
            _dbConnection = new SqlConnection(localDbPath.ConnectionString);
            _dbConnection.Open();
        }

        public void InsertTable(string table, string tableColumns, string[] valuesNames, object[] values)
        {
            var insertRequest = "INSERT INTO " + table + " (" + tableColumns + ") VALUES (" + string.Join(",", valuesNames)
                                + ")";
            var commandDb = new SqlCommand(insertRequest, _dbConnection);
            for (int i = 0; i < values.Length; i++)
            {
                commandDb.Parameters.AddWithValue(valuesNames[i], values[i]);
            }

            commandDb.ExecuteNonQuery();

        }

        public SqlDataReader SelectFromTable(string table, string toSelect)
        {
            var selectRequest = "SELECT " + toSelect + " FROM " + table;

            return new SqlCommand(selectRequest, _dbConnection).ExecuteReader();

        }

        public SqlDataReader SelectFromTableWithCondition(string table, string toSelect, string condition)
        {
            var selectRequest = "SELECT " + toSelect + " FROM " + table + " WHERE " + condition;

            return new SqlCommand(selectRequest, _dbConnection).ExecuteReader();

        }

        public void UpdateTable(string table, string updateCondition, string[] columnNames, string[] valuesNames, object[] values)
        {
            string[] setString = new string[values.Length];
            for (int i = 0; i < setString.Length; i++)
            {
                setString[i] = columnNames[i] + " = " + valuesNames[i];
            }

            var updateCommand = "UPDATE " + table + " SET " + string.Join(", ", setString);
            if (updateCondition != null)
            {
                updateCommand+= " WHERE " + updateCondition;
            }
            var commandDb = new SqlCommand(updateCommand, _dbConnection);
            for (int i = 0; i < values.Length; i++)
            {
                commandDb.Parameters.AddWithValue(valuesNames[i], values[i]);
            }

            commandDb.ExecuteNonQuery();

        }

        public void DeleteFromTable(string table, string deleteCondition)
        {
            var deleteCommand = "DELETE FROM " + table + " WHERE " + deleteCondition;
            var commandDb = new SqlCommand(deleteCommand, _dbConnection);

            commandDb.ExecuteNonQuery();

        }

        public SqlDataReader FreeStyleSelect(string cmd)
        {
            return new SqlCommand(cmd, _dbConnection).ExecuteReader();
        }

        public void FreeStyleExecute(string cmd)
        {
            new SqlCommand(cmd, _dbConnection).ExecuteNonQuery();
        }

    }
}