using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using Microsoft.EntityFrameworkCore;


namespace CoffeCRMBeck.DAL
{
    public class ProductCatalogRepository : IRepository<Menu>
    {
        private readonly AppDbContext _db;

        public ProductCatalogRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<bool> CreateAsync(Menu model)
        {
            if (model != null)
            {
                await _db.Menu.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> EditAsync(Menu model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "Model cannot be null");
                }

                using var connection = _db.Database.GetDbConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "CALL edit_menu_item(@Id, @Name, @ImageURL, @Price, @Type)";
                command.CommandType = System.Data.CommandType.Text;

                // Параметри
                var idParam = command.CreateParameter();
                idParam.ParameterName = "@Id";
                idParam.Value = model.Id;
                command.Parameters.Add(idParam);

                var nameParam = command.CreateParameter();
                nameParam.ParameterName = "@Name";
                nameParam.Value = model.Name ?? (object)DBNull.Value;
                command.Parameters.Add(nameParam);

                var imageUrlParam = command.CreateParameter();
                imageUrlParam.ParameterName = "@ImageURL";
                imageUrlParam.Value = model.ImageURL ?? (object)DBNull.Value;
                command.Parameters.Add(imageUrlParam);

                var priceParam = command.CreateParameter();
                priceParam.ParameterName = "@Price";
                priceParam.Value = model.Price;
                command.Parameters.Add(priceParam);

                var typeParam = command.CreateParameter();
                typeParam.ParameterName = "@Type";
                typeParam.Value = (int)model.Type;
                command.Parameters.Add(typeParam);

                // Виконання процедури
                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Menu model)
        {
            try
            {
                if (model != null)
                {
                    _db.Menu.Remove(model);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public IQueryable<Menu> GetAll()
        {
            return _db.Menu;
        }

        public async Task<Menu> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
            
        }
    }
}

