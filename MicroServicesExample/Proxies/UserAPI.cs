using System.Text;
using System.Text.Json;
using AutoMapper;
using MicroServicesExample.Models;

namespace MicroServicesExample.Proxies;
public class UserProxy
{
    private readonly HttpClient _UserClient;
    private readonly IMapper _mapper;
    public UserProxy(HttpClient UserClient, IConfiguration configuration, IMapper mapper)
    {
        _UserClient = UserClient;
        _UserClient.BaseAddress = new Uri(configuration.GetValue<string>("Urls:UserAPI")!);
        _mapper = mapper;
    }

    public async Task<Users> GetUser(string Email)
    {
        var result = _UserClient.GetAsync($"GetUser?Email={Email}").Result;
        var jsonString = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Users>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task AddUser(AddUserDTO user)
    {
        Users newUser = _mapper.Map<Users>(user);
        var jsonPayload = JsonSerializer.Serialize(newUser);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        await _UserClient.PostAsync("AddUser", content);
    }

    public async Task DeleteUser(string Email)
    {
        await _UserClient.DeleteAsync($"DeleteUser?Email={Email}");
    }

    public async Task<Users> UpdatePassword(string Email, string Password)
    {
        var jsonPayload = JsonSerializer.Serialize(new { Email, Password });
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _UserClient.PutAsync($"UpdatePassword", content);
        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Users>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}