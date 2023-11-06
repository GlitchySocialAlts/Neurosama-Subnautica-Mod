namespace SCHIZO.Items.Data.Crafting;

partial class Ingredient
{
    private NIngredient _converted;

    public NIngredient Convert()
    {
        if (_converted != null) return _converted;

        return _converted = new NIngredient(item.GetTechType(), amount);
    }

    public static bool IsValid(Ingredient ingredient)
    {
        if (ingredient.amount <= 0) return false;
        return Item.IsValid(ingredient.item);
    }
}
