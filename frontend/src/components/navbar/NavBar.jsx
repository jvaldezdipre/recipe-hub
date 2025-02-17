import { Link } from "react-router-dom";
import { useState } from "react";
import Button from "../button/Button";

import "./NavBar.css";

const NavBar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <nav className="navbar">
      <div className="navbar-logo">
        <h4>
          <svg
            width="25"
            height="26"
            viewBox="0 0 25 26"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M2.01582 25.375L0.0908203 23.45L14.1846 9.35625C13.7721 8.39375 13.7148 7.30521 14.0127 6.09063C14.3106 4.87604 14.9637 3.7875 15.9721 2.825C17.1867 1.61042 18.5387 0.9 20.0283 0.69375C21.5179 0.4875 22.7325 0.854167 23.6721 1.79375C24.6117 2.73333 24.9783 3.94792 24.7721 5.4375C24.5658 6.92708 23.8554 8.27917 22.6408 9.49375C21.6783 10.5021 20.5898 11.1552 19.3752 11.4531C18.1606 11.751 17.0721 11.6938 16.1096 11.2812L14.3908 13L24.8408 23.45L22.9158 25.375L12.4658 14.9938L2.01582 25.375ZM6.07207 13.6188L1.94707 9.49375C0.70957 8.25625 0.0908203 6.77813 0.0908203 5.05938C0.0908203 3.34063 0.70957 1.8625 1.94707 0.625L10.4721 9.21875L6.07207 13.6188Z"
              fill="black"
            />
          </svg>
        </h4>
        <h4 className="logo-text">
          Recipe<span>Hub</span>
        </h4>
      </div>
      <div className="navbar-content">
        <div className="toggle-menu" onClick={toggleMenu}>
          &#9776;
        </div>
        <ul className={`navbar-links ${isMenuOpen ? "active" : ""}`}>
          <li className="navbar-link">
            <Link to="/">Home</Link>
          </li>
          <li className="navbar-link">
            <Link to="/recipes">Recipes</Link>
          </li>
          <li className="navbar-link">
            <Link to="/add-recipe">Add Recipe</Link>
          </li>
          <li className="navbar-link">
            <Link to="/about-us">About Us</Link>
          </li>
        </ul>
        <div className={`login-signup-container ${isMenuOpen ? "active" : ""}`}>
          <Button className="btn small-btn secondary">Login</Button>
          <Button className="btn small-btn primary">Sign Up</Button>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
