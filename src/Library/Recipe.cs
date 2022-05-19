//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    /// <summary>
    /// Clase modficada aplicando el patrón Creator, ya que Recipe contiene y agrega instancias de 
    /// Step, Recipe debería tener la responsabilidad de crear dichas instancias.
    /// </summary>
    public class Recipe
    {
        private IList<Step> steps = new List<Step>();

        public Product FinalProduct { get; set; }

        /// <summary>
        /// El método AddStep es modificado para aplicar el patrón creator, 
        /// ahora es capaz de crear un step ya que recibe como argumento todos los datos 
        /// necesarios para crear instancias de Step, y a su vez agrega los objetos 
        /// recién creados a la lista de steps. Con lo cual es menos probable que existan 
        /// instancias de Step que no pertenezcan a alguna instancia de Recipe. 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="quantity"></param>
        /// <param name="equipment"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public Step AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
            return step;
        }

        public void RemoveStep(Step step)
        {
            this.steps.Remove(step);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (Step step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (Step step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }
    }
}