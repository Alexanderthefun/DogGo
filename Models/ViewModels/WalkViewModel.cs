﻿using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkFormViewModel
    {
        public Walks Walk { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Walker> Walkers { get; set; }
    }
}