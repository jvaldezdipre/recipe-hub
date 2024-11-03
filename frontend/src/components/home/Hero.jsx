import { Link } from "react-router-dom";
import Button from "../button/Button";

const Hero = () => {
  return (
    <main className="home-hero">
      <div className="hero home-hero-left">
        <h1 className="hero-title">
          Your Daily Dish A <span>Food</span> Journey
        </h1>
        <p className="hero-text">
          Your one-stop destination for all your recipe needs.
        </p>
        <Button className="btn primary medium-btn">Sign Up</Button>
        <p className="hero-log-text">
          Already have an account?
          <Link> Login</Link>
        </p>
      </div>
      <div className="hero home-hero-right">
        <img src="../../../public/hero-dish-Image.png" alt="review of dish" />
      </div>
    </main>
  );
};

export default Hero;
