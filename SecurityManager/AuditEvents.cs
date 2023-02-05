using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace SecurityManager
{
	public enum AuditEventTypes
	{
		CreateFolderSuccess = 0,
		CreateFileSuccess = 1,
		DeleteFileSuccess = 2,
		MoveToSuccess = 3, 
		RenameFileSuccess = 4
		
	}

	public class AuditEvents
	{
		private static ResourceManager resourceManager = null;
		private static object resourceLock = new object();

		private static ResourceManager ResourceMgr
		{
			get
			{
				lock (resourceLock)
				{
					if (resourceManager == null)
					{
						resourceManager = new ResourceManager
							(typeof(EventFile).ToString(),
							Assembly.GetExecutingAssembly());
					}
					return resourceManager;
				}
			}
		}

		public static string CreateFolderSuccess
		{
			get
			{
				
				return ResourceMgr.GetString(AuditEventTypes.CreateFolderSuccess.ToString());
			}
		}

		public static string CreateFileSuccess
		{
			get
			{
				
				return ResourceMgr.GetString(AuditEventTypes.CreateFileSuccess.ToString());
			}
		}

		public static string RenameFileSuccess
		{
			get
			{
			
				return ResourceMgr.GetString(AuditEventTypes.RenameFileSuccess.ToString());
			}
		}

		public static string DeleteFileSuccess
		{
			get
			{

				return ResourceMgr.GetString(AuditEventTypes.DeleteFileSuccess.ToString());
			}
		}

		public static string MoveToSuccess
		{
			get
			{

				return ResourceMgr.GetString(AuditEventTypes.MoveToSuccess.ToString());
			}
		}
	}
}
