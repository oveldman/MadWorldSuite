using Bunit;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MadWorld.IntegrationTests.BUnit;

public static class RenderedComponentExtensions
{
    public static IRenderedComponent<TY> FindComponent<T, TY>(this IRenderedComponent<T> component, string name) 
        where T : IComponent
        where TY : RadzenComponent
    {
        var inputFields = component.FindComponents<TY>();
        return inputFields
            .First(c => c.Instance.Attributes.TryGetValue("@Name", out var nameObject) && nameObject.Equals(name));
    }
}