using MauiAppBlazor.Data;


namespace MauiAppBlazor.Services.Contracts
{
    public interface ICourseCalculationService
    {
        Task<List<Сourse>> GetСourseAsync();
    }
}
