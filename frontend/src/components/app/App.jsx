import NavBar from "../navbar/NavBar";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import Home from "../home/Home";

function App() {
  return (
    <>
      <NavBar />
      <Routes>
        <Route path="/" element={<Home />} />
      </Routes>
    </>
  );
}

export default App;
