namespace Isca_Travels.Interface
{
    public interface InterfaceService<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task<T> CreateAsync(T anyclass);
        Task<T?> UpdateAsync(int? id, T anyclass);
        Task<T?> DeleteAsync(int? id);
    }
}
