import RecipeCard from "../recipe-card/RecipeCard";

const TrendingRecipe = () => {
  return (
    <div className="home-trending-recipe">
      <h3>Trending Recipe</h3>
      <p className="home-trending-recipe-view-more">View more</p>
      <div className="home-trending-recipe-cards-container">
        <div className="home-trending-recipe-cards-grid">
          {/* Will be looping through the recipes adn render 6 cards with the hgiest ratings */}
          <RecipeCard />
        </div>
      </div>
    </div>
  );
};

export default TrendingRecipe;
