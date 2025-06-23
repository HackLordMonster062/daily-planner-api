using System.Text.Json;

namespace DailyPlannerAPI.Services {
	public class JsonFileStorage : IStorageService {
		private readonly string _basePath = "Data";

		public JsonFileStorage() {
			Directory.CreateDirectory(_basePath);
		}

		public async Task SaveAsync<T>(string key, T data) {
			string fullPath = PathFor(key);

			Console.WriteLine("Saving to: " + fullPath);
			var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
			await File.WriteAllTextAsync(PathFor(key), json);
		}

		public async Task<T?> LoadAsync<T>(string key) {
			if (!File.Exists(PathFor(key))) return default;

			var json = await File.ReadAllTextAsync(PathFor(key));
			return JsonSerializer.Deserialize<T>(json);
		}

		public Task<bool> ExistsAsync(string key) => Task.FromResult(File.Exists(PathFor(key)));

		public Task DeleteAsync(string key) {
			var path = PathFor(key);
			if (File.Exists(path)) File.Delete(path);
			return Task.CompletedTask;
		}

		public Task<List<string>> GetAllKeysAsync() {
			var files = Directory.GetFiles(_basePath, "*.json", SearchOption.AllDirectories);

			var keys = files.Select(f =>
				Path.ChangeExtension(Path.GetRelativePath(_basePath, f)!, null).Replace("\\", "/")
			).ToList();

			return Task.FromResult(keys);
		}

		private string PathFor(string key) { 
			string fullPath = Path.Combine(_basePath, $"{key}.json");
			string directory = Path.GetDirectoryName(fullPath)!;
			Directory.CreateDirectory(directory!);

			return fullPath;
		}
	}

}
