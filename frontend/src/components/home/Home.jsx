import { Link } from "react-router-dom";
import "./Home.css";
const Home = () => {
  return (
    <>
      <main className="home-hero">
        <div className="hero home-hero-left">
          <h1 className="hero-title">
            Your Daily Dish A <span>Food</span> Journey
          </h1>
          <p className="hero-text">
            Your one-stop destination for all your recipe needs.
          </p>
          <button className="btn primary medium-btn">Sign Up</button>
          <p className="hero-log-text">
            Already have an account?
            <Link> Login</Link>
          </p>
        </div>
        <div className="hero home-hero-right">
          <img src="../../../public/hero-dish-Image.png" alt="review of dish" />
        </div>
      </main>
      <section>
        <div>
          <img src="" alt="" />
        </div>
        <div>
          <h2></h2>
          <p></p>
          <button></button>
        </div>
      </section>
    </>
  );
};

export default Home;
