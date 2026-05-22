using System;

public static class IDGenerator
{
	public static string GenerateID(string _prefix = "", string _suffix = "") => _prefix + Guid.NewGuid().ToString().Substring(0,8) + _suffix;
}
