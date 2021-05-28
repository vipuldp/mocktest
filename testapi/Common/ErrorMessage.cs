using System;
using System.IO;

namespace testapi.Common
{
	public static class ErrorMessage
	{
		public static void WriteExceptionLog(Exception ex)
		{
			StreamWriter streamWriter = null;
			string filePath = Directory.GetCurrentDirectory().ToString() + "\\data\\ErrorLog\\";

			DirectoryInfo dir = new DirectoryInfo(filePath);
			if (!dir.Exists)
			{
				dir.Create();
			}
			string FileName = DateTime.Now.ToString("yyyyMMdd") + "_Exception.txt";
			string logFilePath = filePath + FileName;

			try
			{
				if (!File.Exists(logFilePath))
				{
					streamWriter = new StreamWriter(logFilePath);
				}
				else
				{
					streamWriter = File.AppendText(logFilePath);
				}

				do
				{
					string exceptionMessage = "Date Time\t\t\t:" + DateTime.Now.ToString("dd/MMM/yyyy  HH:mm:ss");
					streamWriter.WriteLine(exceptionMessage);
					exceptionMessage = "Exception\t\t\t\t:" + ex.Message.Trim();
					streamWriter.WriteLine(exceptionMessage);
					exceptionMessage = "Inner Exception\t\t\t:" + (ex.InnerException == null ? "" : ex.InnerException.Message.Trim());
					streamWriter.WriteLine(exceptionMessage);
					exceptionMessage = "Source\t\t\t\t:" + (ex.Source == null ? "" : ex.Source.Trim());
					streamWriter.WriteLine(exceptionMessage);
					exceptionMessage = "StackTrace:\t\t\t:" + (ex.StackTrace == null ? "" : ex.StackTrace.Trim());
					streamWriter.WriteLine(exceptionMessage);
					streamWriter.WriteLine();

					ex = ex.InnerException;

				} while (ex != null);

				streamWriter.WriteLine();
				streamWriter.Close();
			}
			catch (Exception exe)
			{
				if (streamWriter != null)
				{
					streamWriter.Close();
				}
				throw exe.InnerException;
			}
		}
	}
}
