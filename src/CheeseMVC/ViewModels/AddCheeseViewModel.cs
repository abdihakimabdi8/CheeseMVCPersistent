using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        private List<CheeseCategory> categories;

        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Provide Category")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<CheeseCategory> Category { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public AddCheeseViewModel() { }

        public AddCheeseViewModel(Cheese cheese, IEnumerable<CheeseCategory> categories )
        {

            Categories = new List<SelectListItem>();

            // <option value="0">Hard</option>
            foreach(CheeseCategory category in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = ((int)CategoryID).ToString(),
                    Text = Name.ToString()
                });
            }

        }

        public AddCheeseViewModel(List<CheeseCategory> list)
        {
#pragma warning disable CS1717 // Assignment made to same variable
            this.categories = categories;
#pragma warning restore CS1717 // Assignment made to same variable
        }
    }
}