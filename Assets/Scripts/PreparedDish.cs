using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedDish : KitchenItem, IEntregable 
{
    [SerializeField] private List<IngredientData> _currentRecipe = new List<IngredientData>();

    private List<GameObject> ingredientsInDish = new List<GameObject>();

    Vector3 stackOriginPoint;

    [SerializeField] private LayerMask raycastMask;

    public void SetOriginPosition(Vector3 pos)
    {
        stackOriginPoint = pos;
    }
    public void AddIngredient(IngredientData iData)
    {
        var ingredientPrefab = Resources.Load<GameObject>("IngridientsModelsAsPrefabs/" + iData.viewModel.name);

        var newIngredientSpawned = Instantiate(ingredientPrefab, transform);

        ingredientsInDish.Add(newIngredientSpawned);

        if (_currentRecipe.Count == 0)
        {
            newIngredientSpawned.transform.position = stackOriginPoint;
        }
        else
        {
            Vector3 originPoint = transform.position + Vector3.up;

            Ray ray = new Ray(originPoint, Vector3.down);

            RaycastHit hit;
        
            Physics.Raycast(ray, out hit, 100f, raycastMask);

            Debug.Log(hit.collider.gameObject.name);
        
            newIngredientSpawned.transform.position = hit.point;    
        }
        
        _currentRecipe.Add(iData);

    }

    public List<IngredientData> GetIngredientsInOrder() => _currentRecipe;
    
    public void Entregar()
    {
        
    }
}
