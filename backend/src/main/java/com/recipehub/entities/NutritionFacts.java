package com.recipehub.entities;

import com.recipehub.constants.StringConstants;
import jakarta.persistence.Embeddable;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Positive;

@Embeddable
public class NutritionFacts {

    @Positive(message = "calories" + StringConstants.INVALID_POSITIVE)
    private Integer calories;

    @Positive(message = "carbohydrates" + StringConstants.INVALID_POSITIVE)
    private Integer carbohydrates;

    @Positive(message = "protein" + StringConstants.INVALID_POSITIVE)
    private Integer protein;

    @Positive(message = "fat" + StringConstants.INVALID_POSITIVE)
    private Integer fat;

    public NutritionFacts() {}

    public NutritionFacts(Integer calories,
                          Integer fat,
                          Integer protein,
                          Integer carbohydrates) {
        this.calories = calories;
        this.fat = fat;
        this.protein = protein;
        this.carbohydrates = carbohydrates;
    }

    @Override
    public String toString() {
        return "NutritionFacts{" +
                "calories=" + calories +
                ", carbohydrates=" + carbohydrates +
                ", protein=" + protein +
                ", fat=" + fat +
                '}';
    }

    public Integer getCalories() {
        return calories;
    }

    public void setCalories(Integer calories) {
        this.calories = calories;
    }

    public Integer getFat() {
        return fat;
    }

    public void setFat(Integer fat) {
        this.fat = fat;
    }

    public Integer getProtein() {
        return protein;
    }

    public void setProtein(Integer protein) {
        this.protein = protein;
    }

    public Integer getCarbohydrates() {
        return carbohydrates;
    }

    public void setCarbohydrates(Integer carbohydrates) {
        this.carbohydrates = carbohydrates;
    }
}
