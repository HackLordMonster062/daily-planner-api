namespace DailyPlannerAPI.Services {
	public interface IStorageService {
		Task SaveAsync<T>(string key, T data);
		Task<T?> LoadAsync<T>(string key);
		Task<bool> ExistsAsync(string key);
		Task DeleteAsync(string key);

		Task<List<string>> GetAllKeysAsync();
	}
}
