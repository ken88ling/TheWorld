﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;


namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        //private WorldContext _context;
        private IWorldRepository _repository;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
        }
        public IActionResult Index()
        {
            //var data = _context.Trips.ToList();
            var data = _repository.GetAllTrips();
            return View(data);
        }

        public IActionResult Contact()
        {
            //throw new InvalidOperationException("bad things happen");
            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {

            if (model.Email.Contains("aol.com")) ModelState.AddModelError("", "We don't support AOL address");
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "from theworld", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message sent";
            }

            return View();
        }
        public IActionResult About()
        {
            return View();
        }

    }
}
