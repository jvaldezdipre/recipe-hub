const RecipeCard = () => {
  //Will be passing props here
  return (
    <div className="recipe-card">
      <div className="recipe-img-container">
        <img src="" alt="" />
      </div>
      <div className="recipe-description-container">
        <div className="rating"></div>
        <p>Recipe Name</p>
        <div className="recipe-description-container-bottom">
          <div className="user-img">
            <img src="" alt="" />
          </div>
          <p>User Name</p>
          <div className="recipe-card-calories-container">
            <div className="svg"></div>
            <p>Calories</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RecipeCard;
