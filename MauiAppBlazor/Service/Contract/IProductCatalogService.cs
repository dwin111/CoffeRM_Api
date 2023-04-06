using MauiAppBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBlazor.Service.Contract
{
    public interface IProductCatalogService
    {
        Task<IEnumerable<ProductCatalog>> GetItems();
        Task<ProductCatalog> GetItem(int id);
        Task<bool> Create(ProductCatalogDto productViewModel);

    }
}
