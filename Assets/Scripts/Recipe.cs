using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
   public string dishName;
   public IngredientData[] ingredients;
}
