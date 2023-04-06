using MauiAppBlazor.Data;
using MauiAppBlazor.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Globalization;


namespace MauiAppBlazor.Pages
{
    public class CourseCalculateBase : ComponentBase
    {
        public List<Сourse> Coures;
        public string CouresUsd { get; set; }
        public decimal InputNumber { get; set; } = decimal.Zero;

        [Inject]
        public ICourseCalculationService CourseCalculationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadCourseAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void OnInputEvent(ChangeEventArgs changeEvent)
        {
            if (changeEvent.Value != null && changeEvent.Value.ToString() != "")
            {
                decimal courseusd = ConvertUANToUSD(Convert.ToDecimal(changeEvent.Value));
                if (Math.Round(courseusd, 3).ToString().Count() > 5)
                {
                    CouresUsd = courseusd.ToString("0,0.000", CultureInfo.InvariantCulture);
                }
                else
                {
                    CouresUsd = Math.Round(courseusd, 3).ToString();
                }
            }
            else
            {
                CouresUsd = "0";
            }
        }

        public decimal ConvertUANToUSD(decimal uan)
        {
            if (uan > 0 && uan != null)
            {
                return uan / Coures[0].Sale;
            }
            else
            {
                return 0;
            }
        }

        public async Task LoadCourseAsync()
        {
            Coures = await CourseCalculationService.GetСourseAsync();
        }


    }
}
