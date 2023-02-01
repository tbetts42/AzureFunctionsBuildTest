using System.Reflection;

namespace FunctionsBuildTest;

/// <summary>
/// This class represents configuration to disable service bus trigger. 
/// https://github.com/Azure/azure-webjobs-sdk/blob/b798412ad74ba97cf2d85487ae8479f277bdd85c/src/Microsoft.Azure.WebJobs/DisableAttribute.cs
/// </summary>
public class TestDisablementProvider
{
    /// <summary>
    /// Gets the flag which indicates if the trigger is disabled.
    /// </summary>
    /// <param name="methodInfo">The instance of method info.</param>
    /// <returns></returns>
    public bool IsDisabled(MethodInfo methodInfo)
    {
        // For the sample, always disable
        return methodInfo.DeclaringType == typeof(ServiceBusHandler);
    }
}
