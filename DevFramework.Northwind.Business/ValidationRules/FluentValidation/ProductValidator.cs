using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("ProductName field cannot be empty!");
            RuleFor(p => p.QuantityPerUnit).MaximumLength(20).WithMessage("QuantityPerUnit field cannot be greater than 20 characters!");

            // CategoryId si 6 olan ürünün fiyatı 150 birimden az olmalıdır gibi...
            RuleFor(p => p.UnitPrice).LessThan(150).When(p => p.CategoryId == 6);

            // Kendi kurallarımızı da yazabilmemize olanak tanıyor.
            // RuleFor(p => p.ProductName).Must(StartWithControl);

        }

        private bool StartWithControl(string arg) => arg.StartsWith("P-2022");
    }
}
