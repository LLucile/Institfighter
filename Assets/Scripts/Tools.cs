//#define DBGLOG_ANTON
//#define DBGLOG_ISABELLE
//#define DBGLOG_JULIEN
//#define DBGLOG_LOIC
//#define DBGLOG_LUCILE
//#define DBGLOG_QUENTIN

public static class Tools
{
	[System.Diagnostics.Conditional("DBGLOG_ANTON")]
	public static void LogAnton(string message)
	{
		UnityEngine.Debug.Log ("ANTON:" + message);
	}
	
	[System.Diagnostics.Conditional("DBGLOG_ISABELLE")]
	public static void LogIsabelle(string message)
	{
		UnityEngine.Debug.Log ("ISABELLE:" + message);
	}

	[System.Diagnostics.Conditional("DBGLOG_JULIEN")]
	public static void LogJulien(string message)
	{
		UnityEngine.Debug.Log ("JULIEN:" + message);
	}
	
	[System.Diagnostics.Conditional("DBGLOG_LOIC")]
	public static void LogLoic(string message)
	{
		UnityEngine.Debug.Log ("LOIC:" + message);
	}
	
	[System.Diagnostics.Conditional("DBGLOG_LUCILE")]
	public static void LogLucile(string message)
	{
		UnityEngine.Debug.Log ("LUCILE:" + message);
	}
	
	[System.Diagnostics.Conditional("DBGLOG_QUENTIN")]
	public static void LogQuentin(string message)
	{
		UnityEngine.Debug.Log ("QUENTIN:" + message);
	}
}
