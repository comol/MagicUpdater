using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MagicUpdaterCommon.Data
{
	public class TrySaveLocalSqlSettingsToBase : TryResult
	{
		public TrySaveLocalSqlSettingsToBase(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public static class SqlWorks
	{
		private static readonly Dictionary<SqlDbType, Type> SqlDbTypeToType
		= new Dictionary<SqlDbType, Type>
			  {
				  {SqlDbType.BigInt, typeof (long)},
				  {SqlDbType.Binary, typeof (byte[])},
				  {SqlDbType.Image, typeof (byte[])},
				  {SqlDbType.Timestamp, typeof (byte[])},
				  {SqlDbType.VarBinary, typeof (byte[])},
				  {SqlDbType.Bit, typeof (bool)},
				  {SqlDbType.Char, typeof (string)},
				  {SqlDbType.NChar, typeof (string)},
				  {SqlDbType.NText, typeof (string)},
				  {SqlDbType.NVarChar, typeof (string)},
				  {SqlDbType.Text, typeof (string)},
				  {SqlDbType.VarChar, typeof (string)},
				  {SqlDbType.Xml, typeof (string)},
				  {SqlDbType.DateTime, typeof (DateTime)},
				  {SqlDbType.SmallDateTime, typeof (DateTime)},
				  {SqlDbType.Date, typeof (DateTime)},
				  {SqlDbType.Time, typeof (DateTime)},
				  {SqlDbType.DateTime2, typeof (DateTime)},
				  {SqlDbType.Decimal, typeof (decimal)},
				  {SqlDbType.Money, typeof (decimal)},
				  {SqlDbType.SmallMoney, typeof (decimal)},
				  {SqlDbType.Float, typeof (double)},
				  {SqlDbType.Int, typeof (int)},
				  {SqlDbType.Real, typeof (float)},
				  {SqlDbType.UniqueIdentifier, typeof (Guid)},
				  {SqlDbType.SmallInt, typeof (short)},
				  {SqlDbType.TinyInt, typeof (byte)},
				  {SqlDbType.Variant, typeof (object)},
				  {SqlDbType.Udt, typeof (object)},
				  {SqlDbType.Structured, typeof (DataTable)},
				  {SqlDbType.DateTimeOffset, typeof (DateTimeOffset)}
			  };

		private static SqlConnection Connect(string _connectionString)
		{
			SqlConnection sql = null;
			try
			{
				sql = new SqlConnection(_connectionString);
				sql.Open();
				sql.Close();
			}
			catch (Exception ex)
			{
				if (sql != null)
					sql.Close();
				sql = null;
				//NLogger.Nlog.Error(ex.Message.ToString());
			}

			return sql;
		}

		public static DataSet ExecProcExt(string _connectionString, string procName, params object[] args)
		{
			string msg;
			return ExecProcExt(_connectionString, procName, out msg, args);
		}

		public static DataSet ExecProcExt(string _connectionString, string procName, out string exceptionMsg, params object[] args)
		{
			exceptionMsg = "";
			using (SqlConnection sql = Connect(_connectionString))
			{
				if (sql == null)
					return null;
				try
				{
					SqlCommand proc = new SqlCommand(procName, sql);
					proc.CommandType = CommandType.StoredProcedure;
					if (sql.State == ConnectionState.Closed)
						sql.Open();
					SqlCommandBuilder.DeriveParameters(proc);
					if (args.Length == proc.Parameters.Count - 1)
					{
						for (int i = 0; i < args.Length; i++)
						{
							SqlParameter p = proc.Parameters[i + 1];
							p.Value = Convert.ChangeType(args[i], SqlDbTypeToType[p.SqlDbType]) ?? DBNull.Value;
						}
					}
					else
						throw new IndexOutOfRangeException(
							$"Количество передаваемых аргументов не совпадает с количеством аргументов в хранимой процедуре: {procName}");
					using (SqlDataAdapter adap = new SqlDataAdapter(proc))
					{
						DataSet ds = new DataSet();
						adap.Fill(ds);
						if (ds != null && ds.Tables.Count > 0)
							foreach (DataTable t in ds.Tables)
							{
								if (t.Rows != null && t.Rows.Count > 0)
									return ds;
							}
						return null;
					}
				}
				catch (Exception ex)
				{
					//TODO Гасим и пишем ошибку в базу, возможно лучше не гасить и дать приложению свалиться (подумаем)
					exceptionMsg = ex.Message.ToString();

					NLogger.LogErrorToHdd(ex.Message.ToString());
					return null;
				}
			}
		}

		public static bool CheckStoredProcExists(string storeProcName)
		{
			string query = $"select * from sysobjects where type='P' and name='{storeProcName}'";
			bool spExists = false;
			using (SqlConnection conn = new SqlConnection(MainSettings.JsonSettings.ConnectionString))
			{
				conn.Open();
				using (SqlCommand command = new SqlCommand(query, conn))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							spExists = true;
							break;
						}
					}
				}
			}
			return spExists;
		}

		public static DataSet ExecProc(string procName, params object[] args)
		{
			return ExecProcExt(MainSettings.JsonSettings.ConnectionString, procName, args);
		}

		public static DataSet ExecProc(string procName)
		{
			return ExecProcExt(MainSettings.JsonSettings.ConnectionString, procName, new object[0]);
		}

		public static bool CheckSQL_Connection(string _connectionString, out string exceptionMsg)
		{
			exceptionMsg = null;
			SqlConnection sql = null;
			try
			{
				sql = new SqlConnection(_connectionString);
				sql.Open();
				sql.Close();
				return true;
			}
			catch (Exception ex)
			{
				if (sql != null)
					sql.Close();
				sql = null;
				exceptionMsg = ex.Message.ToString();
				return false;
			}
		}
		public static bool CheckSQL_Connection(string _connectionString)
		{
			string msg;
			return CheckSQL_Connection(_connectionString, out msg);
		}

		public static DataSet ExecSql(string query)
		{
			using (SqlConnection conn = new SqlConnection(MainSettings.JsonSettings.ConnectionString))
			{
				conn.Open();
				using (SqlCommand command = new SqlCommand(query, conn))
				{
					using (SqlDataAdapter da = new SqlDataAdapter())
					{
						da.SelectCommand = command;
						DataSet ds = new DataSet();
						da.Fill(ds);
						if (ds != null && ds.Tables.Count > 0)
							foreach (DataTable t in ds.Tables)
							{
								if (t.Rows != null && t.Rows.Count > 0)
									return ds;
							}
						return null;
					}
				}
			}
		}

		public static TrySaveLocalSqlSettingsToBase SaveLocalSqlSettingsToBase(SqlLocalSettings sqlLocalSettings)
		{
			var props = sqlLocalSettings.GetType().GetProperties();
			foreach (var prop in props)
			{
				if (prop.GetValue(sqlLocalSettings, null) != null)
				{
					string propValue = prop.GetValue(sqlLocalSettings, null).ToString();
					string exceptionText;
					ExecProcExt(MainSettings.JsonSettings.ConnectionString, "SetLocalSettingsForComputer", out exceptionText, MainSettings.MainSqlSettings.ComputerId , prop.Name, propValue);
					if (!string.IsNullOrEmpty(exceptionText))
					{
						return new TrySaveLocalSqlSettingsToBase(false, exceptionText);
					}
				}
			}

			return new TrySaveLocalSqlSettingsToBase();
		}


		public static bool SaveCommonGlobalSettingsToBase(CommonGlobalSettings sqlGlobalSettings)
		{
			var props = sqlGlobalSettings.GetType().GetProperties();
			foreach (var prop in props)
			{
				if (prop.GetValue(sqlGlobalSettings, null) != null)
				{
					string propValue = prop.GetValue(sqlGlobalSettings, null).ToString();
					string exceptionText;
					ExecProcExt(MainSettings.JsonSettings.ConnectionString, "SetCommonGlobalSettings", out exceptionText, prop.Name, propValue);
					if (!string.IsNullOrEmpty(exceptionText))
					{
						return false;
					}
				}
			}
			return true;
		}

		public static void SaveMainSqlSettingsToBase(SqlMainSettings sqlMainSettings)
		{
			ExecProc("SetMainSqlSettings", sqlMainSettings.ComputerId, sqlMainSettings.ShopID, sqlMainSettings.Is1CServer, sqlMainSettings.IsMainCashbox);
		}
	}
}
