using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class DogFormViewModel
    {
        public Dog Dog { get; set; }
        public Owner Owner { get; set; }
        public Walker Walker { get; set; }
    }
}