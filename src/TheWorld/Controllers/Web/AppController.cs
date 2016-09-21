using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheWorld.Services;
using TheWorld.ViewModels;


namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService ;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService,IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Contact()
        {
            //throw new InvalidOperationException("bad things happen");
            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            
            _mailService.SendMail(_config["MailSettings:ToAddress"],model.Email, "from theworld" , model.Message);

            return View(model);
        }
        public IActionResult About()
        {
            return View();
        }    
        
    }
}
