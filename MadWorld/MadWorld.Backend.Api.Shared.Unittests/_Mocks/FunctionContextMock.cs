using System.Collections.Immutable;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MadWorld.Backend.Api.Shared.Unittests._Mocks;

public static class FunctionContextMock
{
    public static FunctionContext Create(IEnumerable<BindingMetadata> metaData)
    {
        var context = Substitute.For<FunctionContext>();
        var functionDefinition = Substitute.For<FunctionDefinition>();
        var inputBindings = Substitute.For<IImmutableDictionary<string, BindingMetadata>>();
        
        inputBindings.Values.Returns(metaData);
        functionDefinition.InputBindings.Returns(inputBindings);
        context.FunctionDefinition.Returns(functionDefinition);
        
        return context;
    }
    
    public static FunctionContext Create(
        IEnumerable<BindingMetadata> metaData, 
        string assemblyLocation, 
        string functionName,
        string functionTypeName,
        string headers
    )
    {
        var context = Substitute.For<FunctionContext>();
        var features = Substitute.For<IInvocationFeatures>();
        var functionDefinition = Substitute.For<FunctionDefinition>();
        var bindingContext = Substitute.For<BindingContext>();
        var inputBindings = Substitute.For<IImmutableDictionary<string, BindingMetadata>>();

        bindingContext.BindingData["Headers"].Returns(headers);
        functionDefinition.EntryPoint.Returns(functionTypeName);
        functionDefinition.Name.Returns(functionName);
        functionDefinition.PathToAssembly.Returns(assemblyLocation);
        inputBindings.Values.Returns(metaData);
        
        functionDefinition.InputBindings.Returns(inputBindings);

        context.Features.Returns(features);
        context.FunctionDefinition.Returns(functionDefinition);
        context.BindingContext.Returns(bindingContext);
        
        
        return context;
    }
}