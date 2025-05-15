using Microsoft.EntityFrameworkCore;
using Onlab.Dal;
using Onlab.Bll;
using Onlab_v4.Components;
using Microsoft.AspNetCore.Identity;
using Onlab_v4.Components.Account;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Service Registrations
// -------------------------

// Add Razor Components and WebAssembly interactive components
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Enable CORS for Blazor Client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add Entity Framework Core (from DAL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddAutoMapper(typeof(Onlab.Bll.Mappings.MappingProfile));

builder.Services.AddBllServices();

// Add controllers for API endpoints
builder.Services.AddControllers();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<Onlab_v4.Components.Account.IdentityRedirectManager>();
builder.Services.AddScoped<Onlab_v4.Components.Account.IdentityUserAccessor>();
builder.Services.AddTransient<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = IdentityConstants.ApplicationScheme;
//    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//})
//    .AddIdentityCookies();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(/* options */)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders(); // You usually need token providers too
                                 // Potentially add other required Identity services like .AddApiEndpoints() if using that template


// Register Swagger services (must be before building the app)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
//builder.Services.AddSwaggerGen();

// -------------------------
// Build the Application
// -------------------------
var app = builder.Build();

// -------------------------
// Middleware Configuration
// -------------------------

if (app.Environment.IsDevelopment())
{
    // Enable Swagger and WebAssembly debugging in development
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Use HTTPS redirection and serve static files
app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable CORS with the defined policy
app.UseCors("AllowBlazorClient");

// Use routing (needed for controllers and Razor components)
app.UseRouting();

// (Optional) Use Antiforgery if needed
app.UseAntiforgery();

// Use authorization middleware (if you have any authorization configured)
app.UseAuthorization();

// Map API controllers
app.MapControllers();

// app.Services.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();

// Map Razor Components for the Blazor client
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Onlab_v4.Client._Imports).Assembly);

// Run the application
app.Run();
