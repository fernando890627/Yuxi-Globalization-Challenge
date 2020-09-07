using CompanyWebPage.Resources;
using System;
using System.ComponentModel.DataAnnotations;


namespace CompanyWebPage.ViewModel
{
    public class NewsleterSubscribeViewModel
    {
        [Display(Name = "YourAge", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "AgeIsIncorrect")]
        [Range(1, 99, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "AgeIsIncorrect")]
        public int? Age { get; set; }

        [Display(Name = "YourEmailAddress", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "EmailCanNotBeEmpty")]
        public string EmailAddress { get; set; }

        [Display(Name = "YourFirstName", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "FirstNameCanNotBeEmpty")]
        public string FirstName { get; set; }
    }
}
