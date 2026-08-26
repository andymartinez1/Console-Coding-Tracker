namespace CodingTracker.Services;

public interface ICrudService<TAddRequest, TUpdateRequest, TResponse, TKey>
{
    public Task<TResponse> AddAsync(TAddRequest? request);

    public Task<TResponse?> GetByIdAsync(TKey? id);

    public Task<List<TResponse>> GetAllAsync();

    public Task<TResponse?> UpdateAsync(TUpdateRequest? request);

    public Task<bool> DeleteAsync(TKey? id);
}