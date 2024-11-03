package com.recipehub.entities;

import jakarta.persistence.*;
import com.recipehub.constants.StringConstants;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;

import java.util.List;

@Entity
@Table(name = "recipes")
public class Recipe {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @NotBlank(message = "title" + StringConstants.REQUIRED_FIELD)
    private String title;

    @NotBlank(message = "description" + StringConstants.REQUIRED_FIELD)
    private String description;

    @NotBlank(message = "image" + StringConstants.REQUIRED_FIELD)
    private String image;

    @ElementCollection
    private List<String> ingredients;

    @NotBlank(message = "instruction" + StringConstants.REQUIRED_FIELD)
    private String instructions;

    @Positive(message = "servings" + StringConstants.INVALID_POSITIVE)
    private Integer servings;

    @Positive(message = "prepTime" + StringConstants.INVALID_POSITIVE)
    private Integer prepTime;

    @NotBlank(message = "cuisine" + StringConstants.REQUIRED_FIELD)
    private String cuisine;

    @Embedded
    private  NutritionFacts nutritionFacts;

    @ManyToOne
    @NotNull
    private User user;

    public Recipe() {}

    public Recipe(
            NutritionFacts nutritionFacts,
            String cuisine,
            Integer prepTime,
            Integer servings,
            String instructions,
            List<String> ingredients,
            String image,
            String description,
            String title,
            User user) {
        this.nutritionFacts = nutritionFacts;
        this.cuisine = cuisine;
        this.prepTime = prepTime;
        this.servings = servings;
        this.instructions = instructions;
        this.ingredients = ingredients;
        this.image = image;
        this.description = description;
        this.title = title;
        this.user = user;
    }

    @Override
    public String toString() {
        return "Recipe{" +
                "id=" + id +
                ", title='" + title + '\'' +
                ", description='" + description + '\'' +
                ", image='" + image + '\'' +
                ", ingredients=" + ingredients +
                ", instructions='" + instructions + '\'' +
                ", servings=" + servings +
                ", prepTime=" + prepTime +
                ", cuisine='" + cuisine + '\'' +
                ", nutritionFacts=" + nutritionFacts +
                ", user=" + user +
                '}';
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public NutritionFacts getNutritionFacts() {
        return nutritionFacts;
    }

    public void setNutritionFacts(NutritionFacts nutritionFacts) {
        this.nutritionFacts = nutritionFacts;
    }

    public String getCuisine() {
        return cuisine;
    }

    public void setCuisine(String cuisine) {
        this.cuisine = cuisine;
    }

    public Integer getPrepTime() {
        return prepTime;
    }

    public void setPrepTime(Integer prepTime) {
        this.prepTime = prepTime;
    }

    public Integer getServings() {
        return servings;
    }

    public void setServings(Integer servings) {
        this.servings = servings;
    }

    public String getInstructions() {
        return instructions;
    }

    public void setInstructions(String instructions) {
        this.instructions = instructions;
    }

    public List<String> getIngredients() {
        return ingredients;
    }

    public void setIngredients(List<String> ingredients) {
        this.ingredients = ingredients;
    }

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }
}
