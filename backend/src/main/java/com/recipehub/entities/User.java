package com.recipehub.entities;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.recipehub.constants.StringConstants;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;

import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "users")
public class User {

    @OneToMany(mappedBy = "user", cascade = CascadeType.ALL)
    @JsonIgnore
    private final Set<Recipe> recipes = new HashSet<>();

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @NotBlank(message = "username" + StringConstants.REQUIRED_FIELD)
    private String username;
    @NotBlank(message = "password" + StringConstants.REQUIRED_FIELD)
    private String password;
    @NotBlank(message = "fullName" + StringConstants.REQUIRED_FIELD)
    private String fullName;
    @NotBlank(message = "email" + StringConstants.REQUIRED_FIELD)
    private String email;
    @NotBlank(message = "profilePicture" + StringConstants.REQUIRED_FIELD)
    private String profilePicture;

    @ManyToMany
    @JoinTable(name = "user_favorite_recipes",
    joinColumns = @JoinColumn(name = "user_id"),
    inverseJoinColumns = @JoinColumn(name = "recipe_id"))
    private final Set<Recipe> favoriteRecipes = new HashSet<>();

    @ManyToMany
    @JoinTable(name = "user_saved_recipes",
            joinColumns = @JoinColumn(name = "user_id"),
            inverseJoinColumns = @JoinColumn(name = "recipe_id"))
    private final Set<Recipe> savedRecipes = new HashSet<>();

    public User() {
    }

    public User(
                String profilePicture,
                String email,
                String fullName,
                String password,
                String username) {
        this.profilePicture = profilePicture;
        this.email = email;
        this.fullName = fullName;
        this.password = password;
        this.username = username;
    }

    @Override
    public String toString() {
        return "User{" +
                "recipes=" + recipes +
                ", id=" + id +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                ", fullName='" + fullName + '\'' +
                ", email='" + email + '\'' +
                ", profilePicture='" + profilePicture + '\'' +
                ", favoriteRecipes=" + favoriteRecipes +
                ", savedRecipes=" + savedRecipes +
                '}';
    }

    public Set<Recipe> getRecipes() {
        return recipes;
    }

    public String getProfilePicture() {
        return profilePicture;
    }

    public void setProfilePicture(String profilePicture) {
        this.profilePicture = profilePicture;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Set<Recipe> getFavoriteRecipes() {
        return favoriteRecipes;
    }

    public Set<Recipe> getSavedRecipes() {
        return savedRecipes;
    }
}
