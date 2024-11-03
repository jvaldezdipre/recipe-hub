import Button from "../button/Button";

const Section = () => {
  return (
    <section className="home-section">
      <div>
        <img
          src="../../../public/close-up-food-lover-taking-pictures-meal-1.jpg"
          alt="close up food over taking pictures meal"
        />
      </div>
      <div className="home-section-right">
        <h2>
          Share Your <span>Recipes</span>
        </h2>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas
          vitae enim pharetra, venenatis nunc eget, finibus est. Proin velit
        </p>
        <Button className="btn primary medium-btn">Create New Recipe</Button>
      </div>
    </section>
  );
};

export default Section;
