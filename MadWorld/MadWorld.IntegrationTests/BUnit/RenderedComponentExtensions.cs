using Bunit;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MadWorld.IntegrationTests.BUnit;

public static class RenderedComponentExtensions
{
    public static IRenderedComponent<TComponentToFind> FindComponent<TCurrentComponent, TComponentToFind, TInnerType>(this IRenderedComponent<TCurrentComponent> component, string name) 
        where TCurrentComponent : IComponent
        where TComponentToFind : FormComponent<TInnerType>
    {
        var inputFields = component.FindComponents<TComponentToFind>();
        return inputFields
            .First(c => c.Instance.Name.Equals(name));
    }
}