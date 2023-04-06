using MauiAppBlazor.Models;
using MauiAppBlazor.Service.Contract;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBlazor.Pages
{
    public class CRMCoffeBase : ComponentBase
    {
        public IEnumerable<ProductCatalog> fullProductCatalogs { get; set; }
        public List<ProductCatalogModelView> CheckCatalogs { get; set; }
        public double SummPrice { get; set; } = 0;

        public bool modalViewCreateCheck { get; set; }
        public bool modalViewDeleteCheck { get; set; }

        [Inject]
        public IProductCatalogService ProductCatalogService { get; set; }

        [Inject]
        public ICheckService CheckService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            fullProductCatalogs = await ProductCatalogService.GetItems();
            modalViewCreateCheck = false;
            modalViewDeleteCheck = false;
            CheckCatalogs = new();
        }

        public List<Product> GetProduct()
        {
            List<Product> products = new();
            foreach (var productView in CheckCatalogs)
            {
                var product = new Product
                {
                    Id = productView.Id,
                    Name = productView.Name,
                    Price = productView.Price,
                    Amount = productView.Amount,
                    Date = DateTime.UtcNow,
                    TotalPrice = productView.TotalPrice,
                    Type = productView.Type,
                };
                products.Add(product);
            }
            return products;
        }

        public async Task CreateCheck()
        {
            try
            {
                Dictionary<long, long> ProductsIdAndAmoun = new();
                foreach (var item in CheckCatalogs)
                {
                    ProductsIdAndAmoun.Add(item.Id, item.Amount);
                }
                if (ProductsIdAndAmoun.Count > 0)
                {
                    await CheckService.NewCheck(0, 1, ProductsIdAndAmoun);
                }
                modalViewCreateCheck = false;
                ClenCheck();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ClenCheck()
        {
            CheckCatalogs = new();
            SummPrice = 0;
            modalViewDeleteCheck = false;
        }

        public void AddProductToCheck(long id)
        {
            var modelView = CheckCatalogs.FirstOrDefault(cc => cc.Id == id);
            if (modelView == null)
            {
                var model = fullProductCatalogs.FirstOrDefault(fpc => fpc.Id == id);
                modelView = new ProductCatalogModelView
                {
                    Id = model.Id,
                    Name = model.Name,
                    ImageURL = model.ImageURL,
                    Price = model.Price,
                    Amount = 1,
                    TotalPrice = model.Price,
                    Type = model.Type

                };
                CheckCatalogs.Add(modelView);
                SummerPriceProduct(modelView, true);
            }
            else
            {
                AddNumber(modelView);
            }


        }
        public void DeleteProduct(ProductCatalogModelView modelView)
        {
            try
            {
                SummPrice -= modelView.Price * modelView.Amount;
                CheckCatalogs.Remove(modelView);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddNumber(ProductCatalogModelView modelView)
        {
            modelView.Amount++;
            modelView.TotalPrice = modelView.Price * modelView.Amount;
            SummerPriceProduct(modelView, true);
        }
        public void TakeAwayNumber(ProductCatalogModelView modelView)
        {
            double numbeer = modelView.Amount;
            if (--numbeer > 0)
            {
                modelView.Amount--;
                modelView.TotalPrice = modelView.Price * modelView.Amount;
                SummerPriceProduct(modelView, false);
            }
            else
            {
                DeletProductInCheck(modelView);
            }
        }
        public void DeletProductInCheck(ProductCatalogModelView modelView)
        {
            try
            {
                if (modelView != null)
                {
                    SummerPriceProduct(modelView, false);
                    CheckCatalogs.Remove(modelView);
                    if (CheckCatalogs.Count <= 0)
                    {
                        SummPrice = 0;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SummerPriceProduct(ProductCatalogModelView model, bool ItsAdd)
        {
            if (ItsAdd)
            {
                SummPrice += model.Price;
            }
            else
            {
                SummPrice -= model.Price;
            }
        }

    }
}
