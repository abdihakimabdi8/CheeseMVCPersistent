
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCategoryViewModel
    {
        [Display(Name = "Category Name")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please provide a cheese catogory")]
        public string Name { get; set; }

        public Cheese Cheeses { get; set; }

    }
}
