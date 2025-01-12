using BagAPI.Helper;
using BagAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BagAPI.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly BagDBContext _context;
        protected readonly ILogger<BaseService<T>> _logger;

        public BaseService(BagDBContext context, ILogger<BaseService<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Create
        public async Task<ApiResponse<T>> CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return new ApiResponse<T>(true, "Entity created successfully", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity");
                return new ApiResponse<T>(false, "Error creating entity", null);
            }
        }

        // Get All
        public async Task<ApiResponse<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Set<T>().AsNoTracking().ToListAsync();
                return new ApiResponse<IEnumerable<T>>(true, "Entities retrieved successfully", entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entities");
                return new ApiResponse<IEnumerable<T>>(false, "Error retrieving entities", null);
            }
        }

        // Get by Id
        public async Task<ApiResponse<T>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
                if (entity == null)
                {
                    return new ApiResponse<T>(false, "Entity not found", null);
                }
                return new ApiResponse<T>(true, "Entity retrieved successfully", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity");
                return new ApiResponse<T>(false, "Error retrieving entity", null);
            }
        }

        // Update
        public async Task<ApiResponse<T>> UpdateAsync(int id, T entity)
        {
            try
            {
                var existingEntity = await _context.Set<T>().FindAsync(id);
                if (existingEntity == null)
                {
                    return new ApiResponse<T>(false, "Entity not found", null);
                }

                var entityEntry = _context.Entry(existingEntity);

                foreach (var property in entityEntry.Properties)
                {
                    if (!property.Metadata.IsPrimaryKey())
                    {
                        property.CurrentValue = _context.Entry(entity).Property(property.Metadata.Name).CurrentValue;
                    }
                }

                await _context.SaveChangesAsync();
                return new ApiResponse<T>(true, "Entity updated successfully", existingEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity");
                return new ApiResponse<T>(false, "Error updating entity", null);
            }
        }

        // Delete
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    return new ApiResponse<bool>(false, "Entity not found", false);
                }

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Entity deleted successfully", true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity");
                return new ApiResponse<bool>(false, "Error deleting entity", false);
            }
        }
    }
}