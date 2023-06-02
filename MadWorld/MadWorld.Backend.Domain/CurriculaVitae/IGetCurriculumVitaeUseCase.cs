using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public interface IGetCurriculumVitaeUseCase
{
    GetCurriculumVitaeResponse GetCurriculumVitae();
}