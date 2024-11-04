const recipes = [
  {
    id: 101,
    title: "Creamy Alfredo Pasta",
    description: "A delicious creamy pasta with Alfredo sauce.",
    ingredients: [
      "pasta",
      "heavy cream",
      "parmesan cheese",
      "butter",
      "garlic",
    ],
    instructions: [
      "Cook pasta according to package instructions.",
      "Melt butter in a pan and add garlic.",
      "Add heavy cream and parmesan cheese, stirring until smooth.",
      "Combine pasta with sauce, mix well, and serve.",
    ],
    servings: 4,
    cookingTime: 20,
    prepTime: 10,
    cuisine: "Italian",
    category: "Pasta",
    image: "../../public/Creamy alfredo pasta.png",
    userId: 1,
    nutritionFacts: {
      calories: 600,
      carbohydrates: 50,
      protein: 20,
      fat: 35,
    },
    reviews: [
      { userId: 2, comment: "Loved the creamy texture!", rating: 5 },
      {
        userId: 3,
        comment: "Very tasty but a bit too rich for me.",
        rating: 4,
      },
    ],
  },
  {
    id: 102,
    title: "Classic Chocolate Cake",
    description: "A rich and moist chocolate cake.",
    ingredients: [
      "flour",
      "sugar",
      "cocoa powder",
      "eggs",
      "butter",
      "baking powder",
    ],
    instructions: [
      "Preheat oven to 350°F (175°C).",
      "Mix dry ingredients together in a bowl.",
      "Add wet ingredients and mix until smooth.",
      "Pour batter into a greased pan and bake for 30-35 minutes.",
      "Let cool before serving.",
    ],
    servings: 8,
    cookingTime: 45,
    prepTime: 15,
    cuisine: "American",
    category: "Dessert",
    image: "../../public/Classic Chocolate Cake.png",
    userId: 2,
    nutritionFacts: {
      calories: 400,
      carbohydrates: 60,
      protein: 5,
      fat: 20,
    },
    reviews: [
      { userId: 4, comment: "Perfectly moist and chocolaty!", rating: 5 },
      { userId: 5, comment: "Great taste but a bit too sweet.", rating: 4 },
    ],
  },
  {
    id: 103,
    title: "Margherita Pizza",
    description:
      "Classic Italian pizza with fresh tomatoes, mozzarella, and basil.",
    ingredients: [
      "pizza dough",
      "tomato sauce",
      "mozzarella cheese",
      "basil",
      "olive oil",
    ],
    instructions: [
      "Preheat oven to 475°F (245°C).",
      "Roll out pizza dough on a floured surface.",
      "Spread tomato sauce on dough, then add mozzarella and basil.",
      "Bake until crust is golden and cheese is melted, about 10-12 minutes.",
    ],
    servings: 2,
    cookingTime: 15,
    prepTime: 20,
    cuisine: "Italian",
    category: "Pizza",
    image: "../../public/Margherita Pizza.png",
    userId: 3,
    nutritionFacts: {
      calories: 700,
      carbohydrates: 90,
      protein: 30,
      fat: 25,
    },
    reviews: [
      { userId: 1, comment: "Simple and delicious!", rating: 5 },
      { userId: 6, comment: "Tasted fresh, loved the basil!", rating: 4 },
    ],
  },
  {
    id: 104,
    title: "Chicken Tikka Masala",
    description: "Spicy grilled chicken in a creamy tomato curry sauce.",
    ingredients: ["chicken", "yogurt", "spices", "tomato paste", "cream"],
    instructions: [
      "Marinate chicken in yogurt and spices for at least 1 hour.",
      "Grill or sear chicken until cooked through.",
      "Prepare sauce by cooking spices and tomato paste, then add cream.",
      "Combine chicken with sauce and simmer until thickened.",
    ],
    servings: 4,
    cookingTime: 30,
    prepTime: 60,
    cuisine: "Indian",
    category: "Main Course",
    image: "../../public/Chicken Tikka Masala.png",
    userId: 4,
    nutritionFacts: {
      calories: 600,
      carbohydrates: 15,
      protein: 40,
      fat: 35,
    },
    reviews: [
      {
        userId: 2,
        comment: "Excellent flavors, just like a restaurant!",
        rating: 5,
      },
      {
        userId: 7,
        comment: "Good, but a bit too spicy for my taste.",
        rating: 3,
      },
    ],
  },
  {
    id: 105,
    title: "Avocado Toast",
    description: "A healthy toast topped with creamy avocado and seasonings.",
    ingredients: ["bread", "avocado", "salt", "pepper", "lemon juice"],
    instructions: [
      "Toast the bread to desired crispiness.",
      "Mash avocado in a bowl and season with salt, pepper, and lemon juice.",
      "Spread avocado mixture on toasted bread and serve.",
    ],
    servings: 1,
    cookingTime: 5,
    prepTime: 5,
    cuisine: "American",
    category: "Breakfast",
    image: "../../public/Avocado Toast.png",
    userId: 5,
    nutritionFacts: {
      calories: 250,
      carbohydrates: 30,
      protein: 5,
      fat: 15,
    },
    reviews: [
      { userId: 3, comment: "Quick, easy, and delicious!", rating: 5 },
      {
        userId: 6,
        comment: "Could use more seasoning, but good overall.",
        rating: 4,
      },
    ],
  },
  {
    id: 106,
    title: "Caesar Salad",
    description:
      "A classic Caesar salad with romaine lettuce, croutons, and Caesar dressing.",
    ingredients: [
      "romaine lettuce",
      "croutons",
      "parmesan cheese",
      "Caesar dressing",
    ],
    instructions: [
      "Chop romaine lettuce and place in a large bowl.",
      "Add croutons and parmesan cheese.",
      "Toss with Caesar dressing until evenly coated and serve.",
    ],
    servings: 2,
    cookingTime: 0,
    prepTime: 10,
    cuisine: "American",
    category: "Salad",
    image: "../../public/Caesar Salad.png",
    userId: 6,
    nutritionFacts: {
      calories: 200,
      carbohydrates: 10,
      protein: 10,
      fat: 15,
    },
    reviews: [
      { userId: 3, comment: "Simple and refreshing!", rating: 4 },
      {
        userId: 5,
        comment: "Could use more dressing, but overall tasty.",
        rating: 3,
      },
    ],
  },
  {
    id: 107,
    title: "Beef Tacos",
    description: "Tasty beef tacos with fresh toppings and soft tortillas.",
    ingredients: [
      "ground beef",
      "taco seasoning",
      "tortillas",
      "lettuce",
      "cheese",
    ],
    instructions: [
      "Cook ground beef in a skillet and season with taco seasoning.",
      "Warm tortillas in a pan or microwave.",
      "Assemble tacos by adding beef, lettuce, and cheese to tortillas.",
    ],
    servings: 4,
    cookingTime: 15,
    prepTime: 10,
    cuisine: "Mexican",
    category: "Main Course",
    image: "../../public/Beef Tacos.png",
    userId: 7,
    nutritionFacts: {
      calories: 500,
      carbohydrates: 45,
      protein: 25,
      fat: 20,
    },
    reviews: [
      { userId: 4, comment: "Perfectly seasoned and delicious!", rating: 5 },
      {
        userId: 6,
        comment: "Good, but could use a bit more spice.",
        rating: 4,
      },
    ],
  },
  {
    id: 108,
    title: "French Onion Soup",
    description:
      "A rich, flavorful soup with caramelized onions and melted cheese.",
    ingredients: [
      "onions",
      "beef broth",
      "butter",
      "French bread",
      "gruyere cheese",
    ],
    instructions: [
      "Caramelize onions in butter until deeply browned.",
      "Add beef broth and simmer for 20 minutes.",
      "Top soup with French bread and cheese, then broil until cheese melts.",
    ],
    servings: 3,
    cookingTime: 40,
    prepTime: 15,
    cuisine: "French",
    category: "Soup",
    image: "../../public/French Onion Soup.png",
    userId: 1,
    nutritionFacts: {
      calories: 300,
      carbohydrates: 20,
      protein: 10,
      fat: 15,
    },
    reviews: [
      { userId: 2, comment: "Loved the flavor and cheese topping!", rating: 5 },
      { userId: 5, comment: "Good, but too rich for a full bowl.", rating: 3 },
    ],
  },
  {
    id: 109,
    title: "Falafel Wrap",
    description:
      "A tasty vegetarian wrap with crispy falafel, veggies, and tahini sauce.",
    ingredients: ["falafel", "pita bread", "lettuce", "tomato", "tahini sauce"],
    instructions: [
      "Prepare falafel according to recipe.",
      "Stuff pita with falafel, lettuce, and tomato.",
      "Drizzle with tahini sauce and serve.",
    ],
    servings: 1,
    cookingTime: 10,
    prepTime: 5,
    cuisine: "Middle Eastern",
    category: "Sandwich",
    image: "../../public/Falafel Wrap.png",
    userId: 4,
    nutritionFacts: {
      calories: 300,
      carbohydrates: 40,
      protein: 10,
      fat: 15,
    },
    reviews: [
      { userId: 3, comment: "Crispy and full of flavor!", rating: 5 },
      { userId: 7, comment: "Good for a quick meal.", rating: 4 },
    ],
  },
  {
    id: 110,
    title: "Pumpkin Pie",
    description:
      "A classic fall dessert with spiced pumpkin filling and flaky crust.",
    ingredients: ["pumpkin puree", "pie crust", "sugar", "cinnamon", "nutmeg"],
    instructions: [
      "Prepare filling and pour into pie crust.",
      "Bake until filling is set and crust is golden.",
      "Let cool before serving.",
    ],
    servings: 8,
    cookingTime: 60,
    prepTime: 20,
    cuisine: "American",
    category: "Dessert",
    image: "",
    userId: 5,
    nutritionFacts: {
      calories: 350,
      carbohydrates: 45,
      protein: 5,
      fat: 15,
    },
    reviews: [
      { userId: 1, comment: "Perfectly spiced and delicious!", rating: 5 },
      {
        userId: 6,
        comment: "Good texture but could use more cinnamon.",
        rating: 4,
      },
    ],
  },
];

export default recipes;

// function calculateAverageRating(recipe) {
//   if (!recipe.reviews || recipe.reviews.length === 0) return 0;

//   const totalRating = recipe.reviews.reduce(
//     (sum, review) => sum + review.rating,
//     0
//   );
//   return (totalRating / recipe.reviews.length).toFixed(1); // Returns a one-decimal average
// }

// // Example usage to display average rating for each recipe
// recipes.forEach((recipe) => {
//   const averageRating = calculateAverageRating(recipe);
//   console.log(`Average Rating for ${recipe.title}: ${averageRating} stars`);
// });
