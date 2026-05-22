using System;

public static class IDGenerator
{
	#region Fields & Properties
	#region Fields
	#endregion
	
	#region Properties
	#endregion
	#endregion

	#region Methods
	public static string GenerateID(string _prefix = "", string _suffix = "") => _prefix + Guid.NewGuid().ToString().Substring(0,8) + _suffix;
	#endregion Methods
}
