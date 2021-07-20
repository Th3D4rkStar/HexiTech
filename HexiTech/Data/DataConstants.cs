﻿namespace HexiTech.Data
{
    public class DataConstants
    {
        public class Product
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;

            public const int SeriesMaxLength = 15;

            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 15;

            public const int DescriptionMinLength = 10;
        }

        public class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public class ProductType
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
        }
    }
}