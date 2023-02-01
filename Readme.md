# Sample app to demonstrate a build failure for Azure Functions.

We have an Azure Function that needs a conditional `[Disable]` attribute to be applied.
If that function also has a `[FunctionName]` attribute, the build fails, and cites
`Microsoft.NET.Sdk.Functions.Build.targets` as the source.

The full error message we receive in our production code is as follows:

```
System.ArgumentException: Object of type 'Mono.Cecil.TypeReference' cannot be converted to type 'System.Type'.
   at System.RuntimeType.TryChangeType(Object value, Binder binder, CultureInfo culture, Boolean needsSpecialCast)
   at System.RuntimeType.CheckValue(Object value, Binder binder, CultureInfo culture, BindingFlags invokeAttr)
   at System.Reflection.MethodBase.CheckArguments(StackAllocedArguments& stackArgs, ReadOnlySpan`1 parameters, Binder binder, BindingFlags invokeAttr, CultureInfo culture, Signature sig)
   at System.Reflection.RuntimeConstructorInfo.Invoke(BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at MakeFunctionJson.TypeUtility.ToReflection(CustomAttribute customAttribute) in /_/src/Microsoft.NET.Sdk.Functions.Generator/TypeUtility.cs:line 106
   at MakeFunctionJson.MethodInfoExtensions.GetDisabled(MethodDefinition method) in /_/src/Microsoft.NET.Sdk.Functions.Generator/MethodInfoExtensions.cs:line 118
   at MakeFunctionJson.MethodInfoExtensions.HasUnsuportedAttributes(MethodDefinition method, String& error) in /_/src/Microsoft.NET.Sdk.Functions.Generator/MethodInfoExtensions.cs:line 164
   at MakeFunctionJson.FunctionJsonConverter.GenerateFunctions(IEnumerable`1 types)+MoveNext() in /_/src/Microsoft.NET.Sdk.Functions.Generator/FunctionJsonConverter.cs:line 125
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at MakeFunctionJson.FunctionJsonConverter.TryGenerateFunctionJsons() in /_/src/Microsoft.NET.Sdk.Functions.Generator/FunctionJsonConverter.cs:line 194
   at MakeFunctionJson.FunctionJsonConverter.TryRun() in /_/src/Microsoft.NET.Sdk.Functions.Generator/FunctionJsonConverter.cs:line 87
Error generating functions metadata
```

This sample code creates the same build failure, but the error is:

```
the constructor 'DisableAttribute(Type)' is not supported.
Error generating functions metadata
```

## Workarounds

1. Remove one of the attributes. `[Disable]` and `[FunctionName]` each compile successfully.
   This may be feasible. We need to see if `[FunctionName]` is not needed for our Service Bus 
   triggers.
2. Use `[Disable(string settingName)]` instead of `[Disable(Type providerType)]`. This does not
   cause a compiler error, but it will not work for our scenario.