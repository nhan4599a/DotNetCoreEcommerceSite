﻿using FluentValidation;

namespace SharedModel
{
    public class ProductModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
