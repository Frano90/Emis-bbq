using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
   public string dishName;
   public IngredientData[] ingredients;

   public int GetTotalCost()
   {
      int total = 0;
      for (int i = 0; i < ingredients.Length; i++)
      {
         total += ingredients[i].cost;
      }

      return total;
   }
}
