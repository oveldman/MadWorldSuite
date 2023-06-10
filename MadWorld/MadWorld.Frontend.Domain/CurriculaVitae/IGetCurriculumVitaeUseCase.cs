using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Frontend.Domain.CurriculaVitae;

public interface IGetCurriculumVitaeUseCase
{
    Task<CurriculumVitaeContract> GetCurriculumVitaeAsync();
}