using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.SettingsTools;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public static class NLogger
	{
		private static void SetAgentLastError(ref string errMessage)
		{
			try
			{
				if (MainSettings.MainSqlSettings != null && MainSettings.MainSqlSettings.ComputerId.HasValue)
				{
					using (SqlConnection sql = Connect(MainSettings.JsonSettings.ConnectionString))
					{
						SqlCommand proc = new SqlCommand("SetAgentLastError", sql);
						proc.CommandType = CommandType.StoredProcedure;
						if (sql.State == ConnectionState.Closed)
							sql.Open();
						proc.Parameters.Add("@ComputerId", SqlDbType.Int).Value = MainSettings.MainSqlSettings.ComputerId;
						proc.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar).Value = errMessage ?? "";
						proc.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errMessage = $"{errMessage}. AgentLastError: {ex.ToString()}";
			}
		}

		public static void LogErrorToBaseOrHdd(int? computerId, string errMessage)
		{
			SetAgentLastError(ref errMessage);
			if (computerId == null || computerId == 0 || !LogErrorToBase(computerId, errMessage))
				LogManager.GetCurrentClassLogger().Error($"{System.Reflection.Assembly.GetCallingAssembly().GetName().Name} {errMessage}");
		}

		public static void LogErrorToBaseAndHdd(int? computerId, string errMessage)
		{
			SetAgentLastError(ref errMessage);
			if (computerId != null && computerId != 0)
			{
				LogErrorToBase(computerId, errMessage);
			}
			LogManager.GetCurrentClassLogger().Error($"{System.Reflection.Assembly.GetCallingAssembly().GetName().Name} {errMessage}");
		}
		public static void SetReportForOperation(int operationId, bool isCompleted, string message)
		{
			SqlWorks.ExecProc("SendOperationReport", operationId, isCompleted, message);
		}

		public static void LogErrorToHdd(string errorMessage)
		{
			SetAgentLastError(ref errorMessage);
			LogManager.GetCurrentClassLogger().Error($"{System.Reflection.Assembly.GetCallingAssembly().GetName().Name} {errorMessage}");
		}

		public static void LogDebugToHdd(string debugMessage)
		{
			LogManager.GetCurrentClassLogger().Debug($"{System.Reflection.Assembly.GetCallingAssembly().GetName().Name} {debugMessage}");
		}

		public static void LogErrorToBaseOrHdd(int? computerId, string errMessage, string loggerName)
		{
			SetAgentLastError(ref errMessage);
			if (computerId == null || computerId == 0 || !LogErrorToBase(computerId, errMessage))
				LogManager.GetLogger(loggerName).Error(errMessage);
		}

		public static void LogErrorToBaseAndHdd(int? computerId, string errMessage, string loggerName)
		{
			SetAgentLastError(ref errMessage);
			if (computerId != null && computerId != 0)
			{
				LogErrorToBase(computerId, errMessage);
			}
			LogManager.GetLogger(loggerName).Error(errMessage);
		}

		public static void LogErrorToHdd(string errorMessage, string loggerName)
		{
			SetAgentLastError(ref errorMessage);
			if (string.IsNullOrEmpty(loggerName))
			{
				LogErrorToHdd(errorMessage);
			}
			else
			{
				LogManager.GetLogger(loggerName)?.Error(errorMessage);
			}
		}

		public static void LogDebugToHdd(string debugMessage, string loggerName)
		{
			LogManager.GetLogger(loggerName).Debug(debugMessage);
		}

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
				LogManager.GetCurrentClassLogger().Error(ex.Message.ToString());
			}

			return sql;
		}

		private static bool LogErrorToBase(int? computerId, string errMessage)
		{
			try
			{
				using (SqlConnection sql = Connect(MainSettings.JsonSettings.ConnectionString))
				{
					if (sql == null)
						return false;

					SqlCommand proc = new SqlCommand("LogError", sql);
					proc.CommandType = CommandType.StoredProcedure;
					if (sql.State == ConnectionState.Closed)
						sql.Open();
					proc.Parameters.Add("@ComputerId", SqlDbType.Int).Value = computerId;
					proc.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar).Value = errMessage;
					proc.ExecuteNonQuery();

					return true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
