namespace MadWorld.Unittests;

public class AutoDomainDataAttribute : AutoDataAttribute
{
    public AutoDomainDataAttribute()
        : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}