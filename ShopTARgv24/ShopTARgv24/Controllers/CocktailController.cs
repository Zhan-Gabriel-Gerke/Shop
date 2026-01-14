using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Dto.CocktailDto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Models.Cocktail;

namespace ShopTARgv24.Controllers;

public class CocktailController : Controller
{
    private readonly ICocktailServices _cocktailServices;

        public CocktailController
             (
                 ICocktailServices cocktailServices
             )
        {
            _cocktailServices = cocktailServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCocktails(SearchCocktailViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Cocktail", "Cocktail", new { cocktail = model.SearchCocktail });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Cocktail(string cocktail)
        {
            CocktailResultDto dto = new()
            {
                StrDrink = cocktail
            };

            var cocktailResult = await _cocktailServices.GetCocktails(dto);

            CocktailViewModel vm = new();

            vm.IdDrink = cocktailResult.Drinks[0].IdDrink;
            vm.StrDrink = cocktailResult.Drinks[0].StrDrink;
            vm.StrDrinkAlternate = cocktailResult.Drinks[0].StrDrinkAlternate;
            vm.StrTags = cocktailResult.Drinks[0].StrTags;
            vm.StrVideo = cocktailResult.Drinks[0].StrVideo;
            vm.StrCategory = cocktailResult.Drinks[0].StrCategory;
            vm.StrIBA = cocktailResult.Drinks[0].StrIBA;
            vm.StrAlcoholic = cocktailResult.Drinks[0].StrAlcoholic;
            vm.StrGlass = cocktailResult.Drinks[0].StrGlass;
            vm.StrInstructions = cocktailResult.Drinks[0].StrInstructions;
            vm.StrInstructionsES = cocktailResult.Drinks[0].StrInstructionsES;
            vm.StrInstructionsDE = cocktailResult.Drinks[0].StrInstructionsDE;
            vm.StrInstructionsFR = cocktailResult.Drinks[0].StrInstructionsFR;
            vm.StrInstructionsIT = cocktailResult.Drinks[0].StrInstructionsIT;
            vm.StrInstructionsZHHANS = cocktailResult.Drinks[0].StrInstructionsZHHANS;
            vm.StrInstructionsZHHANT = cocktailResult.Drinks[0].StrInstructionsZHHANT;
            vm.StrDrinkThumb = cocktailResult.Drinks[0].StrDrinkThumb;
            vm.StrIngredient1 = cocktailResult.Drinks[0].StrIngredient1;
            vm.StrIngredient2 = cocktailResult.Drinks[0].StrIngredient2;
            vm.StrIngredient3 = cocktailResult.Drinks[0].StrIngredient3;
            vm.StrIngredient4 = cocktailResult.Drinks[0].StrIngredient4;
            vm.StrIngredient5 = cocktailResult.Drinks[0].StrIngredient5;
            vm.StrIngredient6 = cocktailResult.Drinks[0].StrIngredient6;
            vm.StrIngredient7 = cocktailResult.Drinks[0].StrIngredient7;
            vm.StrIngredient8 = cocktailResult.Drinks[0].StrIngredient8;
            vm.StrIngredient9 = cocktailResult.Drinks[0].StrIngredient9;
            vm.StrIngredient10 = cocktailResult.Drinks[0].StrIngredient10;
            vm.StrIngredient11 = cocktailResult.Drinks[0].StrIngredient11;
            vm.StrIngredient12 = cocktailResult.Drinks[0].StrIngredient12;
            vm.StrIngredient13 = cocktailResult.Drinks[0].StrIngredient13;
            vm.StrIngredient14 = cocktailResult.Drinks[0].StrIngredient14;
            vm.StrIngredient15 = cocktailResult.Drinks[0].StrIngredient15;
            vm.StrMeasure1 = cocktailResult.Drinks[0].StrMeasure1;
            vm.StrMeasure2 = cocktailResult.Drinks[0].StrMeasure2;
            vm.StrMeasure3 = cocktailResult.Drinks[0].StrMeasure3;
            vm.StrMeasure4 = cocktailResult.Drinks[0].StrMeasure4;
            vm.StrMeasure5 = cocktailResult.Drinks[0].StrMeasure5;
            vm.StrMeasure6 = cocktailResult.Drinks[0].StrMeasure6;
            vm.StrMeasure7 = cocktailResult.Drinks[0].StrMeasure7;
            vm.StrMeasure8 = cocktailResult.Drinks[0].StrMeasure8;
            vm.StrMeasure9 = cocktailResult.Drinks[0].StrMeasure9;
            vm.StrMeasure10 = cocktailResult.Drinks[0].StrMeasure10;
            vm.StrMeasure11 = cocktailResult.Drinks[0].StrMeasure11;
            vm.StrMeasure12 = cocktailResult.Drinks[0].StrMeasure12;
            vm.StrMeasure13 = cocktailResult.Drinks[0].StrMeasure13;
            vm.StrMeasure14 = cocktailResult.Drinks[0].StrMeasure14;
            vm.StrMeasure15 = cocktailResult.Drinks[0].StrMeasure15;
            vm.StrImageSource = cocktailResult.Drinks[0].StrImageSource;
            vm.StrImageAttribution = cocktailResult.Drinks[0].StrImageAttribution;
            vm.StrCreativeCommonsConfirmed = cocktailResult.Drinks[0].StrCreativeCommonsConfirmed;
            vm.DateModified = cocktailResult.Drinks[0].DateModified;

            return View(vm);
        }
}