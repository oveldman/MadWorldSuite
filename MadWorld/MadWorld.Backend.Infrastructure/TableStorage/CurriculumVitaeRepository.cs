using MadWorld.Backend.Domain.CurriculaVitae;

namespace MadWorld.Backend.Infrastructure.TableStorage;

public class CurriculumVitaeRepository : ICurriculumVitaeRepository
{
    public CurriculumVitae GetCurriculumVitae()
    {
        return new CurriculumVitae()
        {
            FullName = "Max Mustermann"
        };
    }
}