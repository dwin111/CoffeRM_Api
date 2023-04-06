using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBlazor.Data
{
    public class Сourse
    {
        public int Id { get; set; }
        public string CCY { get; set; }
        public string Base_CCY { get; set; }
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
    }
}
