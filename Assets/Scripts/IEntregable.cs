using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntregable
{
    void Entregar();
    List<IngredientData> GetIngredientsInOrder();

}
