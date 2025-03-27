using api.Models;
using BCrypt.Net;

namespace api.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Check if there's already data in the database
            if (context.Users.Any() || context.Recipes.Any())
            {
                return; // Database has been seeded already
            }

            // Add users
            var user1 = new User
            {
                Email = "chef1@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), // Never hardcode passwords in production
                FullName = "Chef Jamie",
                ProfilePicture = "https://randomuser.me/api/portraits/men/32.jpg",
                Bio = "Professional chef with 10 years of experience in Italian cuisine."
            };

            var user2 = new User
            {
                Email = "foodie@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), // Never hardcode passwords in production
                FullName = "Foodie Sarah",
                ProfilePicture = "https://randomuser.me/api/portraits/women/44.jpg",
                Bio = "Food enthusiast who loves to experiment with new flavors and dishes."
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();

            // Add recipes for user1
            var user1Recipes = new List<Recipe>
            {
                new Recipe
                {
                    Title = "Spaghetti Carbonara",
                    Image = "https://source.unsplash.com/featured/?pasta,spaghetti",
                    Description = "Classic Italian pasta dish made with eggs, cheese, pancetta, and black pepper.",
                    Ingredients = new List<string>
                    {
                        "400g spaghetti",
                        "200g pancetta or guanciale, diced",
                        "4 large eggs",
                        "100g Pecorino Romano cheese, grated",
                        "50g Parmigiano Reggiano, grated",
                        "Black pepper, freshly ground",
                        "Salt to taste"
                    },
                    Instructions = new List<string>
                    {
                        "Bring a large pot of salted water to boil and cook spaghetti until al dente.",
                        "While pasta cooks, sauté the pancetta in a large pan until crispy.",
                        "In a bowl, whisk together eggs, grated cheeses, and pepper.",
                        "Drain pasta, reserving some cooking water.",
                        "Add hot pasta to the pan with pancetta, remove from heat.",
                        "Quickly stir in the egg mixture, thinning with pasta water if needed.",
                        "Serve immediately with extra cheese and black pepper."
                    },
                    Servings = 4,
                    CookingTime = 20,
                    PrepTime = 10,
                    Cuisine = "Italian",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 520,
                        Carbohydrates = 65,
                        Protein = 25,
                        Fat = 18
                    },
                    UserId = user1.Id,
                    Author = user1
                },
                new Recipe
                {
                    Title = "Mushroom Risotto",
                    Image = "https://source.unsplash.com/featured/?risotto,mushroom",
                    Description = "Creamy Italian rice dish with mushrooms and Parmesan cheese.",
                    Ingredients = new List<string>
                    {
                        "300g Arborio rice",
                        "400g mixed mushrooms",
                        "1 onion, finely chopped",
                        "2 cloves garlic, minced",
                        "150ml white wine",
                        "1L vegetable stock, hot",
                        "50g Parmesan cheese, grated",
                        "2 tbsp butter",
                        "2 tbsp olive oil",
                        "Fresh thyme leaves",
                        "Salt and pepper to taste"
                    },
                    Instructions = new List<string>
                    {
                        "Clean and slice mushrooms. Heat stock in a separate pot.",
                        "In a large pan, heat oil and butter. Add onion and garlic, sauté until soft.",
                        "Add mushrooms and cook until they release their moisture.",
                        "Add rice and stir to coat with oil, cook for 1-2 minutes.",
                        "Pour in wine and stir until absorbed.",
                        "Gradually add hot stock, one ladle at a time, stirring constantly.",
                        "Continue adding stock until rice is creamy and al dente (about 18-20 minutes).",
                        "Remove from heat, stir in Parmesan and remaining butter.",
                        "Season with salt, pepper, and thyme. Let rest for 2 minutes before serving."
                    },
                    Servings = 4,
                    CookingTime = 30,
                    PrepTime = 15,
                    Cuisine = "Italian",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 420,
                        Carbohydrates = 65,
                        Protein = 10,
                        Fat = 12
                    },
                    UserId = user1.Id,
                    Author = user1
                },
                new Recipe
                {
                    Title = "Tiramisu",
                    Image = "https://source.unsplash.com/featured/?tiramisu,dessert",
                    Description = "Classic Italian dessert with layers of coffee-soaked ladyfingers and mascarpone cream.",
                    Ingredients = new List<string>
                    {
                        "6 egg yolks",
                        "200g granulated sugar",
                        "500g mascarpone cheese",
                        "300ml strong coffee, cooled",
                        "200g ladyfinger biscuits",
                        "2 tbsp cocoa powder",
                        "30ml Marsala wine or coffee liqueur (optional)"
                    },
                    Instructions = new List<string>
                    {
                        "Whisk egg yolks and sugar until pale and thick.",
                        "Gently fold in mascarpone cheese until smooth.",
                        "Mix coffee with Marsala wine if using.",
                        "Quickly dip ladyfingers in coffee mixture and arrange in a single layer in a dish.",
                        "Spread half the mascarpone mixture over the ladyfingers.",
                        "Repeat with another layer of soaked ladyfingers and remaining mascarpone.",
                        "Dust generously with cocoa powder.",
                        "Refrigerate for at least 6 hours or overnight before serving."
                    },
                    Servings = 8,
                    CookingTime = 30,
                    PrepTime = 20,
                    Cuisine = "Italian",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 380,
                        Carbohydrates = 32,
                        Protein = 7,
                        Fat = 24
                    },
                    UserId = user1.Id,
                    Author = user1
                },
                new Recipe
                {
                    Title = "Margherita Pizza",
                    Image = "https://source.unsplash.com/featured/?pizza,margherita",
                    Description = "Classic Neapolitan pizza with tomatoes, mozzarella, and basil.",
                    Ingredients = new List<string>
                    {
                        "500g pizza dough",
                        "200g San Marzano tomatoes, crushed",
                        "200g fresh mozzarella, sliced",
                        "Fresh basil leaves",
                        "2 tbsp olive oil",
                        "Salt to taste"
                    },
                    Instructions = new List<string>
                    {
                        "Preheat oven to highest temperature (ideally 500°F/260°C) with pizza stone if available.",
                        "Stretch dough into a 12-inch circle on floured surface.",
                        "Spread crushed tomatoes evenly over dough, leaving border for crust.",
                        "Arrange mozzarella slices over tomatoes.",
                        "Bake until crust is golden and cheese is bubbling (8-10 minutes).",
                        "Remove from oven, top with fresh basil leaves and drizzle with olive oil.",
                        "Season with salt, slice and serve immediately."
                    },
                    Servings = 4,
                    CookingTime = 10,
                    PrepTime = 20,
                    Cuisine = "Italian",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 300,
                        Carbohydrates = 40,
                        Protein = 12,
                        Fat = 10
                    },
                    UserId = user1.Id,
                    Author = user1
                },
                new Recipe
                {
                    Title = "Minestrone Soup",
                    Image = "https://source.unsplash.com/featured/?soup,minestrone",
                    Description = "Hearty Italian vegetable soup with beans and pasta.",
                    Ingredients = new List<string>
                    {
                        "2 tbsp olive oil",
                        "1 large onion, diced",
                        "2 carrots, diced",
                        "2 celery stalks, diced",
                        "2 cloves garlic, minced",
                        "1 zucchini, diced",
                        "1 can (400g) diced tomatoes",
                        "1 can (400g) cannellini beans, drained",
                        "1.5L vegetable broth",
                        "100g small pasta (like ditalini)",
                        "1 bay leaf",
                        "1 tsp dried oregano",
                        "2 tbsp fresh parsley, chopped",
                        "Parmesan cheese, grated (for serving)",
                        "Salt and pepper to taste"
                    },
                    Instructions = new List<string>
                    {
                        "Heat olive oil in a large pot over medium heat.",
                        "Add onion, carrots, and celery. Cook until softened (about 5 minutes).",
                        "Add garlic and cook for 30 seconds until fragrant.",
                        "Add zucchini, diced tomatoes, beans, broth, bay leaf, and oregano.",
                        "Bring to a boil, then reduce heat and simmer for 20 minutes.",
                        "Add pasta and cook until al dente (usually 8-10 minutes).",
                        "Remove bay leaf, stir in parsley, and season with salt and pepper.",
                        "Serve hot with grated Parmesan cheese on top."
                    },
                    Servings = 6,
                    CookingTime = 40,
                    PrepTime = 15,
                    Cuisine = "Italian",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 250,
                        Carbohydrates = 40,
                        Protein = 10,
                        Fat = 6
                    },
                    UserId = user1.Id,
                    Author = user1
                }
            };

            // Add recipes for user2
            var user2Recipes = new List<Recipe>
            {
                new Recipe
                {
                    Title = "Thai Green Curry",
                    Image = "https://source.unsplash.com/featured/?curry,thai",
                    Description = "Aromatic Thai curry with coconut milk, vegetables and your choice of protein.",
                    Ingredients = new List<string>
                    {
                        "400ml coconut milk",
                        "2-3 tbsp green curry paste",
                        "500g chicken breast or tofu, sliced",
                        "1 eggplant, diced",
                        "1 red bell pepper, sliced",
                        "100g snow peas",
                        "2 tbsp fish sauce (or soy sauce for vegetarian)",
                        "1 tbsp palm sugar or brown sugar",
                        "4-5 kaffir lime leaves",
                        "1 handful Thai basil leaves",
                        "2 tbsp vegetable oil",
                        "1 red chili, sliced (optional)"
                    },
                    Instructions = new List<string>
                    {
                        "Heat oil in a large pot over medium heat.",
                        "Add curry paste and cook for 1 minute until fragrant.",
                        "Add chicken or tofu and cook until browned on all sides.",
                        "Pour in coconut milk and bring to a simmer.",
                        "Add eggplant and bell pepper, cook for 5 minutes.",
                        "Add fish sauce, sugar, and lime leaves.",
                        "Simmer for 10 minutes until vegetables are tender.",
                        "Add snow peas and cook for 2 more minutes.",
                        "Remove from heat and stir in Thai basil leaves.",
                        "Serve hot with steamed jasmine rice and garnish with sliced chili if desired."
                    },
                    Servings = 4,
                    CookingTime = 25,
                    PrepTime = 15,
                    Cuisine = "Thai",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 420,
                        Carbohydrates = 12,
                        Protein = 28,
                        Fat = 30
                    },
                    UserId = user2.Id,
                    Author = user2
                },
                new Recipe
                {
                    Title = "Beef Tacos",
                    Image = "https://source.unsplash.com/featured/?tacos,mexican",
                    Description = "Classic Mexican tacos with seasoned ground beef and fresh toppings.",
                    Ingredients = new List<string>
                    {
                        "500g ground beef",
                        "1 onion, finely diced",
                        "2 cloves garlic, minced",
                        "2 tbsp taco seasoning",
                        "8 small corn or flour tortillas",
                        "1 cup lettuce, shredded",
                        "1 tomato, diced",
                        "1/2 cup cheddar cheese, shredded",
                        "1/4 cup sour cream",
                        "1/4 cup salsa",
                        "1 avocado, sliced",
                        "Fresh cilantro, chopped",
                        "1 lime, cut into wedges",
                        "Salt to taste",
                        "2 tbsp olive oil"
                    },
                    Instructions = new List<string>
                    {
                        "Heat oil in a large skillet over medium-high heat.",
                        "Add onion and cook until translucent (about 3 minutes).",
                        "Add garlic and cook for 30 seconds until fragrant.",
                        "Add ground beef and cook until browned, breaking it up as it cooks.",
                        "Drain excess fat if necessary.",
                        "Add taco seasoning and 1/4 cup water, stir well.",
                        "Simmer for 5 minutes until the liquid is mostly absorbed.",
                        "Warm tortillas in a dry pan or microwave.",
                        "Assemble tacos: tortilla, beef, lettuce, tomato, cheese, sour cream, salsa, avocado, and cilantro.",
                        "Serve with lime wedges on the side."
                    },
                    Servings = 4,
                    CookingTime = 20,
                    PrepTime = 15,
                    Cuisine = "Mexican",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 450,
                        Carbohydrates = 30,
                        Protein = 25,
                        Fat = 28
                    },
                    UserId = user2.Id,
                    Author = user2
                },
                new Recipe
                {
                    Title = "Chocolate Chip Cookies",
                    Image = "https://source.unsplash.com/featured/?cookies,chocolate",
                    Description = "Classic homemade chocolate chip cookies - soft in the center and crispy at the edges.",
                    Ingredients = new List<string>
                    {
                        "250g all-purpose flour",
                        "1/2 tsp baking soda",
                        "1/2 tsp salt",
                        "170g unsalted butter, melted",
                        "200g brown sugar",
                        "100g granulated sugar",
                        "1 tbsp vanilla extract",
                        "1 egg",
                        "1 egg yolk",
                        "200g semi-sweet chocolate chips"
                    },
                    Instructions = new List<string>
                    {
                        "Preheat oven to 325°F (165°C). Line baking sheets with parchment paper.",
                        "Sift together flour, baking soda, and salt in a bowl.",
                        "In a large bowl, cream together melted butter and sugars until well blended.",
                        "Beat in vanilla, egg, and egg yolk until light and creamy.",
                        "Mix in the sifted ingredients until just blended.",
                        "Stir in chocolate chips by hand.",
                        "Drop cookie dough 1/4 cup at a time onto prepared cookie sheets. Cookies should be about 3 inches apart.",
                        "Bake for 15-17 minutes, or until edges are lightly toasted.",
                        "Cool on baking sheets for a few minutes before transferring to wire racks."
                    },
                    Servings = 24,
                    CookingTime = 15,
                    PrepTime = 15,
                    Cuisine = "American",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 180,
                        Carbohydrates = 24,
                        Protein = 2,
                        Fat = 9
                    },
                    UserId = user2.Id,
                    Author = user2
                },
                new Recipe
                {
                    Title = "Chicken Stir Fry",
                    Image = "https://source.unsplash.com/featured/?stirfry,chicken",
                    Description = "Quick and colorful stir fry with chicken and crisp vegetables in a savory sauce.",
                    Ingredients = new List<string>
                    {
                        "500g chicken breast, sliced into thin strips",
                        "1 red bell pepper, sliced",
                        "1 yellow bell pepper, sliced",
                        "2 carrots, julienned",
                        "1 broccoli head, cut into florets",
                        "1 onion, sliced",
                        "3 cloves garlic, minced",
                        "1 tbsp ginger, grated",
                        "3 tbsp soy sauce",
                        "1 tbsp hoisin sauce",
                        "1 tbsp oyster sauce",
                        "1 tsp sesame oil",
                        "1 tbsp cornstarch",
                        "3 tbsp vegetable oil",
                        "2 green onions, sliced",
                        "1 tbsp sesame seeds (for garnish)"
                    },
                    Instructions = new List<string>
                    {
                        "Mix soy sauce, hoisin sauce, oyster sauce, sesame oil, and cornstarch in a small bowl.",
                        "Heat 1 tbsp oil in a wok or large skillet over high heat.",
                        "Add chicken and stir-fry until no longer pink (3-4 minutes). Remove and set aside.",
                        "Add remaining oil to the wok.",
                        "Add garlic and ginger, stir-fry for 30 seconds until fragrant.",
                        "Add vegetables in order of hardness: carrots first, then broccoli, bell peppers, and finally onion.",
                        "Stir-fry for 4-5 minutes until vegetables are crisp-tender.",
                        "Return chicken to the wok.",
                        "Pour sauce mixture over everything and stir until sauce thickens and coats everything (1-2 minutes).",
                        "Garnish with green onions and sesame seeds. Serve hot with rice or noodles."
                    },
                    Servings = 4,
                    CookingTime = 15,
                    PrepTime = 15,
                    Cuisine = "Asian",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 320,
                        Carbohydrates = 18,
                        Protein = 28,
                        Fat = 16
                    },
                    UserId = user2.Id,
                    Author = user2
                },
                new Recipe
                {
                    Title = "Greek Salad",
                    Image = "https://source.unsplash.com/featured/?salad,greek",
                    Description = "Fresh Mediterranean salad with tomatoes, cucumber, olives, and feta cheese.",
                    Ingredients = new List<string>
                    {
                        "4 large tomatoes, cut into chunks",
                        "1 cucumber, sliced",
                        "1 red onion, thinly sliced",
                        "1 green bell pepper, chopped",
                        "200g feta cheese, cubed",
                        "100g kalamata olives",
                        "2 tsp dried oregano",
                        "4 tbsp extra virgin olive oil",
                        "2 tbsp red wine vinegar",
                        "1 tsp Dijon mustard",
                        "Salt and freshly ground black pepper to taste"
                    },
                    Instructions = new List<string>
                    {
                        "In a large bowl, combine tomatoes, cucumber, onion, bell pepper, olives, and feta cheese.",
                        "In a small bowl, whisk together olive oil, red wine vinegar, and Dijon mustard.",
                        "Season the dressing with salt, pepper, and dried oregano.",
                        "Pour the dressing over the salad and toss gently to combine.",
                        "Let the salad sit for 10 minutes to allow flavors to meld.",
                        "Serve at room temperature for the best flavor."
                    },
                    Servings = 4,
                    CookingTime = 0,
                    PrepTime = 15,
                    Cuisine = "Greek",
                    NutritionFacts = new NutritionFacts
                    {
                        Calories = 240,
                        Carbohydrates = 10,
                        Protein = 8,
                        Fat = 20
                    },
                    UserId = user2.Id,
                    Author = user2
                }
            };

            context.Recipes.AddRange(user1Recipes);
            context.Recipes.AddRange(user2Recipes);
            context.SaveChanges();
        }
    }
} 