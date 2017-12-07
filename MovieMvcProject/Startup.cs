
using Microsoft.Owin;
using MovieMvcProject.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieMvcProject.Startup))]
namespace MovieMvcProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }

       

       
	

	}
}

