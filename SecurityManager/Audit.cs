using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SecurityManager
{
	public class Audit : IDisposable
	{

		private static EventLog customLog = null;
		const string SourceName = "SecurityManager.Audit";
		const string LogName = "MySecTest";

		static Audit()
		{
			try
			{
				if (!EventLog.SourceExists(SourceName))
				{
					EventLog.CreateEventSource(SourceName, LogName);
				}
				customLog = new EventLog(LogName,
					Environment.MachineName, SourceName);
			}
			catch (Exception e)
			{
				customLog = null;
				Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
			}
		}


		public static void CreateFolderSuccess(string fileName, string userName)
		{
			//TO DO

			if (customLog != null)
			{
				string UserCreateFolderSuccess =
					AuditEvents.CreateFolderSuccess;
				string message = String.Format(UserCreateFolderSuccess, fileName, 
					userName);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.CreateFolderSuccess));
			}
		}

		public static void CreateFileSuccess(string fileName, string userName)
		{
			//TO DO
			if (customLog != null)
			{
				string UserCreateFileSuccess =
					AuditEvents.CreateFileSuccess;
				string message = String.Format(UserCreateFileSuccess, fileName,
					userName);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.CreateFileSuccess));
			}
		}

	
		public static void RenameFileSuccess(string fileName, string userName, string newName)
		{
			if (customLog != null)
			{
				string UserRenameFileSuccess =
					AuditEvents.RenameFileSuccess;
				string message = String.Format(UserRenameFileSuccess, fileName,
					userName, newName);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.RenameFileSuccess));
			}
		}

		public static void DeleteFileSuccess(string fileName, string userName)
		{
			if (customLog != null)
			{
				string UserDeleteFileSuccess =
					AuditEvents.DeleteFileSuccess;
				string message = String.Format(UserDeleteFileSuccess, fileName,
					userName);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.DeleteFileSuccess));
			}
		}

		public static void MoveToSuccess(string fileName, string userName, string destination)
		{
			if (customLog != null)
			{
				string UserMoveToSuccess =
					AuditEvents.MoveToSuccess;
				string message = String.Format(UserMoveToSuccess, fileName, 
					userName, destination);
				customLog.WriteEntry(message);
			}
			else
			{
				throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
					(int)AuditEventTypes.MoveToSuccess));
			}
		}

		public void Dispose()
		{
			if (customLog != null)
			{
				customLog.Dispose();
				customLog = null;
			}
		}
	}
}
