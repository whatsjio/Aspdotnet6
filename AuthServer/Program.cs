using AuthServer;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using System.Text.Encodings.Web;
using System.Text.Unicode;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//获取指定配置路径
builder.Host.ConfigureAppConfiguration(app =>
{
    app.AddKeyPerFile(directoryPath: "/run/secrets", optional: true);
});

string connectstr = null;
connectstr = builder.Configuration["Movies:ConnectionString"];

//感知docker密钥信息
if (connectstr == null)
{
    var sercertstr = builder.Configuration["Movies_ServiceApiKey"];
    if (!string.IsNullOrEmpty(sercertstr))
        connectstr = JsonConvert.DeserializeObject<JToken>(sercertstr).GetTrueValue<string>("mysqlconnectstr");
}

//初始化日志组件
var logger = LogManager.Setup().RegisterNLogWeb().GetCurrentClassLogger();
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
}).UseNLog();

//redis配置
var sectionredis = builder.Configuration.GetSection("Redis:Default");
string redisconnectionString = sectionredis.GetSection("Connection").Value;
string redisinstanceName = sectionredis.GetSection("InstanceName").Value;
string redissyscustomkey = sectionredis.GetSection("SysCustomKey").Value;
int redisdefaultDB = int.Parse(sectionredis.GetSection("DefaultDB").Value ?? "0");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => {
    builder.Register(c =>
    {
        //配置mysql
        var optionsBuilder = new DbContextOptionsBuilder<DbTContext>();
        optionsBuilder.UseMySql(connectstr, MySqlServerVersion.LatestSupportedServerVersion);
        return optionsBuilder.Options;
    }).InstancePerLifetimeScope();
    builder.RegisterType<DbTContext>().AsSelf().InstancePerLifetimeScope();
    builder.Register(c => new RedisHelper(redisconnectionString, redisinstanceName, redissyscustomkey, redisdefaultDB)).AsSelf().InstancePerLifetimeScope();
    //新模块组件注册    
    builder.RegisterModule<AutofacModuleRegister>();
});

//AddHttpClient 来注册 IHttpClientFactory
builder.Services.AddHttpClient();



//加载鉴权地址
AppraisalUrl = builder.Configuration["Appraisalurl"];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins(AppraisalUrl).AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.GetApiResources())//配置类定义的授权范围
            .AddInMemoryApiScopes(Config.GetApiScopes())
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryClients(Config.GetClients()); //配置类定义的授权客户端

builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
builder.Services.AddTransient<IProfileService, ProfileService>();

builder.Services.AddControllersWithViews().AddJsonOptions(options => {
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseIdentityServer();

//初始化数据库
using (var scope = AutofacModuleRegister.GetContainer().BeginLifetimeScope())
{
    var dbint = scope.Resolve<DbTContext>();
    dbint.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();



app.MapRazorPages();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Login}/{id?}");
});
app.Run();



partial class Program
{

    /// <summary>
    /// 鉴权地址
    /// </summary>
    public static string AppraisalUrl { get; private set; }
}