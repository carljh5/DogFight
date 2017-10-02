using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingAlgorithm : MonoBehaviour
{
    public float breedingChance;
    public int averageNoOfPuppies,maxNoOfPuppies;


    public List<Dog> Breed(Dog mother, Dog father)
    {
        //virility of the mother increases chance of conception
        //virility of the father increases likelihood of more puppies

        var puppies = new List<Dog>();

        if (mother.male || !father.male)
            return puppies;


        return puppies;
    }


}
