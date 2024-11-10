# Fetch data from external Rest_Api using Asp.net Mvc App
## Steps

1. Create a new mvc app

```bash
dotnet new mvc -o ReatApiExmaple
cd ReatApiApp

```

2. Create a new model Post.cs and add the following

```csharp

namespace RestApiExample.Models
{
public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}
}
```
3. Create a Services Class, create a new class ApiService.cs 

```bash
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using RestApiExample.Models;


namespace RestApiExample.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");
            return response ?? new List<Post>();
        }
    }
}


```

4. Register the Service in Program.cs

```csharp
// Register HttpClient and ApiService
builder.Services.AddHttpClient<ApiService>();

// Add services to the container.
builder.Services.AddControllersWithViews()
```

5. Create a PostController.cs Controller to Use the Service

```csharp
using Microsoft.AspNetCore.Mvc;
using RestApiExample.Services;
using System.Threading.Tasks;

namespace RestApiExample.Controllers
{
    public class PostController : Controller
    {
        private readonly ApiService _apiService;

        public PostController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
             var posts = await _apiService.GetPostsAsync();
            return View(posts); 
           
        }
    }
}
```

6. Create the Index View for Posts: Add an Index.cshtml view in Views/Posts

```csharp
@model List<MvcRestApiExample.Services.Post>

<h2>Posts</h2>

<table class="table">
    <thead>
        <tr>
            <th>Title</th>
            <th>Body</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var post in Model)
        {
            <tr>
                <td>@post.Title</td>
                <td>@post.Body</td>
            </tr>
        }
    </tbody>
</table>

```
