using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Plato : PickableReceiver
{
    [SerializeField] private List<IngredientData> _currentRecipe = new List<IngredientData>();
    private PreparedDish _currentPreparedDish;
    [SerializeField] private OrderView orderViewUI;
    public override void OnReceiveIngredient(IPickable pickable)
    {
        if (!(pickable is Ingredient)) return;

        Ingredient ingredient =  pickable as Ingredient;
        
        if (!_currentRecipe.Any())
        {
            PreparedDish newDish = Resources.Load<PreparedDish>("PreparedDish");
            _currentPreparedDish = Instantiate<PreparedDish>(newDish);
            
            
            _currentPreparedDish.MoveTo(this);

            _currentPreparedDish.OnMoveToAnotherPlace += ClearDish;
        }
        
        _currentPreparedDish.AddIngredient(ingredient.CurrentIngredientData);
        
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();
        ingredient.MoveTo(null);
        orderViewUI.SetIngredientImage(ingredient.CurrentIngredientData.grabbedImage);
    
        _currentRecipe.Add(ingredient.CurrentIngredientData);
        
        pickable.Delete();
    }

    void ClearDish()
    {
        _currentPreparedDish.OnMoveToAnotherPlace -= ClearDish;
        
        _currentRecipe.Clear();
        orderViewUI.CleanRecipe();
    }

    protected override void Update(){ }
}
