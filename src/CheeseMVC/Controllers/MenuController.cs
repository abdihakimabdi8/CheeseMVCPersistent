﻿using System;
using System.Collections.Generic;
using System.Linq;
using CheeseMVC.Data;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        MenuController (CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {

            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("/Menu");
             }
            return View(addMenuViewModel);
         }
            
        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();
            Menu menu = context.Menus.Single(m => m.ID == id);

            ViewMenuViewModel viewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
               
            };

            return View(viewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();

            return View(new AddMenuItemViewModel(menu, cheeses));
     
        }
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
            {
                if (ModelState.IsValid)
                {
                    var cheeseID = addMenuItemViewModel.CheeseID;
                    var menuID = addMenuItemViewModel.MenuID;
                    IList<CheeseMenu> existingItems = context.CheeseMenus
                        .Where(cm => cm.CheeseID == cheeseID)
                        .Where(cm => cm.MenuID == menuID).ToList();
                    if(existingItems.Count == 0)
                    {
                        CheeseMenu menuItem = new CheeseMenu
                        {
                            Cheese = context.Cheeses.Single(cm => cm.ID == cheeseID),
                            Menu = context.Menus.Single(cm => cm.ID == menuID)
                        };
                        context.CheeseMenus.Add(menuItem);
                        context.SaveChanges();
                    }
                    return Redirect(string.Format("/Menu/ViewMenu{ 0}", addMenuItemViewModel));
                }
            return View(addMenuItemViewModel);
        }
    }
}

 
        