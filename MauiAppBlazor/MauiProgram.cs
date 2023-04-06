using Microsoft.Extensions.Logging;
using MauiAppBlazor.Data;
using MauiAppBlazor.Services;
using MauiAppBlazor.Services.Contracts;
using MauiAppBlazor.Service.Contract;
using MauiAppBlazor.Service;

namespace MauiAppBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7057/") });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddScoped<ICourseCalculationService, CourseCalculationService>();
        builder.Services.AddScoped<IProductCatalogService, ProductCatalogService>();
        builder.Services.AddScoped<ICheckService, CheckService>();

        return builder.Build();
	}
}
